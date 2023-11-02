// Copyright (c) Umbraco.
// See LICENSE for more details.

using System.ComponentModel;

namespace Umbraco.Cms.Core.Configuration.Models;

/// <summary>
///     Typed configuration options for healthchecks settings.
/// </summary>
[UmbracoOptions(Constants.Configuration.ConfigRecurringBackgroundJobStatus)]
public class RecurringBackgroundJobStatusSettings
{
#if DEBUG
    internal const bool StaticLogJobFailures = true;
    internal const bool StaticLogJobStarts = false;
    internal const bool StaticLogJobSuccesses = false;
    internal static readonly string[] StaticIgnoredJobs = new string[]
    {
        "Umbraco.Cms.Infrastructure.BackgroundJobs.Jobs.ServerRegistration.InstructionProcessJob",
        "Umbraco.Cms.Infrastructure.BackgroundJobs.Jobs.ServerRegistration.TouchServerJob"
    };
#else
    internal const bool StaticLogJobFailures = true;
    internal const bool StaticLogJobStarts = true;
    internal const bool StaticLogJobSuccesses = true;
    internal readonly string[] StaticIgnoredJobs = new string[] {};
#endif

    /// <summary>
    ///     Gets or sets a value for the collection of healthchecks that are disabled.
    /// </summary>
    public List<string> IgnoredJobs { get; set; } = new List<string>(StaticIgnoredJobs);

    /// <summary>
    ///     Gets or sets a value indicating whether successful job failures should be logged on not.
    /// </summary>
    [DefaultValue(StaticLogJobFailures)]
    public bool LogJobFailures { get; set; } = StaticLogJobFailures;

    /// <summary>
    ///     Gets or sets a value indicating whether successful job failures should be logged on not.
    /// </summary>
    [DefaultValue(StaticLogJobSuccesses)]
    public bool LogJobSuccesses { get; set; } = StaticLogJobSuccesses;

    /// <summary>
    ///     Gets or sets a value indicating whether successful job starts should be logged on not.
    /// </summary>
    [DefaultValue(StaticLogJobStarts)]
    public bool LogJobStarts { get; set; } = StaticLogJobStarts;
}
