using BusinessObjects.Entities;

namespace DataAccessLayer
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            var listCatagories = new List<Category>();
            try
            {
                using var context = new FunewsManagementDbContext();
                listCatagories = context.Categories.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listCatagories;
        }
    }
}
