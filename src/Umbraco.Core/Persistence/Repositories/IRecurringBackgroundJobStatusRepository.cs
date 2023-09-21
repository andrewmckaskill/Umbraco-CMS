using Umbraco.Cms.Core.Models;

namespace Umbraco.Cms.Core.Persistence.Repositories
{
    public interface IRecurringBackgroundJobStatusRepository : IReadWriteQueryRepository<int, IRecurringBackgroundJobStatus>
    {
        bool Exists(string name);
        IEnumerable<IRecurringBackgroundJobStatus> GetAll();
        IRecurringBackgroundJobStatus? GetByName(string name);
    }
}
