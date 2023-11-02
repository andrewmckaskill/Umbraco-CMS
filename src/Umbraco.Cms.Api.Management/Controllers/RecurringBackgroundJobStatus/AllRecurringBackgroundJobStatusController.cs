using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Api.Common.ViewModels.Pagination;
using Umbraco.Cms.Api.Management.Factories;
using Umbraco.Cms.Api.Management.ViewModels.RecurringBackgroundJobStatus;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Cms.Api.Management.Controllers.RecurringBackgroundJobStatus;

[ApiVersion("1.0")]
public class AllRecurringBackgroundJobStatusController : RecurringBackgroundJobStatusControllerBase
{
    private readonly IRecurringBackgroundJobStatusPresentationFactory _presentationFactory;
    private readonly IRecurringBackgroundJobStatusService _service;
    private readonly IUmbracoMapper _umbracoMapper;

    public AllRecurringBackgroundJobStatusController(
        IRecurringBackgroundJobStatusPresentationFactory presentationFactory,
        IRecurringBackgroundJobStatusService service,
         IUmbracoMapper umbracoMapper)
    {
        _presentationFactory = presentationFactory;
        _service = service;
        _umbracoMapper = umbracoMapper;
    }

    /// <summary>
    ///     Gets a paginated grouped list of all names the health checks are grouped by.
    /// </summary>
    /// <param name="skip">The amount of items to skip.</param>
    /// <param name="take">The amount of items to take.</param>
    /// <returns>The paged result of health checks group names.</returns>
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(PagedViewModel<RecurringBackgroundJobStatusResponseModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedViewModel<RecurringBackgroundJobStatusResponseModel>>> All(int skip = 0, int take = 100)
    {
        var jobs = await _service.GetManyAsync();
        var jobsArray = jobs.ToArray();


        var viewModel = new PagedViewModel<RecurringBackgroundJobStatusResponseModel>
        {
            Total = jobsArray.Length,
            Items = jobsArray.Skip(skip).Take(take).Select(_presentationFactory.CreateBackgroundJobPresentationModel)
        };

        return await Task.FromResult(Ok(viewModel));
    }
}
