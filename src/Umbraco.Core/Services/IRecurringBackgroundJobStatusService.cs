using Umbraco.Cms.Core.Models;

namespace Umbraco.Cms.Core.Services
{
    public interface IRecurringBackgroundJobStatusService
    {
        Task CreateUpdateAsync(IRecurringBackgroundJobStatus recurringBackgroundJobStatus);
        Task<bool> ExistsAsync(string key);
        Task<IRecurringBackgroundJobStatus?> GetByNameAsync(string name);
        Task<IEnumerable<IRecurringBackgroundJobStatus>> GetManyAsync(params int[] ids);
    }
}
