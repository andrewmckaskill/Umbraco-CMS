using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Persistence.Querying;
using Umbraco.Cms.Core.Persistence.Repositories;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services.OperationStatus;

namespace Umbraco.Cms.Core.Services;

internal sealed class RecurringBackgroundJobStatusService : RepositoryService, IRecurringBackgroundJobStatusService
{
    private readonly IRecurringBackgroundJobStatusRepository _recurringBackgroundJobStatusRepository;


    public RecurringBackgroundJobStatusService(
        ICoreScopeProvider provider,
        ILoggerFactory loggerFactory,
        IEventMessagesFactory eventMessagesFactory,
        IRecurringBackgroundJobStatusRepository recurringBackgroundJobStatusRepository
        )
        : base(provider, loggerFactory, eventMessagesFactory)
    {
        _recurringBackgroundJobStatusRepository = recurringBackgroundJobStatusRepository;
    }

    /// <inheritdoc />
    public async Task<IRecurringBackgroundJobStatus?> GetByNameAsync(string name)
    {
        using (ScopeProvider.CreateCoreScope(autoComplete: true))
        {
            IRecurringBackgroundJobStatus? item = _recurringBackgroundJobStatusRepository.GetByName(name);
            return await Task.FromResult(item);
        }
    }


    /// <inheritdoc />
    public async Task<IEnumerable<IRecurringBackgroundJobStatus>> GetManyAsync(params int[] ids)
    {
        using (ScopeProvider.CreateCoreScope(autoComplete: true))
        {
            IEnumerable<IRecurringBackgroundJobStatus> items = _recurringBackgroundJobStatusRepository.GetMany(ids).ToArray();
            return await Task.FromResult(items);
        }
    }




    public async Task<bool> ExistsAsync(string key)
    {
        using (ScopeProvider.CreateCoreScope(autoComplete: true))
        {
            bool exists = _recurringBackgroundJobStatusRepository.Exists(key);
            return await Task.FromResult(exists);
        }
    }



    public async Task CreateUpdateAsync(IRecurringBackgroundJobStatus recurringBackgroundJobStatus)
    {
        using (ICoreScope scope = ScopeProvider.CreateCoreScope())
        {
            _recurringBackgroundJobStatusRepository.Save(recurringBackgroundJobStatus);

            scope.Complete();
        }
    }

    private async Task<IEnumerable<IRecurringBackgroundJobStatus>> GetByQueryAsync(IQuery<IRecurringBackgroundJobStatus> query)
    {
        using (ScopeProvider.CreateCoreScope(autoComplete: true))
        {
            IRecurringBackgroundJobStatus[] items = _recurringBackgroundJobStatusRepository.Get(query).ToArray();
            return await Task.FromResult(items);
        }
    }


}
