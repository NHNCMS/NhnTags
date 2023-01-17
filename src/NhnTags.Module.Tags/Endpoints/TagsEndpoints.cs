using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NhnTags.Module.Shared.Extensions.Dtos;
using NhnTags.Module.Tags.Abstracts;
using NhnTags.Module.Tags.Extensions.Dtos;

namespace NhnTags.Module.Tags.Endpoints;

public static class TagsEndpoints
{
    public static async Task<IResult> HandleCreateTag(ITagService service,
        [FromBody] TagWithoutIdDto tag)
    {
        var createdTagId = await service.CreateTag(tag);
        return Results.Created($"/{createdTagId}", new IdDto(createdTagId));
    }

    public static async Task<IResult> HandleDeleteTag(ITagService service, [FromRoute] string id)
    {
        try
        {
            var idTag = await service.DeleteTag(id);
            return Results.Ok(new IdDto(idTag));
        }
        catch (Exception)
        {
            return Results.NotFound();
        }
    }

    public static async Task<IResult> HandleReplaceTag(ITagService service, [FromRoute] string id,
        [FromBody] TagWithoutIdDto tag)
    {
        try
        {
            var idAuthor = await service.ReplaceTag(id, tag);
            return Results.Ok(new IdDto(idAuthor));
        }
        catch (Exception)
        {
            return Results.NotFound();
        }
    }

    public static async Task<IResult> HandlePatchTag(ITagService service, [FromRoute] string id,
        [FromBody] TagPatchDto body)
    {
        try
        {
            var idAuthor = await service.UpdateTag(id, body);
            return Results.Ok(new IdDto(idAuthor));
        }
        catch
        {
            return Results.NotFound();
        }
    }

    public static async Task<IResult> HandleGetTag(ITagService service, [FromRoute] string id)
    {
        try
        {
            return Results.Ok(await service.GetTag(id));
        }
        catch (Exception)
        {
            return Results.NotFound();
        }
    }
}