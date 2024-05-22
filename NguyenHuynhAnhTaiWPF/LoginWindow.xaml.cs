using BusinessObjects.Entities;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using System.IO;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace NguyenHuynhAnhTaiWPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        protected readonly ISystemAccountService iSystemAccountService;

        public LoginWindow(ISystemAccountService systemAccountService)
        {
            InitializeComponent();
            iSystemAccountService = systemAccountService;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
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
            var adminInfo = GetAdminInfo();
            SystemAccount? systemAccount = iSystemAccountService.GetAccountByEmailAndPass(txtEmail.Text, txtPassword.Password);
            if (adminInfo.adminEmail.Equals(txtEmail.Text) && adminInfo.adminPassword.Equals(txtPassword.Password))
            {
                this.Hide();
                new AdminWindow().Show();
                return;
            }
            if (systemAccount is not null)
            {
                if (systemAccount.AccountRole == 1)
                {
                    var mainWindow = new MainWindow(systemAccount, iSystemAccountService);
                    this.Hide();
                    mainWindow.Show();
                    return;
                }
                MessageBox.Show("You do not have permission!", "Warning",
                                                   MessageBoxButton.OK,
                                                   MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show("Email or password is incorrect", "Warning",
                             MessageBoxButton.OK,
                             MessageBoxImage.Error);
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

        private bool CheckLogin(string email, string password)
        {
            SystemAccount? systemAccount = iSystemAccountService.GetAccountByEmailAndPass(email, password);
            if (systemAccount != null)
            {
                return true;
            }
            return false;
        }
    }
}
