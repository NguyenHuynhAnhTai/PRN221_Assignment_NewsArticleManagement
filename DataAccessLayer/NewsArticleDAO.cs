using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class NewsArticleDAO
    {
        public static List<NewsArticle> GetProducts()
        {
            var listProducts = new List<NewsArticle>();
            try
            {
                using var db = new FunewsManagementDbContext();
                listProducts = db.NewsArticles.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listProducts;
        }

        public static void SaveProduct(NewsArticle p)
        {
            try
            {
                using var db = new FunewsManagementDbContext();
                db.NewsArticles.Add(p);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateProduct(NewsArticle p)
        {
            try
            {
                using var db = new FunewsManagementDbContext();
                db.Entry<NewsArticle>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteProduct(NewsArticle p)
        {
            try
            {
                using var db = new FunewsManagementDbContext();
                var p1 = db.NewsArticles.SingleOrDefault(x => x.NewsArticleId == p.NewsArticleId);
                db.NewsArticles.Remove(p1);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static NewsArticle GetProductById(int id)
        {
            using var db = new FunewsManagementDbContext();
            return db.NewsArticles.FirstOrDefault(x => x.NewsArticleId.Equals(id));
        }
    }
}
