using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Infrastructure.Persistence.Dtos;

namespace Umbraco.Cms.Infrastructure.Persistence.Factories;

internal static class RecurringBackgroundJobStatusFactory
{
    #region Implementation of IEntityFactory<RecurringBackgroundJobStatus,RecurringBackgroundJobStatusDto>

    public static IRecurringBackgroundJobStatus BuildEntity(RecurringBackgroundJobStatusDto dto)
    {
        var item = new RecurringBackgroundJobStatus(dto.Name)
        {
            Id = dto.Id,
            Name = dto.Name,
            LastExecutionDateTime = dto.LastExecutionDateTime,
            LastExecutionWasSuccessful = dto.LastExecutionWasSuccessful
        };

        // reset dirty initial properties (U4-1946)
        item.ResetDirtyProperties(false);
        return item;
    }

    public static RecurringBackgroundJobStatusDto BuildDto(IRecurringBackgroundJobStatus entity) =>
        new RecurringBackgroundJobStatusDto
        {
            Id = entity.Id,
            Name = entity.Name,
            LastExecutionDateTime = entity.LastExecutionDateTime,
            LastExecutionWasSuccessful = entity.LastExecutionWasSuccessful
        };

    #endregion
}
