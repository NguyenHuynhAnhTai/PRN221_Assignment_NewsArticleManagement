using BusinessObjects.Entities;
using DataAccessLayer;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class TagRepository : ITagRepository
    {
        public List<Tag> GetTags() => TagDAO.GetTags(); 
    }
}
