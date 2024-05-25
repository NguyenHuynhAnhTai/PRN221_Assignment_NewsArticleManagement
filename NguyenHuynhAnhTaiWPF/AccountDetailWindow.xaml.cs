using BusinessObjects;
using BusinessObjects.Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AccountDetailWindow.xaml
    /// </summary>
    public partial class AccountDetailWindow : Window
    {
        private readonly ISystemAccountService iSystemAccountService;

        public AccountDetailWindow(ISystemAccountService iSystemAccountService)
        {
            InitializeComponent();
            this.iSystemAccountService = iSystemAccountService;
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string pattern = @"^[A-Za-z][\w\-\.]*@FUNewsManagement\.org$";
                Regex regex = new Regex(pattern);
                if (txtName.Text.Trim() == "" || txtEmail.Text.Trim() == "" || txtPassword.Password.Trim() == "")
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
                if (txtRole.Text.Trim().ToLower() == "staff")
                    updateAccount.AccountRole = 1;
                else
                    updateAccount.AccountRole = 2;
                updateAccount.AccountName = txtName.Text;
                updateAccount.AccountEmail = txtEmail.Text;
                updateAccount.AccountPassword = txtPassword.Password;

                iSystemAccountService.UpdateAccount(updateAccount);

                MessageBox.Show("Update successfully!", "Success",
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

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            short id;
            try
            {
                id = short.Parse(txtID.Text.Trim());
            }
            catch (Exception)
            {
                MessageBox.Show("ID must be a number!", "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                string pattern = @"^[A-Za-z][\w\-\.]*@FUNewsManagement\.org$";
                Regex regex = new Regex(pattern);
                if (txtName.Text.Trim() == "" || txtEmail.Text.Trim() == "" || txtPassword.Password.Trim() == "")
                {
                    MessageBox.Show("Please fill in all information", "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (iSystemAccountService.GetAccountById(id) is not null)
                {
                    MessageBox.Show("ID is already existed!", "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                SystemAccount addAccount = new SystemAccount();

                addAccount.AccountId = short.Parse(txtID.Text.Trim());
                addAccount.AccountName = txtName.Text;
                if (txtRole.Text.Trim().ToLower() == "staff")
                    addAccount.AccountRole = 1;
                else
                    addAccount.AccountRole = 2;
                addAccount.AccountEmail = txtEmail.Text;
                addAccount.AccountPassword = txtPassword.Password;

                iSystemAccountService.AddAccount(addAccount);

                MessageBox.Show("Create successfully!", "Success",
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

        private void Window_Activated(object sender, EventArgs e)
        {
            ResetInput();
            LoadData();
        }

        private void LoadData()
        {
            try {
                var account = StaticUserInformation.UserInfo;
                if (StaticWindowOptions.IsCreateMode)
                {
                    txtID.IsEnabled = true;
                    btnCreate.Visibility = Visibility.Visible;
                    btnUpdate.Visibility = Visibility.Collapsed;
                }
                else if (account is not null && StaticWindowOptions.IsEditMode)
                {
                    txtID.IsEnabled = false;
                    btnUpdate.Visibility = Visibility.Visible;
                    btnCreate.Visibility = Visibility.Collapsed;

                    txtID.Text = account.AccountId.ToString();
                    txtEmail.Text = account.AccountEmail;
                    txtName.Text = account.AccountName;
                    txtRole.Text = account.AccountRole == 1 ? "staff" : "lecturer";
                    txtPassword.Password = account.AccountPassword;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ResetInput()
        {
            txtID.Text = "";
            txtEmail.Text = "";
            txtName.Text = "";
            txtRole.Text = "";
            txtPassword.Password = "";
        }
    }
}
