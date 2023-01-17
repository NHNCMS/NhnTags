using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NhnTags.Filters;

public class SwaggerFilter:IDocumentFilter
{
    private readonly string _pathPrefix;

    public SwaggerFilter(string prefix)
    {
        this._pathPrefix = prefix;
    }

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var paths = swaggerDoc.Paths.Keys.ToList();
        foreach (var path in paths)
        {
            var pathToChange = swaggerDoc.Paths[path];
            swaggerDoc.Paths.Remove(path);
            swaggerDoc.Paths.Add($"{_pathPrefix}{path}", pathToChange);
        }
    }
}