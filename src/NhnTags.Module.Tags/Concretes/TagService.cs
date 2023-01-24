using NhnTags.Broker.Kafka;
using NhnTags.Broker.Kafka.Messages;
using NhnTags.DataModel.Abstracts;
using NhnTags.DataModel.Models;
using NhnTags.Module.Tags.Abstracts;
using NhnTags.Module.Tags.Extensions.Dtos;

namespace NhnTags.Module.Tags.Concretes;

internal sealed class TagService : ITagService
{
    private readonly IPersister<TagModel> _persister;

    private readonly IServiceBus _bus;

    public TagService(IPersister<TagModel> persister, IServiceBus bus)
    {
        _persister = persister;
        _bus = bus;
    }

    public async Task<TagDto> GetTag(string tagId)
    {
        var tagModel = await _persister.GetById(tagId);
        return string.IsNullOrWhiteSpace(tagModel.Id) ? throw new Exception() : tagModel.ToDto();
    }

    public async Task<string> CreateTag(TagWithoutIdDto newTagWithoutId)
    {
        var tagModel = TagModel.CreateTagModel(newTagWithoutId);
        await _persister.Insert(tagModel);
        var tagSubmitted = new TagSubmitted(Guid.NewGuid(), Guid.NewGuid(), tagModel.Id, tagModel.Description);
        await _bus.Publish(tagSubmitted, CancellationToken.None);
        return tagModel.Id;
    }

    public async Task<string> ReplaceTag(string tagId, TagWithoutIdDto tag)
    {
        var tagModel = TagModel.ReplaceTagModel(tagId, tag);
        await _persister.Replace(tagModel);

        return tagModel.Id;
    }

    public async Task<string> UpdateTag(string tagId, TagPatchDto patchDto)
    {
        var propsToUpdate = patchDto.GetType().GetProperties()
            .ToDictionary(pi => pi.Name, pi => pi.GetValue(patchDto))
            .Where(pi => pi.Value != null)
            .ToDictionary(el => el.Key, el => el.Value!);

        await _persister.UpdateOne(tagId, propsToUpdate);

        return tagId;
    }

    public async Task<string> DeleteTag(string tagId)
    {
        await _persister.Delete(tagId);

        return tagId;
    }
}