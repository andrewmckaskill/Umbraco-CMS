using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Api.Management.ViewModels.BackgroundJobs;
using Umbraco.Cms.Api.Management.ViewModels.HealthCheck;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.HealthChecks;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Infrastructure.BackgroundJobs;

namespace Umbraco.Cms.Api.Management.Factories;

public class BackgroundJobPresentationFactory : IBackgroundJobPresentationFactory
{

    private readonly IEnumerable<IRecurringBackgroundJob> _backgroundJobs;
    private readonly ILogger _logger;
    private readonly IUmbracoMapper _umbracoMapper;

    public BackgroundJobPresentationFactory(
        IEnumerable<IRecurringBackgroundJob> backgroundJobs,
        ILogger<IBackgroundJobPresentationFactory> logger,
        IUmbracoMapper umbracoMapper)
    {

        _backgroundJobs = backgroundJobs;
        _logger = logger;
        _umbracoMapper = umbracoMapper;
    }

    public IEnumerable<BackgroundJobPresentationModel> GetBackgroundJobOverview()
    {

        IEnumerable<BackgroundJobPresentationModel> models = _backgroundJobs
            .Select(CreateBackgroundJobPresentationModel)
            .OrderBy(x => x.Name);

        return models;
    }

   

    public BackgroundJobPresentationModel CreateBackgroundJobPresentationModel(IRecurringBackgroundJob job)
    {


        var backgroundJobPresentationModel = new BackgroundJobPresentationModel
        {
            Name = job.GetType().Name,
        };

        return backgroundJobPresentationModel;
    }
}
