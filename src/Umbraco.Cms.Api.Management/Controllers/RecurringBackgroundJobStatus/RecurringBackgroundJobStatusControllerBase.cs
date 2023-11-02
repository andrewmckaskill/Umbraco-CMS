using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Api.Management.Routing;
using Umbraco.Cms.Web.Common.Authorization;
using Constants = Umbraco.Cms.Core.Constants;

namespace Umbraco.Cms.Api.Management.Controllers.RecurringBackgroundJobStatus;

[ApiController]
[VersionedApiBackOfficeRoute($"{Constants.RecurringBackgroundJobStatus.RoutePath.RecurringBackgroundJobStatus}")]
[ApiExplorerSettings(GroupName = "Recurring Background Jobs")]
[Authorize(Policy = "New" + AuthorizationPolicies.SectionAccessSettings)]
public abstract class RecurringBackgroundJobStatusControllerBase : ManagementApiControllerBase
{
}
