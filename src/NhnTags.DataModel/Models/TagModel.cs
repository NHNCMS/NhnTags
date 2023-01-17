using NhnTags.DataModel.Abstracts;
using NhnTags.Module.Tags.Extensions.Dtos;

namespace NhnTags.DataModel.Models;

public class TagModel : ModelBase
{
    protected TagModel()
    {
    }

    public string Name { get; private set; } = string.Empty;
    public string Type { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public static TagModel CreateTagModel(TagWithoutIdDto dto)
    {
        return new TagModel
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            Type = dto.Type,
            Description = dto.Description
        };
    }

    public static TagModel ReplaceTagModel(string id, TagWithoutIdDto dto)
    {
        return new TagModel
        {
            Id = id,
            Name = dto.Name,
            Type = dto.Type,
            Description = dto.Description
        };
    }

    public TagDto ToDto()
    {
        return new TagDto
        {
            Id = Id,
            Name = Name,
            Type = Type,
            Description = Description
        };
    }
}