using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Api.Management.Routing;
using Umbraco.Cms.Web.Common.Authorization;
using Constants = Umbraco.Cms.Core.Constants;

namespace Umbraco.Cms.Api.Management.Controllers.BackgroundJob;

[ApiController]
[VersionedApiBackOfficeRoute($"{Constants.BackgroundJobs.RoutePath.BackgroundJob}")]
[ApiExplorerSettings(GroupName = "Background Job")]
[Authorize(Policy = "New" + AuthorizationPolicies.SectionAccessSettings)]
public abstract class BackgroundJobControllerBase : ManagementApiControllerBase
{
}
