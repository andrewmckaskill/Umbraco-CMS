using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Api.Common.ViewModels.Pagination;
using Umbraco.Cms.Api.Management.Factories;
using Umbraco.Cms.Api.Management.ViewModels.BackgroundJobs;
using Umbraco.Cms.Api.Management.ViewModels.HealthCheck;
using Umbraco.Cms.Core.Mapping;

namespace Umbraco.Cms.Api.Management.Controllers.BackgroundJob;

[ApiVersion("1.0")]
public class AllBackgroundJobController : BackgroundJobControllerBase
{
    private readonly IBackgroundJobPresentationFactory _backgroundJobPresentationFactory;
    private readonly IUmbracoMapper _umbracoMapper;

    public AllBackgroundJobController(
        IBackgroundJobPresentationFactory backgroundJobPresentationFactory,
        IUmbracoMapper umbracoMapper)
    {
        _backgroundJobPresentationFactory = backgroundJobPresentationFactory;
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
    [ProducesResponseType(typeof(PagedViewModel<BackgroundJobPresentationModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedViewModel<BackgroundJobPresentationModel>>> All(int skip = 0, int take = 100)
    {
        BackgroundJobPresentationModel[] jobs = _backgroundJobPresentationFactory
            .GetBackgroundJobOverview()
            .ToArray();

        var viewModel = new PagedViewModel<BackgroundJobPresentationModel>
        {
            Total = jobs.Length,
            Items = jobs.Skip(skip).Take(take)
        };

        return await Task.FromResult(Ok(viewModel));
    }
}
