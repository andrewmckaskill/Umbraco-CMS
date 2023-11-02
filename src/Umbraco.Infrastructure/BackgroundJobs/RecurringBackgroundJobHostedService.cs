using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog.Core;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Runtime;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Sync;
using Umbraco.Cms.Infrastructure.HostedServices;

namespace Umbraco.Cms.Infrastructure.BackgroundJobs;

public static class RecurringBackgroundJobHostedService
{
    public static Func<IRecurringBackgroundJob, IHostedService> CreateHostedServiceFactory(IServiceProvider serviceProvider) =>
        (IRecurringBackgroundJob job) =>
        {
            Type hostedServiceType = typeof(RecurringBackgroundJobHostedService<>).MakeGenericType(job.GetType());
            return (IHostedService)ActivatorUtilities.CreateInstance(serviceProvider, hostedServiceType, job);
        };
}

/// <summary>
/// Runs a recurring background job inside a hosted service.
/// Generic version for DependencyInjection
/// </summary>
/// <typeparam name="TJob">Type of the Job</typeparam>
public class RecurringBackgroundJobHostedService<TJob> : RecurringHostedServiceBase where TJob : IRecurringBackgroundJob
{

    private readonly ILogger<RecurringBackgroundJobHostedService<TJob>> _logger;
    private readonly IMainDom _mainDom;
    private readonly IRuntimeState _runtimeState;
    private readonly IServerRoleAccessor _serverRoleAccessor;
    private readonly IEventAggregator _eventAggregator;
    private readonly IRecurringBackgroundJob _job;
    private readonly string _jobName;

    public RecurringBackgroundJobHostedService(
        IRuntimeState runtimeState,
        ILogger<RecurringBackgroundJobHostedService<TJob>> logger,
        IMainDom mainDom,
        IServerRoleAccessor serverRoleAccessor,
        IEventAggregator eventAggregator,
        TJob job)
        : base(logger, job.Period, job.Delay)
    {
        _runtimeState = runtimeState;
        _logger = logger;
        _mainDom = mainDom;
        _serverRoleAccessor = serverRoleAccessor;
        _eventAggregator = eventAggregator;
        _job = job;
        _jobName = job.GetType().Name;

        _job.PeriodChanged += (sender, e) => ChangePeriod(_job.Period);
    }

    public string JobName { get { return _jobName; } }

    /// <inheritdoc />
    public override async Task PerformExecuteAsync(object? state)
    {
        _logger.LogDebug($"Job {_jobName} checking");
        var executingNotification = new Notifications.RecurringBackgroundJobExecutingNotification(_job, new EventMessages());


        try
        {

            if (_runtimeState.Level != RuntimeLevel.Run)
            {
                _logger.LogDebug($"Job {_jobName} not running as runlevel not yet ready");
                return;
            }

            // Don't run on replicas nor unknown role servers
            if (!_job.ServerRoles.Contains(_serverRoleAccessor.CurrentServerRole))
            {
                _logger.LogDebug($"Job {_jobName} not running on this server role");
                return;
            }

            // Ensure we do not run if not main domain, but do NOT lock it
            if (!_mainDom.IsMainDom)
            {
                _logger.LogDebug($"Job {_jobName} not running as not MainDom");
                return;
            }

            _logger.LogDebug($"Job {_jobName} executing");

            await _eventAggregator.PublishAsync(executingNotification);

            await _job.RunJobAsync();
            await _eventAggregator.PublishAsync(new Notifications.RecurringBackgroundJobExecutedNotification(_job, new EventMessages()).WithStateFrom(executingNotification));
            _logger.LogDebug($"Job {_jobName} Completed");

        }
        catch (Exception ex)
        {
            await _eventAggregator.PublishAsync(new Notifications.RecurringBackgroundJobFailedNotification(_job, new EventMessages()).WithStateFrom(executingNotification));
            _logger.LogError(ex, "Unhandled exception in recurring background job.");
        }

    }

}
