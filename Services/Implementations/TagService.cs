using BusinessObjects.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations
{
    public class TagService
    {
        private readonly ITagService iTagService;

        public TagService(ITagService tagService)
        {
            iTagService = tagService;
        }

        public List<Tag> GetTags()
        {
            return iTagService.GetTags();
        }
    }
}
