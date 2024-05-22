using BusinessObjects.Entities;
using Services.Implementations;
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
    /// Interaction logic for UpdateAccountWindow.xaml
    /// </summary>
    public partial class UpdateAccountWindow : Window
    {
        private readonly SystemAccount userAccount;
        private readonly ISystemAccountService iSystemAccountService;

        public UpdateAccountWindow(SystemAccount userAccount, ISystemAccountService systemAccountService)
        {
            InitializeComponent();
            this.userAccount = userAccount;
            iSystemAccountService = systemAccountService;
            LoadData();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
                throw new Exception(ex.Message);
            }
            finally
            {
                LoadData();
            }
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to close this window?", "Confirmation",
                                                                      MessageBoxButton.OKCancel,
                                                                      MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                this.Close();
            }
            else return;
        }
    }
}
