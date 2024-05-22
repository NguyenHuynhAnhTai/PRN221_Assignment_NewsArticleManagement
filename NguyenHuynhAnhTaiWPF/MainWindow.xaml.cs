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
        private readonly SystemAccount userAccount;

        private readonly ISystemAccountService iSystemAccountService;

        public MainWindow(SystemAccount systemAccount, ISystemAccountService systemAccountService)
        {
            InitializeComponent();
            userAccount = systemAccount;
            iSystemAccountService = systemAccountService;

            LoadData();
        }

        private void LoadData()
        {   
            var account = iSystemAccountService.GetAccountById(userAccount.AccountId);
            txtID.Text = account.AccountId.ToString();
            if (account.AccountRole == 1)
                txtRole.Text = "staff";
            txtEmail.Text = account.AccountEmail;
            txtName.Text = account.AccountName;
            txtPassword.Password = account.AccountPassword;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            new UpdateAccountWindow(userAccount, iSystemAccountService).Show();
        }
    }
}