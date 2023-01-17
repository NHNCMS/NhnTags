using NhnTags.Module.Tags.Extensions.Dtos;

namespace NhnTags.Module.Tags.Abstracts;

public interface ITagService
{
    Task<TagDto> GetTag(string tagId);

    Task<string> CreateTag(TagWithoutIdDto newTagWithoutId);

    Task<string> ReplaceTag(string tagId, TagWithoutIdDto tag);

    Task<string> UpdateTag(string tagId, TagPatchDto patchDto);

    Task<string> DeleteTag(string TagId);
}