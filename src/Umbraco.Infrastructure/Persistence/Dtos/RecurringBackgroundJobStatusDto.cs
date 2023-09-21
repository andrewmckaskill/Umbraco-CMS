using NPoco;
using Umbraco.Cms.Core;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Umbraco.Cms.Infrastructure.Persistence.Dtos;

[TableName(TableName)]
[PrimaryKey("id")]
[ExplicitColumns]
internal class RecurringBackgroundJobStatusDto
{
    public const string TableName = Constants.DatabaseSchema.Tables.RecurringBackgroundJobStatus;

    /// <summary>
    ///     Gets or sets the identifier of the language.
    /// </summary>
    [Column("id")]
    [PrimaryKeyColumn(IdentitySeed = 2)]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the Recurring background job
    /// </summary>
    [Column("name")]
    [Index(IndexTypes.UniqueNonClustered)]
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the datetime of the last execution
    /// </summary>
    [Column("lastExecutionDateTime")]
    [NullSetting(NullSetting = NullSettings.Null)]
    
    public DateTime? LastExecutionDateTime { get; set; }

    

    /// <summary>
    ///     Gets or sets a value indicating whether the last execution was successful
    /// </summary>
    [Column("lastExecutionWasSuccessful")]
    [NullSetting(NullSetting = NullSettings.Null)]
    public bool? LastExecutionWasSuccessful { get; set; }

    
}
