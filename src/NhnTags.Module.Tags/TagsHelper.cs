using Microsoft.Extensions.DependencyInjection;
using NhnTags.Module.Tags.Abstracts;
using NhnTags.Module.Tags.Concretes;

namespace NhnTags.Module.Tags;

public static class TagsHelper
{
    public static IServiceCollection AddTagServices(this IServiceCollection services)
    {
        services.AddScoped<ITagService, TagService>();

        return services;
    }
}