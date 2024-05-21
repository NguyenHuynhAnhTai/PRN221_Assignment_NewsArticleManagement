using BusinessObjects.Entities;
using DataAccessLayer;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        public List<Category> GetCategories() => CategoryDAO.GetCategories();
    }
}
