using Umbraco.Cms.Api.Management.ViewModels.BackgroundJobs;
using Umbraco.Cms.Infrastructure.BackgroundJobs;

namespace Umbraco.Cms.Api.Management.Factories
{
    public interface IBackgroundJobPresentationFactory
    {
        BackgroundJobPresentationModel CreateBackgroundJobPresentationModel(IRecurringBackgroundJob job);
        IEnumerable<BackgroundJobPresentationModel> GetBackgroundJobOverview();
    }
}
