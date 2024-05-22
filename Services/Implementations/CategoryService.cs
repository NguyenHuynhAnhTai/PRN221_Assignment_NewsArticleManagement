using BusinessObjects.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryService iCategoryService;

        public CategoryService(ICategoryService categoryService)
        {
            iCategoryService = categoryService;
        }

        public List<Category> GetCategories() 
        {
            return iCategoryService.GetCategories(); 
        }
    }
}
