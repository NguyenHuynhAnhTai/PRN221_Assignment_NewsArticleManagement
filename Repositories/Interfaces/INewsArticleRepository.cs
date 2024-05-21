using BusinessObjects.Entities;

namespace Repositories.Interfaces
{
    public interface INewsArticleRepository
    {
        void SaveProduct(NewsArticle p);
        void UpdateProduct(NewsArticle p);
        void DeleteProduct(NewsArticle p);
        List<NewsArticle> GetProducts();
        NewsArticle GetProductById(int id);
    }
}
