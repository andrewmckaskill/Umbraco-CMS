namespace Umbraco.Cms.Api.Management.ViewModels.BackgroundJobs;

public class BackgroundJobPresentationModel 
{

    public required string Name { get; set; }


    public DateTime? LastExecutionDateTime { get; set; }
    public bool? LastExecutionSuccess { get; set; }
}
