using BusinessObjects;
using BusinessObjects.Entities;
using Services.Interfaces;
using System.Text.RegularExpressions;
using System.Windows;

namespace NguyenHuynhAnhTaiWPF
{
    /// <summary>
    /// Interaction logic for UpdateAccountWindow.xaml
    /// </summary>
    public partial class UpdateAccountWindow : Window
    {
        private SystemAccount? userAccount;
        private readonly ISystemAccountService iSystemAccountService;

        public UpdateAccountWindow(ISystemAccountService systemAccountService)
        {
            InitializeComponent();
            iSystemAccountService = systemAccountService;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string pattern = @"^[A-Za-z][\w\-\.]*@FUNewsManagement\.org$";
                Regex regex = new Regex(pattern);
                if (txtID.Text.Trim() == "" || txtName.Text.Trim() == "" || txtEmail.Text.Trim() == "" || txtPassword.Password.Trim() == "")
                {
                    MessageBox.Show("Please fill in all information", "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (!regex.IsMatch(txtEmail.Text))
                {
                    MessageBox.Show("Email is invalid!\n" +
                                    "Format email: Word + .... + @FUNewsManagement.org", "Warn",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                    return;
                }
                SystemAccount updateAccount = new SystemAccount();

                updateAccount.AccountId = short.Parse(txtID.Text);
                if (txtRole.Text == "staff")
                    updateAccount.AccountRole = 1;
                updateAccount.AccountName = txtName.Text;
                updateAccount.AccountEmail = txtEmail.Text;
                updateAccount.AccountPassword = txtPassword.Password;

                iSystemAccountService.UpdateAccount(updateAccount);

                MessageBox.Show("Update successfully!", "Success",
                                                   MessageBoxButton.OK,
                                                   MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                LoadData();
                this.Hide();
            }
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to close this window?", "Confirmation",
                                                                      MessageBoxButton.OKCancel,
                                                                      MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                this.Hide();
            }
            else return;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
