using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Api.Management.ViewModels.RecurringBackgroundJobStatus;
using Umbraco.Extensions;

namespace Umbraco.Cms.Api.Management.Mapping.RecurringBackgroundJobStatus;

public class RecurringBackgroundJobStatusMapDefinition : IMapDefinition
{
    public void DefineMaps(IUmbracoMapper mapper)
    {
        mapper.Define<IRecurringBackgroundJobStatus, RecurringBackgroundJobStatusResponseModel>((_, _) => new RecurringBackgroundJobStatusResponseModel(), Map);
    }

    // Umbraco.Code.MapAll
    private void Map(IRecurringBackgroundJobStatus source, RecurringBackgroundJobStatusResponseModel target, MapperContext context)
    {
        target.Name = source.Name;
        target.LastExecutionDateTime = source.LastExecutionDateTime;
        target.LastExecutionWasSuccessful = source.LastExecutionWasSuccessful;
    }

}
