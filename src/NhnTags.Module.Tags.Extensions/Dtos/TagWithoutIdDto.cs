namespace NhnTags.Module.Tags.Extensions.Dtos;

public record TagWithoutIdDto
{
    public string Name { get; init; } = string.Empty;

    public string Type { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;
}