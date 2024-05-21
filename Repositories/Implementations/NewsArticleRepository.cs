using BusinessObjects.Entities;
using DataAccessLayer;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        public void DeleteProduct(NewsArticle p) => NewsArticleDAO.DeleteProduct(p);
        public void SaveProduct(NewsArticle p) => NewsArticleDAO.SaveProduct(p);
        public void UpdateProduct(NewsArticle p) => NewsArticleDAO.UpdateProduct(p);
        public List<NewsArticle> GetProducts() => NewsArticleDAO.GetProducts();
        public NewsArticle GetProductById(int id) => NewsArticleDAO.GetProductById(id);
    }
}
