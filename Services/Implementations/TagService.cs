using BusinessObjects.Entities;
using Repositories.Interfaces;

namespace Services.Implementations
{
    public class TagService
    {
        private readonly ITagRepository iTagRepository;

        public TagService(ITagRepository tagRepository)
        {
            iTagRepository = tagRepository;
        }

        public List<Tag> GetTags()
        {
            return iTagRepository.GetTags();
        }
    }
}
