using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Api.Management.Factories;
using Umbraco.Cms.Api.Management.Mapping.Dictionary;
using Umbraco.Cms.Api.Management.Services;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Mapping;

namespace Umbraco.Cms.Api.Management.DependencyInjection;

internal static class BackgroundJobBuilderExtensions
{
    internal static IUmbracoBuilder AddBackgroundJobs(this IUmbracoBuilder builder)
    {
        builder.Services
            .AddTransient<IBackgroundJobPresentationFactory, BackgroundJobPresentationFactory>();

        return builder;
    }
}
