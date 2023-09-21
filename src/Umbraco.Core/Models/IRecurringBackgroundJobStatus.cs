using Umbraco.Cms.Core.Models.Entities;

namespace Umbraco.Cms.Core.Models
{
    public interface IRecurringBackgroundJobStatus : IEntity, IRememberBeingDirty
    {
        string Name { get; set; }
        DateTime? LastExecutionDateTime { get; set; }
        bool? LastExecutionWasSuccessful { get; set; }
    }
}
