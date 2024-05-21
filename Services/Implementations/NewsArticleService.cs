using BusinessObjects.Entities;
using Services.Interfaces;

namespace Services.Implementations
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly INewsArticleService iProductService;

        public NewsArticleService(INewsArticleService productService)
        {
            iProductService = productService;
        }

        public void DeleteProduct(NewsArticle p)
        {
            iProductService.DeleteProduct(p);
        }

        public NewsArticle GetProductById(int id)
        {
            return iProductService.GetProductById(id);
        }

        public List<NewsArticle> GetProducts()
        {
            return iProductService.GetProducts();
        }

        public void SaveProduct(NewsArticle p)
        {
            iProductService.SaveProduct(p);
        }

        public void UpdateProduct(NewsArticle p)
        {
            iProductService.UpdateProduct(p);
        }
    }
}
