namespace Umbraco.Cms.Api.Management.ViewModels.RecurringBackgroundJobStatus;

public class RecurringBackgroundJobStatusResponseModel
{

    public string Name { get; set; } = "";

    public DateTime? LastExecutionDateTime { get; set; }
    public bool? LastExecutionWasSuccessful { get; set; }
}
