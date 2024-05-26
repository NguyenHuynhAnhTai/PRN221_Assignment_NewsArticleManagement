using BusinessObjects.Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NguyenHuynhAnhTaiWPF
{
    /// <summary>
    /// Interaction logic for HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        private readonly INewsArticleService iNewsArticleService;

        public HistoryWindow(INewsArticleService iNewsArticleService)
        {
            InitializeComponent();
            this.iNewsArticleService = iNewsArticleService;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to close this window?", "Confirmation",
                                                                      MessageBoxButton.OKCancel,
                                                                      MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
                this.Hide();
            else
                return;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var newsArticlesHistory = iNewsArticleService.GetNewsArticles().Select(a => new
                {
                    Id = a.NewsArticleId,
                    Title = a.NewsTitle,
                    Category = a.Category?.CategoryName,
                    Tag = string.Join(", ", a.Tags.Select(t => t.TagName)),
                    a.NewsStatus,
                    Content = a.NewsContent,
                    a.CreatedDate,
                    CreatedBy = a.CreatedBy?.AccountName,
                    a.ModifiedDate,
                }).ToList();

                dgvNewsHistory.ItemsSource = newsArticlesHistory;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
