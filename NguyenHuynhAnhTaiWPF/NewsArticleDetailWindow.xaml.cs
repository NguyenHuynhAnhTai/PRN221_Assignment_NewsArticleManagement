using BusinessObjects;
using BusinessObjects.Entities;
using Services.Interfaces;
using System.Windows;

namespace NguyenHuynhAnhTaiWPF
{
    /// <summary>
    /// Interaction logic for NewsArticleDetailWindow.xaml
    /// </summary>
    public partial class NewsArticleDetailWindow : Window
    {
        private readonly ICategoryService iCategoryService;
        private readonly ITagService iTagService;
        private readonly INewsArticleService iNewsArticleService;

        public NewsArticleDetailWindow(ICategoryService iCategoryService,
                                        ITagService iTagService,
                                        INewsArticleService iNewsArticleService)
        {
            InitializeComponent();
            this.iCategoryService = iCategoryService;
            this.iTagService = iTagService;
            this.iNewsArticleService = iNewsArticleService;
        }

        private void Window_Activated(object sender, EventArgs e)
        {

            if (StaticWindowOptions.IsCreateMode)
            {
                txtID.IsEnabled = true;
                btnCreate.Visibility = Visibility.Visible;
                btnUpdate.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtID.IsEnabled = false;
                btnUpdate.Visibility = Visibility.Visible;
                btnCreate.Visibility = Visibility.Collapsed;
            }
            ResetInput();
            LoadData();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtID.Text.Trim() == "" || txtNewsArticleTitle.Text.Trim() == "" || txtNewsArticleContent.Text.Trim() == "" || cboCategory.Text == "")
                {
                    MessageBox.Show("Please fill in all information", "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (iNewsArticleService.GetNewsArticleById(txtID.Text.Trim()) is not null)
                {
                    MessageBox.Show("News article ID already exists!", "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var inputedCategory = cboCategory.SelectedItem as Category;
                var createdBy = StaticUserInformation.UserInfo;
                var selectedTags = lboTag.Items.OfType<TagItem>().Where(t => t.IsSelected).Select(t => new Tag
                {
                    TagId = t.TagId,
                    TagName = t.TagName,
                }).ToList();

                var article = new NewsArticle
                {
                    NewsArticleId = txtID.Text,
                    NewsTitle = txtNewsArticleTitle.Text,
                    NewsContent = txtNewsArticleContent.Text,
                    NewsStatus = true,
                    CategoryId = inputedCategory?.CategoryId,
                    CreatedById = createdBy?.AccountId,
                    CreatedDate = DateTime.Now,
                };

                iNewsArticleService.SaveNewsArticle(article);
                MessageBox.Show("Create news article successfully!", "Success",
                                                       MessageBoxButton.OK,
                                                       MessageBoxImage.Information);
                this.Hide();
                ResetInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var updateArticle = StaticArticleInformation.ArticleInfo;
                var inputedCategory = cboCategory.SelectedItem as Category;
                var createdBy = StaticUserInformation.UserInfo;
                var selectedTags = lboTag.Items.OfType<TagItem>().Where(t => t.IsSelected).Select(t => new Tag
                {
                    TagId = t.TagId,
                    TagName = t.TagName,
                }).ToList();

                var article = new NewsArticle
                {
                    NewsArticleId = txtID.Text,
                    NewsTitle = txtNewsArticleTitle.Text,
                    NewsContent = txtNewsArticleContent.Text,
                    CategoryId = inputedCategory?.CategoryId,
                    NewsStatus = cbActiveStatus.IsChecked ?? true,
                    CreatedById = createdBy?.AccountId,
                    CreatedDate = updateArticle?.CreatedDate,
                    ModifiedDate = DateTime.Now,
                };

                iNewsArticleService.UpdateNewsArticle(article);
                MessageBox.Show("Update news article successfully!", "Success",
                                                       MessageBoxButton.OK,
                                                       MessageBoxImage.Information);
                this.Hide();
                ResetInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to close this window?", "Close",
                                                                      MessageBoxButton.YesNo,
                                                                      MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                this.Hide();
            else
                return;
        }

        private void LoadData()
        {
            try
            {
                NewsArticle? articleData = StaticArticleInformation.ArticleInfo;
                var tagList = iTagService.GetTags();
                var cateList = iCategoryService.GetCategories();

                lboTag.ItemsSource = tagList.Select(t => new TagItem
                {
                    TagId = t.TagId,
                    TagName = t.TagName,
                }).ToList();

                cboCategory.ItemsSource = cateList;
                cboCategory.DisplayMemberPath = "CategoryName";
                cboCategory.SelectedValuePath = "CategoryId";

                if (articleData is not null && StaticWindowOptions.IsEditMode)
                {
                    txtID.Text = articleData.NewsArticleId;
                    txtNewsArticleTitle.Text = articleData.NewsTitle;
                    txtNewsArticleContent.Text = articleData.NewsContent;
                    cbActiveStatus.IsChecked = articleData.NewsStatus;
                    cboCategory.SelectedValue = articleData.CategoryId;
                    foreach (var tag in articleData.Tags)
                    {
                        var tagItem = lboTag.Items.OfType<TagItem>().FirstOrDefault(t => t.TagId == tag.TagId);
                        if (tagItem is not null) tagItem.IsSelected = true;
                    }
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ResetInput()
        {
            txtID.Text = "";
            txtNewsArticleTitle.Text = "";
            txtNewsArticleContent.Text = "";
            cboCategory.SelectedIndex = 0;
            cboCategory.Text = "";
            lboTag.SelectedIndex = 0;
        }

        public class TagItem : Tag
        {
            public bool IsSelected { get; set; }
        }
    }
}
