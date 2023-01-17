namespace NhnTags.Module.Tags.Extensions.Dtos;

public record TagPatchDto
{
    public string? Name { get; init; }

    public string? Type { get; init; }

    public string? Description { get; init; }
}