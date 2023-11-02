
using System;
using global::Umbraco.Cms.Core.Events;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Notifications;

namespace Umbraco.Cms.Infrastructure.BackgroundJobs
{
    public class NotificationHandlers
    {
        public class ExecutingNotificationHandler : INotificationAsyncHandler<RecurringBackgroundJobExecutingNotification>
        {
            private readonly IRecurringBackgroundJobStatusService _service;
            private readonly RecurringBackgroundJobStatusSettings _options;

            public ExecutingNotificationHandler(
                IRecurringBackgroundJobStatusService service,
                IOptions<RecurringBackgroundJobStatusSettings> options)
            {
                _service = service;
                _options = options.Value;
            }

            
            public async Task HandleAsync(RecurringBackgroundJobExecutingNotification notification, CancellationToken token)
            {
                var name = notification.Job.GetType().FullName!;
                if (!_options.LogJobStarts || _options.IgnoredJobs.Contains(name))
                {
                    return;
                }

                var jobStatus = await _service.GetByNameAsync(name) ??
                    new RecurringBackgroundJobStatus(name);

                jobStatus.LastExecutionDateTime = DateTime.Now;
                jobStatus.LastExecutionWasSuccessful = null;
                await _service.CreateUpdateAsync(jobStatus);
            }
        }
        public class ExecutedNotificationHandler : INotificationAsyncHandler<RecurringBackgroundJobExecutedNotification>
        {
            private readonly IRecurringBackgroundJobStatusService _service;
            private readonly RecurringBackgroundJobStatusSettings _options;

            public ExecutedNotificationHandler(
                IRecurringBackgroundJobStatusService service,
                IOptions<RecurringBackgroundJobStatusSettings> options)
            {
                _service = service;
                _options = options.Value;
            }

            public async Task HandleAsync(RecurringBackgroundJobExecutedNotification notification, CancellationToken cancellationToken)
            {
                var name = notification.Job.GetType().FullName!;
                if (!_options.LogJobSuccesses || _options.IgnoredJobs.Contains(name))
                {
                    return;
                }

                var jobStatus = await _service.GetByNameAsync(name) ??
                    new RecurringBackgroundJobStatus(name);

                jobStatus.LastExecutionWasSuccessful = true;
                await _service.CreateUpdateAsync(jobStatus);
            }
        }

        public class FailedNotificationHandler : INotificationAsyncHandler<RecurringBackgroundJobFailedNotification>
        {
            private readonly IRecurringBackgroundJobStatusService _service;
            private readonly RecurringBackgroundJobStatusSettings _options;

            public FailedNotificationHandler(
                IRecurringBackgroundJobStatusService service,
                IOptions<RecurringBackgroundJobStatusSettings> options)
            {
                _service = service;
                _options = options.Value;
            }
            public async Task HandleAsync(RecurringBackgroundJobFailedNotification notification, CancellationToken cancellationToken)
            {
                var name = notification.Job.GetType().FullName!;
                if (_options.LogJobFailures == false || _options.IgnoredJobs.Contains(name))
                {
                    return;
                }

                var jobStatus = await _service.GetByNameAsync(name) ??
                    new RecurringBackgroundJobStatus(name);

                jobStatus.LastExecutionWasSuccessful = false;
                await _service.CreateUpdateAsync(jobStatus);
            }
        }
    }
}
