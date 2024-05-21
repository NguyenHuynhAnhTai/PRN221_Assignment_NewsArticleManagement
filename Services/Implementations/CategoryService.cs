using BusinessObjects.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository iCategoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            iCategoryRepository = categoryRepository;
        }

        public List<Category> GetCategories() 
        {
            return iCategoryRepository.GetCategories(); 
        }
    }
}
