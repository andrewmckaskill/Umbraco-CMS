using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Infrastructure.Persistence.Dtos;

namespace Umbraco.Cms.Infrastructure.Persistence.Mappers;

[MapperFor(typeof(IRecurringBackgroundJobStatus))]
[MapperFor(typeof(RecurringBackgroundJobStatus))]
public sealed class RecurringBackgroundJobStatusMapper : BaseMapper
{
    public RecurringBackgroundJobStatusMapper(Lazy<ISqlContext> sqlContext, MapperConfigurationStore maps)
        : base(sqlContext, maps)
    {
    }

    protected override void DefineMaps()
    {
        DefineMap<RecurringBackgroundJobStatus, RecurringBackgroundJobStatusDto>(nameof(RecurringBackgroundJobStatus.Id), nameof(RecurringBackgroundJobStatusDto.Id));
        DefineMap<RecurringBackgroundJobStatus, RecurringBackgroundJobStatusDto>(nameof(RecurringBackgroundJobStatus.Name), nameof(RecurringBackgroundJobStatusDto.Name));
        DefineMap<RecurringBackgroundJobStatus, RecurringBackgroundJobStatusDto>(nameof(RecurringBackgroundJobStatus.LastExecutionDateTime), nameof(RecurringBackgroundJobStatusDto.LastExecutionDateTime));
        DefineMap<RecurringBackgroundJobStatus, RecurringBackgroundJobStatusDto>(nameof(RecurringBackgroundJobStatus.LastExecutionWasSuccessful), nameof(RecurringBackgroundJobStatusDto.LastExecutionWasSuccessful));
        
    }
}
