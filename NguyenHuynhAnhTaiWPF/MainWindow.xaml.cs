using BusinessObjects;
using BusinessObjects.Entities;
using Services.Interfaces;
using System.Windows;

namespace NguyenHuynhAnhTaiWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SystemAccount? userAccount;

        private readonly ISystemAccountService iSystemAccountService;
        private readonly UpdateAccountWindow updateAccountWindow;
        private readonly NewsArticleManagementWindow newsArticleManagementWindow;
        private readonly CategoryManagementWindow categoryManagementWindow;
        private readonly HistoryWindow historyWindow;

        public MainWindow(ISystemAccountService systemAccountService,
                            UpdateAccountWindow updateAccountWindow,
                            NewsArticleManagementWindow newsArticleManagementWindow,
                            CategoryManagementWindow categoryManagementWindow,
                            HistoryWindow historyWindow)
        {
            InitializeComponent();
            iSystemAccountService = systemAccountService;
            this.updateAccountWindow = updateAccountWindow;
            this.newsArticleManagementWindow = newsArticleManagementWindow;
            this.categoryManagementWindow = categoryManagementWindow;
            this.historyWindow = historyWindow;
        }

        private void LoadData()
        {
            userAccount = StaticUserInformation.UserInfo;
            if (userAccount is not null)
            {
                var account = iSystemAccountService.GetAccountById(userAccount.AccountId);
                txtID.Text = account.AccountId.ToString();
                if (account.AccountRole == 1)
                    txtRole.Text = "staff";
                txtEmail.Text = account.AccountEmail;
                txtName.Text = account.AccountName;
                txtPassword.Password = account.AccountPassword;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            updateAccountWindow.ShowDialog();
        }

        private void btnNewsArticleManagement_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            newsArticleManagementWindow.ShowDialog();
            if (newsArticleManagementWindow.IsActive == false)
                this.Show();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to close this window?", "Confirmation",
                                                                      MessageBoxButton.OKCancel,
                                                                      MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
                this.Close();
            else
                return;
        }

        private void btnCategoryManagement_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            categoryManagementWindow.ShowDialog();
            if (categoryManagementWindow.IsActive == false)
                this.Show();
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            StaticUserInformation.UserInfo = userAccount;
            this.Hide();
            historyWindow.ShowDialog();
            if (historyWindow.IsActive == false)
                this.Show();
        }
    }
}