using BusinessObjects.Entities;

namespace Services.Interfaces
{
    public interface INewsArticleService
    {
        void SaveProduct(NewsArticle p);
        void DeleteProduct(NewsArticle p);
        void UpdateProduct(NewsArticle p);
        List<NewsArticle> GetProducts();
        NewsArticle GetProductById(int id);
    }
}
