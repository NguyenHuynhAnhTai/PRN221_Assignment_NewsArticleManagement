using BusinessObjects;
using BusinessObjects.Entities;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using System.IO;
using System.Windows;

namespace NguyenHuynhAnhTaiWPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly ISystemAccountService iSystemAccountService;
        private readonly MainWindow mainWindow;
        private readonly AdminWindow adminWindow;
        private readonly NewsArticleManagementWindow newsArticleManagementWindow;

        public LoginWindow(ISystemAccountService systemAccountService, 
                            MainWindow mainWindow,
                            AdminWindow adminWindow,
                            NewsArticleManagementWindow newsArticleManagementWindow)
        {
            InitializeComponent();
            iSystemAccountService = systemAccountService;
            this.mainWindow = mainWindow;
            this.adminWindow = adminWindow;
            this.newsArticleManagementWindow = newsArticleManagementWindow;
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var adminInfo = GetAdminInfo();
                SystemAccount? systemAccount = iSystemAccountService.GetAccountByEmailAndPass(txtEmail.Text, txtPassword.Password);
                if (adminInfo.adminEmail.Equals(txtEmail.Text) && adminInfo.adminPassword.Equals(txtPassword.Password))
                {
                    this.Hide();
                    adminWindow.Show();
                    return;
                }
                if (systemAccount is not null)
                {
                    if (systemAccount.AccountRole == 1)
                    {
                        this.Hide();
                        StaticUserInformation.UserInfo = systemAccount;
                        mainWindow.Show();
                        return;
                    }
                }
                MessageBox.Show("Email or password is incorrect", "Warning",
                                 MessageBoxButton.OK,
                                 MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private (string adminEmail, string adminPassword) GetAdminInfo()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
            string adminEmail = configuration["admin:email"];
            string adminPassword = configuration["admin:password"];
            return (adminEmail, adminPassword);
        }

        private void btnNewsView_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            newsArticleManagementWindow.Show();
        }
    }
}
