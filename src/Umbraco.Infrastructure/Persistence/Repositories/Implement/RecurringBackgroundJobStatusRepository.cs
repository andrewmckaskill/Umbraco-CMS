using System.Data;
using System.Linq;
using Microsoft.Extensions.Logging;
using NPoco;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Persistence.Querying;
using Umbraco.Cms.Core.Persistence.Repositories;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Infrastructure.Persistence.Dtos;
using Umbraco.Cms.Infrastructure.Persistence.Factories;
using Umbraco.Cms.Infrastructure.Persistence.Querying;
using Umbraco.Cms.Infrastructure.Scoping;
using Umbraco.Extensions;

namespace Umbraco.Cms.Infrastructure.Persistence.Repositories.Implement;

internal class RecurringBackgroundJobStatusRepository : EntityRepositoryBase<int, IRecurringBackgroundJobStatus>, IRecurringBackgroundJobStatusRepository
{
    public RecurringBackgroundJobStatusRepository(IScopeAccessor scopeAccessor, AppCaches cache, ILogger<RecurringBackgroundJobStatusRepository> logger)
        : base(scopeAccessor, cache, logger)
    { }

    public IRecurringBackgroundJobStatus? GetByName(string name)
        => GetMany().FirstOrDefault(x => x.Name.InvariantEquals(name));

    public bool Exists(string name)
        => GetMany().Any(x => x.Name.InvariantEquals(name));

    public IEnumerable<IRecurringBackgroundJobStatus> GetAll()
        => GetMany();



    protected override IRecurringBackgroundJobStatus? PerformGet(int id)
        // Use the underlying GetAll which will force cache all 
        => GetMany().FirstOrDefault(x => x.Id == id);

    protected override IEnumerable<IRecurringBackgroundJobStatus> PerformGetAll(params int[]? ids)
    {
        Sql<ISqlContext> sql = GetBaseQuery(false);
        if (ids?.Any() ?? false)
        {
            sql.WhereIn<RecurringBackgroundJobStatusDto>(x => x.Id, ids);
        }
        sql.OrderBy<RecurringBackgroundJobStatusDto>(dto => dto.Name);

        return Database.Fetch<RecurringBackgroundJobStatusDto>(sql).Select(RecurringBackgroundJobStatusFactory.BuildEntity);
    }

    protected override IEnumerable<IRecurringBackgroundJobStatus> PerformGetByQuery(IQuery<IRecurringBackgroundJobStatus> query)
    {
        Sql<ISqlContext> sqlClause = GetBaseQuery(false);
        var translator = new SqlTranslator<IRecurringBackgroundJobStatus>(sqlClause, query);
        Sql<ISqlContext> sql = translator.Translate();

        List<RecurringBackgroundJobStatusDto>? dtos = Database.Fetch<RecurringBackgroundJobStatusDto>(sql);

        foreach (RecurringBackgroundJobStatusDto dto in dtos)
        {
            yield return RecurringBackgroundJobStatusFactory.BuildEntity(dto);
        }
    }

protected override Sql<ISqlContext> GetBaseQuery(bool isCount)
    {
        Sql<ISqlContext> sql = Sql();
        if (isCount)
        {
            sql.SelectCount().From<RecurringBackgroundJobStatusDto>();
        }
        else
        {
            sql.SelectAll()
                .From<RecurringBackgroundJobStatusDto>();
        }

        return sql;
    }

    protected override string GetBaseWhereClause()
        => $"{Constants.DatabaseSchema.Tables.RecurringBackgroundJobStatus}.id = @id";

    protected override IEnumerable<string> GetDeleteClauses()
        => new[]
        {
            $"DELETE FROM {Constants.DatabaseSchema.Tables.RecurringBackgroundJobStatus} WHERE id = @id",
        };

    protected override void PersistNewItem(IRecurringBackgroundJobStatus entity)
    {
        var exists = Database.ExecuteScalar<int>(
            $"SELECT COUNT(*) FROM {Constants.DatabaseSchema.Tables.RecurringBackgroundJobStatus} WHERE name = @name",
            new { name = entity.Name });
        if (exists > 0)
        {
            throw new DuplicateNameException($"The name {entity.Name} is already assigned.");
        }

        entity.AddingEntity();


        RecurringBackgroundJobStatusDto dto = RecurringBackgroundJobStatusFactory.BuildDto(entity);

        var id = Convert.ToInt32(Database.Insert(dto));
        entity.Id = id;

        entity.ResetDirtyProperties();
    }

    protected override void PersistUpdatedItem(IRecurringBackgroundJobStatus entity)
    {
        entity.UpdatingEntity();

        // Ensure there is no other record with the same name on another entity
        var exists = Database.ExecuteScalar<int>(
            $"SELECT COUNT(*) FROM {Constants.DatabaseSchema.Tables.RecurringBackgroundJobStatus} WHERE name = @name AND id <> @id",
            new { name = entity.Name, id = entity.Id });
        if (exists > 0)
        {
            throw new DuplicateNameException($"The name {entity.Name} is already assigned.");
        }

        RecurringBackgroundJobStatusDto dto = RecurringBackgroundJobStatusFactory.BuildDto(entity);

        Database.Update(dto);

        entity.ResetDirtyProperties();
    }


}
