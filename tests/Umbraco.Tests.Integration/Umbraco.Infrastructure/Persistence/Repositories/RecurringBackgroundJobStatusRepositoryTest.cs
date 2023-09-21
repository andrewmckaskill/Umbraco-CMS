// Copyright (c) Umbraco.
// See LICENSE for more details.

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Persistence.Repositories;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Tests.Common.Testing;
using Umbraco.Cms.Tests.Integration.Testing;

namespace Umbraco.Cms.Tests.Integration.Umbraco.Infrastructure.Persistence.Repositories;

[TestFixture]
[UmbracoTest(Database = UmbracoTestOptions.Database.NewSchemaPerTest)]
public class RecurringBackgroundJobStatusRepositoryTest : UmbracoIntegrationTest
{
    [SetUp]
    public async Task SetUp() => await CreateTestData();

    private IRecurringBackgroundJobStatusRepository CreateRepository() => GetRequiredService<IRecurringBackgroundJobStatusRepository>();

    [Test]
    public async Task Can_Perform_Get_By_Name_On_RecurringBackgroundJobStatusRepository()
    {
        // Arrange
        var provider = ScopeProvider;
        using (provider.CreateScope())
        {
            var repository = CreateRepository();
            var lastExecutionDateTime = DateTime.Now;
            var job = (IRecurringBackgroundJobStatus)new RecurringBackgroundJobStatus("Umbraco.Infrastructure.Jobs.ContentVersionCleanupJob")
            {
                LastExecutionDateTime = lastExecutionDateTime,
                LastExecutionWasSuccessful = true,
            };

            repository.Save(job);

            // re-get
            job = repository.GetByName("Umbraco.Infrastructure.Jobs.ContentVersionCleanupJob");

            // Assert
            Assert.That(job, Is.Not.Null);
            Assert.That(job.Name, Is.EqualTo("Umbraco.Infrastructure.Jobs.ContentVersionCleanupJob"));
            Assert.That(job.LastExecutionDateTime, Is.EqualTo(lastExecutionDateTime));
            Assert.That(job.LastExecutionWasSuccessful, Is.True);
        }
    }

    [Test]
    public async Task Can_Perform_Get_On_RecurringBackgroundJobStatusRepository()
    {
        // Arrange
        var provider = ScopeProvider;
        using (provider.CreateScope())
        {
            var repository = CreateRepository();
            var lastExecutionDateTime = DateTime.Now;
            var job = (IRecurringBackgroundJobStatus)new RecurringBackgroundJobStatus("Umbraco.Infrastructure.Jobs.ContentVersionCleanupJob")
            {
                LastExecutionDateTime = lastExecutionDateTime,
                LastExecutionWasSuccessful = true
            };

            repository.Save(job);

            // re-get
            job = repository.Get(job.Id);

            // Assert
            Assert.That(job, Is.Not.Null);
            Assert.That(job.Name, Is.EqualTo("Umbraco.Infrastructure.Jobs.ContentVersionCleanupJob"));
            Assert.That(job.LastExecutionDateTime, Is.EqualTo(lastExecutionDateTime));
            Assert.That(job.LastExecutionWasSuccessful, Is.True);
        }
    }

    

    [Test]
    public void Can_Perform_Get_On_RecurringBackgroundJobStatusRepository_When_No_LastExecutionDate_Assigned()
    {
        // Arrange
        var provider = ScopeProvider;
        using (provider.CreateScope())
        {
            var repository = CreateRepository();
            var job = (IRecurringBackgroundJobStatus)new RecurringBackgroundJobStatus("Umbraco.Infrastructure.Jobs.ContentVersionCleanupJob");

            repository.Save(job);

            // re-get
            job = repository.Get(job.Id);

            // Assert
            Assert.That(job, Is.Not.Null);
            Assert.That(job.Name, Is.EqualTo("Umbraco.Infrastructure.Jobs.ContentVersionCleanupJob"));
            Assert.That(job.LastExecutionDateTime, Is.Null);
            Assert.That(job.LastExecutionWasSuccessful, Is.Null);
        }
    }

    [Test]
    public void Can_Perform_GetMany_On_RecurringBackgroundJobStatusRepository()
    {
        // Arrange
        var provider = ScopeProvider;
        using (provider.CreateScope())
        {
            var repository = CreateRepository();

            // Act
            var jobs = repository.GetMany(1,2).ToArray();

            // Assert
            Assert.That(jobs, Is.Not.Null);
            Assert.That(jobs.Any(), Is.True);
            Assert.That(jobs.Any(x => x == null), Is.False);
            Assert.That(jobs.Count(), Is.EqualTo(2));
        }
    }

