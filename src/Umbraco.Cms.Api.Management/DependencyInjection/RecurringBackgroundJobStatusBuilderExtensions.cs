using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Api.Management.Factories;
using Umbraco.Cms.Api.Management.Mapping.Dictionary;
using Umbraco.Cms.Api.Management.Mapping.RecurringBackgroundJobStatus;
using Umbraco.Cms.Api.Management.Services;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Mapping;

namespace Umbraco.Cms.Api.Management.DependencyInjection;

internal static class RecurringBackgroundJobStatusBuilderExtensions
{
    internal static IUmbracoBuilder AddRecurringBackgroundJobStatus(this IUmbracoBuilder builder)
    {
        builder.Services
            .AddTransient<IRecurringBackgroundJobStatusPresentationFactory, RecurringBackgroundJobStatusPresentationFactory>();

        builder.WithCollectionBuilder<MapDefinitionCollectionBuilder>().Add<RecurringBackgroundJobStatusMapDefinition>();

        return builder;
    }
}
