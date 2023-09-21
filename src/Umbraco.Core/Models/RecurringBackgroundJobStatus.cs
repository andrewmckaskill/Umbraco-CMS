using System.Runtime.Serialization;
using Umbraco.Cms.Core.Models.Entities;
using Umbraco.Extensions;

namespace Umbraco.Cms.Core.Models;

/// <summary>
///     Represents a Dictionary Item
/// </summary>
[Serializable]
[DataContract(IsReference = true)]
public class RecurringBackgroundJobStatus : EntityBase, IRecurringBackgroundJobStatus
{

    private string _name;
    private DateTime? _lastExecutionDateTime;
    private bool? _lastExecutionWasSuccessful;

    public RecurringBackgroundJobStatus(string name)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Value can't be empty or consist only of white-space characters.",
                nameof(name));
        }

        _name = name;

    }

    /// <summary>
    ///     Gets or sets the name of the background job status record
    /// </summary>
    [DataMember]
    public string Name
    {
        get => _name;
        set => SetPropertyValueAndDetectChanges(value, ref _name!, nameof(Name));
    }

    /// <summary>
    ///     Gets or sets the last execution date/time for the background job status record
    /// </summary>
    [DataMember]
    public DateTime? LastExecutionDateTime
    {
        get => _lastExecutionDateTime;
        set => SetPropertyValueAndDetectChanges(value, ref _lastExecutionDateTime, nameof(LastExecutionDateTime));
    }

    /// <summary>
    ///     Gets or sets a last execution success flag for the background job status record
    /// </summary>
    [DataMember]
    public bool? LastExecutionWasSuccessful
    {
        get => _lastExecutionWasSuccessful;
        set => SetPropertyValueAndDetectChanges(value, ref _lastExecutionWasSuccessful!, nameof(LastExecutionDateTime));
    }
}
