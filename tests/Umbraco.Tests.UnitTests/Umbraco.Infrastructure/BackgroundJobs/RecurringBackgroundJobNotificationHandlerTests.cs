// Copyright (c) Umbraco.
// See LICENSE for more details.

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Configuration;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Runtime;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Sync;
using Umbraco.Cms.Infrastructure.BackgroundJobs;
using Umbraco.Cms.Infrastructure.HostedServices;
using Umbraco.Cms.Infrastructure.Notifications;
using static Umbraco.Cms.Infrastructure.BackgroundJobs.NotificationHandlers;

namespace Umbraco.Cms.Tests.UnitTests.Umbraco.Infrastructure.BackgroundJobs;

[TestFixture]
public class RecurringBackgroundJobNotificationHandlerTests
{

    [Test]
    public async Task Does_Not_Log_Starting_Status_When_LogJobStarts_Config_is_false()
    {
        var mockJob = new Mock<IRecurringBackgroundJob>();
        var mockService = new Mock<IRecurringBackgroundJobStatusService>();
        var mockOptions = new Mock<IOptions<RecurringBackgroundJobStatusSettings>>();
        mockOptions.SetupGet(s => s.Value).Returns(new RecurringBackgroundJobStatusSettings() {
            LogJobStarts = false
        });


        var sut = new ExecutingNotificationHandler(mockService.Object, mockOptions.Object);
        await sut.HandleAsync(new RecurringBackgroundJobExecutingNotification(mockJob.Object, new EventMessages()), CancellationToken.None);

        mockService.Verify(x => x.CreateUpdateAsync(It.IsAny<IRecurringBackgroundJobStatus>()), Times.Never);
    }

    [Test]
    public async Task Logs_Starting_Status_When_LogJobStarts_Config_is_true()
    {
        var mockJob = new Mock<IRecurringBackgroundJob>();
        var mockService = new Mock<IRecurringBackgroundJobStatusService>();
        var mockOptions = new Mock<IOptions<RecurringBackgroundJobStatusSettings>>();
        mockOptions.SetupGet(s => s.Value).Returns(new RecurringBackgroundJobStatusSettings()
        {
            LogJobStarts = true
        });


        var sut = new ExecutingNotificationHandler(mockService.Object, mockOptions.Object);
        await sut.HandleAsync(new RecurringBackgroundJobExecutingNotification(mockJob.Object, new EventMessages()), CancellationToken.None);

        mockService.Verify(x => x.CreateUpdateAsync(It.IsAny<IRecurringBackgroundJobStatus>()), Times.Once);
    }

    [Test]
    public async Task Does_Not_Log_Success_Status_When_LogJobSuccesses_Config_is_false()
    {
        var mockJob = new Mock<IRecurringBackgroundJob>();
        var mockService = new Mock<IRecurringBackgroundJobStatusService>();
        var mockOptions = new Mock<IOptions<RecurringBackgroundJobStatusSettings>>();
        mockOptions.SetupGet(s => s.Value).Returns(new RecurringBackgroundJobStatusSettings()
        {
            LogJobSuccesses = false
        });


        var sut = new ExecutedNotificationHandler(mockService.Object, mockOptions.Object);
        await sut.HandleAsync(new RecurringBackgroundJobExecutedNotification(mockJob.Object, new EventMessages()), CancellationToken.None);

        mockService.Verify(x => x.CreateUpdateAsync(It.IsAny<IRecurringBackgroundJobStatus>()), Times.Never);
    }

    [Test]
    public async Task Logs_Success_Status_When_LogJobSuccesses_Config_is_true()
    {
        var mockJob = new Mock<IRecurringBackgroundJob>();
        var mockService = new Mock<IRecurringBackgroundJobStatusService>();
        var mockOptions = new Mock<IOptions<RecurringBackgroundJobStatusSettings>>();
        mockOptions.SetupGet(s => s.Value).Returns(new RecurringBackgroundJobStatusSettings()
        {
            LogJobSuccesses = true
        });


        var sut = new ExecutedNotificationHandler(mockService.Object, mockOptions.Object);
        await sut.HandleAsync(new RecurringBackgroundJobExecutedNotification(mockJob.Object, new EventMessages()), CancellationToken.None);

        mockService.Verify(x => x.CreateUpdateAsync(It.IsAny<IRecurringBackgroundJobStatus>()), Times.Once);
    }

    [Test]
    public async Task Does_Not_Log_Failure_Status_When_LogJobFailures_Config_is_false()
    {
        var mockJob = new Mock<IRecurringBackgroundJob>();
        var mockService = new Mock<IRecurringBackgroundJobStatusService>();
        var mockOptions = new Mock<IOptions<RecurringBackgroundJobStatusSettings>>();
        mockOptions.SetupGet(s => s.Value).Returns(new RecurringBackgroundJobStatusSettings()
        {
            LogJobFailures = false
        });


        var sut = new FailedNotificationHandler(mockService.Object, mockOptions.Object);
        await sut.HandleAsync(new RecurringBackgroundJobFailedNotification(mockJob.Object, new EventMessages()), CancellationToken.None);

        mockService.Verify(x => x.CreateUpdateAsync(It.IsAny<IRecurringBackgroundJobStatus>()), Times.Never);
    }

    [Test]
    public async Task Logs_Failure_Status_When_LogJobFailures_Config_is_true()
    {
        var mockJob = new Mock<IRecurringBackgroundJob>();
        var mockService = new Mock<IRecurringBackgroundJobStatusService>();
        var mockOptions = new Mock<IOptions<RecurringBackgroundJobStatusSettings>>();
        mockOptions.SetupGet(s => s.Value).Returns(new RecurringBackgroundJobStatusSettings()
        {
            LogJobFailures = true
        });


        var sut = new FailedNotificationHandler(mockService.Object, mockOptions.Object);
        await sut.HandleAsync(new RecurringBackgroundJobFailedNotification(mockJob.Object, new EventMessages()), CancellationToken.None);

        mockService.Verify(x => x.CreateUpdateAsync(It.IsAny<IRecurringBackgroundJobStatus>()), Times.Once);
    }

    [Test]
    public async Task Does_Not_Log_Failure_Status_When_Job_In_IgnoreList()
    {
        var mockJob = new Mock<IRecurringBackgroundJob>();
        var mockService = new Mock<IRecurringBackgroundJobStatusService>();
        var mockOptions = new Mock<IOptions<RecurringBackgroundJobStatusSettings>>();
        mockOptions.SetupGet(s => s.Value).Returns(new RecurringBackgroundJobStatusSettings()
        {
            LogJobFailures = true,
            IgnoredJobs = new List<string>() { mockJob.Object.GetType().FullName }
        });


        var sut = new FailedNotificationHandler(mockService.Object, mockOptions.Object);
        await sut.HandleAsync(new RecurringBackgroundJobFailedNotification(mockJob.Object, new EventMessages()), CancellationToken.None);

        mockService.Verify(x => x.CreateUpdateAsync(It.IsAny<IRecurringBackgroundJobStatus>()), Times.Never);
    }

    

}
