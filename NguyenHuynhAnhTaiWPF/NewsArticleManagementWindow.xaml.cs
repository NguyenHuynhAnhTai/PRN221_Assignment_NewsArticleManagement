using BusinessObjects;
using BusinessObjects.Entities;
using Services.Interfaces;
using System.Windows;
using static NguyenHuynhAnhTaiWPF.NewsArticleDetailWindow;

namespace NguyenHuynhAnhTaiWPF
{
    /// <summary>
    /// Interaction logic for NewsArticleManagementWindow.xaml
    /// </summary>
    public partial class NewsArticleManagementWindow : Window
    {
        private readonly INewsArticleService iNewsArticleService;

        private readonly ICategoryService iCategoryService;

        private readonly ITagService iTagService;

        private readonly NewsArticleDetailWindow newArticleWindow;

        private readonly AddTagWindow addTagWindow;

        public NewsArticleManagementWindow(INewsArticleService iNewsArticleService,
                                            ICategoryService iCategoryService,
                                            ITagService iTagService,
                                            NewsArticleDetailWindow newArticleWindow,
                                            AddTagWindow addTagWindow)
        {
            InitializeComponent();
            this.iNewsArticleService = iNewsArticleService;
            this.iCategoryService = iCategoryService;
            this.iTagService = iTagService;
            this.newArticleWindow = newArticleWindow;
            this.addTagWindow = addTagWindow;
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var row = (dynamic)dgvNewsArticleList.SelectedItem;
                if (row is null)
                {
                    MessageBox.Show("Please select a row to delete!", "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                StaticArticleInformation.ArticleInfo = iNewsArticleService.GetNewsArticleById(row.Id);

                if (StaticArticleInformation.ArticleInfo.NewsStatus == false)
                {
                    MessageBox.Show("This article had been deleted", "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this article?", "Delete",
                                                                                          MessageBoxButton.YesNo,
                                                                                          MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var removeArticle = StaticArticleInformation.ArticleInfo;
                    var inputedCategory = cboCategory.SelectedItem as Category;
                    var createdBy = StaticUserInformation.UserInfo;
                    var selectedTags = lboTag.Items.OfType<TagItem>().Where(t => t.IsSelected).Select(t => new Tag
                    {
                        TagId = t.TagId,
                        TagName = t.TagName,
                    }).ToList();

                    var article = new NewsArticle
                    {
                        NewsArticleId = row.Id,
                        NewsTitle = row.Title,
                        NewsContent = row.Content,
                        CategoryId = removeArticle.CategoryId,
                        NewsStatus = false,
                        CreatedById = createdBy?.AccountId,
                        CreatedDate = removeArticle?.CreatedDate,
                        ModifiedDate = DateTime.Now,
                    };

                    iNewsArticleService.UpdateNewsArticle(article);
                    LoadData();
                }
                else
                    return;
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
                var row = (dynamic)dgvNewsArticleList.SelectedItem;
                if (row is null)
                {
                    MessageBox.Show("Please select a row to update!", "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                StaticArticleInformation.ArticleInfo = iNewsArticleService.GetNewsArticleById(row.Id);
                StaticWindowOptions.IsEditMode = true;
                StaticWindowOptions.IsCreateMode = false;
                newArticleWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            StaticWindowOptions.IsEditMode = false;
            StaticWindowOptions.IsCreateMode = true;
            newArticleWindow.ShowDialog();
        }

        private void LoadCategoryList()
        {
            try
            {
                var cateList = iCategoryService.GetCategories();
                cboCategory.ItemsSource = cateList;
                cboCategory.DisplayMemberPath = "CategoryName";
                cboCategory.SelectedValuePath = "CategoryId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void LoadTagList()
        {
            try
            {
                var tagList = iTagService.GetTags();
                lboTag.ItemsSource = tagList;
                lboTag.SelectedValuePath = "TagId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void LoadNewsArticleList()
        {
            try
            {
                SystemAccount? account = StaticUserInformation.UserInfo;
                if (account is null)
                {
                    btnCreate.Visibility = Visibility.Collapsed;
                    btnUpdate.Visibility = Visibility.Collapsed;
                    btnDelete.Visibility = Visibility.Collapsed;
                }
                else if (account.AccountRole == 1)
                {
                    btnCreate.Visibility = Visibility.Visible;
                    btnUpdate.Visibility = Visibility.Visible;
                    btnDelete.Visibility = Visibility.Visible;
                }
                var newsArticleList = iNewsArticleService.GetNewsArticles();
                if (account is null) 
                    newsArticleList = newsArticleList.Where(a => a.NewsStatus == true).ToList();
                dgvNewsArticleList.ItemsSource = newsArticleList.Select(a => new
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
                ResetInput();
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
            cbActiveStatus.IsChecked = false;
            txtNewsArticleContent.Text = "";
            cboCategory.SelectedIndex = 0;
            cboCategory.Text = "";
            lboTag.SelectedIndex = 0;
            dgvNewsArticleList.SelectedItem = null;
            txtSearch.Text = "";
        }

        private void dgNewsArticleList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                var selectedRow = dgvNewsArticleList.SelectedItem;

                if (selectedRow is null) return;

                var row = (dynamic)selectedRow;

                txtID.Text = row.Id.ToString();
                txtNewsArticleTitle.Text = row.Title;

                if (row.NewsStatus == true)
                {
                    cbActiveStatus.IsChecked = true;
                }
                else
                {
                    cbActiveStatus.IsChecked = false;
                }

                txtNewsArticleContent.Text = row.Content;

                var article = iNewsArticleService.GetNewsArticleById(row.Id.Trim());
                if (article != null)
                {
                    cboCategory.SelectedValue = article.CategoryId;
                    lboTag.ItemsSource = article.Tags;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void LoadData()
        {
            SystemAccount? account = StaticUserInformation.UserInfo;
            if (account is null)
            {
                btnCreate.Visibility = Visibility.Collapsed;
                btnUpdate.Visibility = Visibility.Collapsed;
                btnDelete.Visibility = Visibility.Collapsed;
                btnAddTag.Visibility = Visibility.Collapsed;
            }
            else if (account.AccountRole == 1)
            {
                btnCreate.Visibility = Visibility.Visible;
                btnUpdate.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
                btnAddTag.Visibility = Visibility.Visible;
            }
            LoadCategoryList();
            LoadTagList();
            LoadNewsArticleList();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            ResetInput();
            LoadData();
        }

        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {
            addTagWindow.ShowDialog();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var search = txtSearch.Text;
                var newsArticleList = iNewsArticleService.GetNewsArticles()
                                                            .Where(a => a.NewsTitle != null
                                                                        && a.NewsTitle.Contains(search.Trim()));
                if (StaticUserInformation.UserInfo is null)
                    newsArticleList = newsArticleList.Where(a => a.NewsStatus == true);
                dgvNewsArticleList.ItemsSource = newsArticleList.Select(a => new
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
                ResetInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