    [Test]
    public void Can_Perform_GetAll_On_RecurringBackgroundJobStatusRepository()
    {
        // Arrange
        var provider = ScopeProvider;
        using (provider.CreateScope())
        {
            var repository = CreateRepository();

            // Act
            var dictionaryItems = repository.GetAll().ToArray();

            // Assert
            Assert.That(dictionaryItems, Is.Not.Null);
            Assert.That(dictionaryItems.Any(), Is.True);
            Assert.That(dictionaryItems.Any(x => x == null), Is.False);
            Assert.That(dictionaryItems.Count(), Is.EqualTo(2));
        }
    }

    

    [Test]
    public void Can_Perform_GetByQuery_On_RecurringBackgroundJobStatusRepository()
    {
        // Arrange
        var provider = ScopeProvider;
        using (provider.CreateScope())
        {
            var repository = CreateRepository();

            // Act
            var query = provider.CreateQuery<IRecurringBackgroundJobStatus>().Where(x => x.LastExecutionWasSuccessful == false);
            var result = repository.Get(query).ToArray();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Any(), Is.True);
            Assert.That(result.FirstOrDefault().LastExecutionWasSuccessful, Is.False);
        }
    }

    [Test]
    public void Can_Perform_Count_On_RecurringBackgroundJobStatusRepository()
    {
        // Arrange
        var provider = ScopeProvider;
        using (provider.CreateScope())
        {
            var repository = CreateRepository();

            // Act
            var query = provider.CreateQuery<IRecurringBackgroundJobStatus>().Where(x => x.LastExecutionWasSuccessful == false);
            var result = repository.Count(query);

            // Assert
            Assert.That(result, Is.EqualTo(1));
        }
    }

    [Test]
    public void Can_Perform_Add_On_RecurringBackgroundJobStatusRepository()
    {
        // Arrange
        var provider = ScopeProvider;
        using (provider.CreateScope())
        {
            var repository = CreateRepository();
            var job = new RecurringBackgroundJobStatus("Umbraco.Infrastructure.Jobs.TestJobName");

            // Act
            repository.Save(job);

            var exists = repository.Exists(job.Id);

            // Assert
            Assert.That(job.HasIdentity, Is.True);
            Assert.That(exists, Is.True);
        }
    }

    [Test]
    public void Can_Perform_Update_On_RecurringBackgroundJobStatusRepository()
    {
        // Arrange
        var provider = ScopeProvider;
        using (provider.CreateScope())
        {
            var repository = CreateRepository();
            var lastExecutionDateTime = DateTime.Now;

            // Act
            var item = repository.Get(1);
            item.LastExecutionWasSuccessful = true;
            item.LastExecutionDateTime = lastExecutionDateTime;

            repository.Save(item);

            var job = repository.Get(1);

            // Assert
            Assert.That(job, Is.Not.Null);
            Assert.That(job.LastExecutionDateTime, Is.EqualTo(lastExecutionDateTime));
            Assert.That(job.LastExecutionWasSuccessful, Is.True);
        }
    }

   

    [Test]
    public void Can_Perform_Delete_On_RecurringBackgroundJobStatusRepository()
    {
        // Arrange
        var provider = ScopeProvider;
        using (provider.CreateScope())
        {
            var repository = CreateRepository();

            // Act
            var item = repository.Get(1);
            repository.Delete(item);

            var exists = repository.Exists(1);

            // Assert
            Assert.That(exists, Is.False);
        }
    }

    [Test]
    public void Can_Perform_Exists_On_RecurringBackgroundJobStatusRepository()
    {
        // Arrange
        var provider = ScopeProvider;
        using (provider.CreateScope())
        {
            var repository = CreateRepository();

            // Act
            var exists = repository.Exists(1);

            // Assert
            Assert.That(exists, Is.True);
        }
    }

    

    public async Task CreateTestData()
    {
        var recurringBackgroundJobStatusService = GetRequiredService<IRecurringBackgroundJobStatusService>();
       

       

        await recurringBackgroundJobStatusService.CreateUpdateAsync(
            new RecurringBackgroundJobStatus("Umbraco.Infrastructure.Jobs.TestJob1")
            {
                LastExecutionWasSuccessful = false,
                LastExecutionDateTime = DateTime.Now
            }
           );

        await recurringBackgroundJobStatusService.CreateUpdateAsync(
             new RecurringBackgroundJobStatus("Umbraco.Infrastructure.Jobs.TestJob2")
             {
                 LastExecutionWasSuccessful = true,
                 LastExecutionDateTime = DateTime.Now
             }
            );
    }
}
