using Umbraco.Cms.Api.Management.ViewModels.RecurringBackgroundJobStatus;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Infrastructure.BackgroundJobs;

namespace Umbraco.Cms.Api.Management.Factories
{
    public interface IRecurringBackgroundJobStatusPresentationFactory
    {
        RecurringBackgroundJobStatusResponseModel CreateBackgroundJobPresentationModel(IRecurringBackgroundJobStatus recurringBackgroundJobStatus);
    }
}
