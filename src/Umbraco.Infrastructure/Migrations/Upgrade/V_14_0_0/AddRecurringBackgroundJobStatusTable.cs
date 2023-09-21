using Umbraco.Cms.Infrastructure.Persistence.Dtos;
using Umbraco.Extensions;

namespace Umbraco.Cms.Infrastructure.Migrations.Upgrade.V_14_0_0;

public class AddRecurringBackgroundJobStatusTable : MigrationBase
{
    public AddRecurringBackgroundJobStatusTable(IMigrationContext context) : base(context)
    {
    }

    protected override void Migrate()
    {
        IEnumerable<string> tables = SqlSyntax.GetTablesInSchema(Context.Database);

        if (tables.InvariantContains(RecurringBackgroundJobStatusDto.TableName))
        {
            return;
        }

        Create.Table<RecurringBackgroundJobStatusDto>().Do();
    }
}
