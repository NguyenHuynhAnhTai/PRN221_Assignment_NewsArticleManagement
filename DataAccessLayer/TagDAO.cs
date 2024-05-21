using BusinessObjects.Entities;

namespace DataAccessLayer
{
    public class TagDAO
    {
        public static List<Tag> GetTags()
        {
            var listTags = new List<Tag>();
            try
            {
                using var context = new FunewsManagementDbContext();
                listTags = context.Tags.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listTags;
        }
    }
}
