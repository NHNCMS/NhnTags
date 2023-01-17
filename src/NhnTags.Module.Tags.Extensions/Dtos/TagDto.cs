namespace NhnTags.Module.Tags.Extensions.Dtos;

public record TagDto
{
    public string Id { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public string Type { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;
}