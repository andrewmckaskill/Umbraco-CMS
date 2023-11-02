using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Api.Management.ViewModels.RecurringBackgroundJobStatus;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.HealthChecks;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;


namespace Umbraco.Cms.Api.Management.Factories;

public class RecurringBackgroundJobStatusPresentationFactory : IRecurringBackgroundJobStatusPresentationFactory
{

    private readonly IRecurringBackgroundJobStatusService _recurringBackgroundJobStatusService;
    private readonly ILogger _logger;
    private readonly IUmbracoMapper _umbracoMapper;

    public RecurringBackgroundJobStatusPresentationFactory(
        IRecurringBackgroundJobStatusService recurringBackgroundJobStatusService,
        ILogger<IRecurringBackgroundJobStatusPresentationFactory> logger,
        IUmbracoMapper umbracoMapper)
    {

        _recurringBackgroundJobStatusService = recurringBackgroundJobStatusService;
        _logger = logger;
        _umbracoMapper = umbracoMapper;
    }


    public RecurringBackgroundJobStatusResponseModel CreateBackgroundJobPresentationModel(IRecurringBackgroundJobStatus job)
        => _umbracoMapper.Map<RecurringBackgroundJobStatusResponseModel>(job)!;
}
