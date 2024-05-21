using BusinessObjects.Entities;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
