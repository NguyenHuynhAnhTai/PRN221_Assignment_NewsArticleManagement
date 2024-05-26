using BusinessObjects;
using BusinessObjects.Entities;
using Services.Interfaces;
using System.Windows;

namespace NguyenHuynhAnhTaiWPF
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private readonly ISystemAccountService iSystemAccountService;
        private readonly AccountDetailWindow accountDetailWindow;
        private readonly ReportStatisticWindow reportStatisticWindow;

        public AdminWindow(ISystemAccountService iSystemAccountService, 
                            AccountDetailWindow accountDetailWindow,
                            ReportStatisticWindow reportStatisticWindow)
        {
            InitializeComponent();
            this.iSystemAccountService = iSystemAccountService;
            this.accountDetailWindow = accountDetailWindow;
            this.reportStatisticWindow = reportStatisticWindow;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var searchValue = txtSearch.Text;
                var accountList = iSystemAccountService.GetAccounts()
                                                        .Where(a => a.AccountName.Contains(searchValue.Trim()));
                dgvAccountList.ItemsSource = accountList.Select(a => new
                {
                    Id = a.AccountId,
                    Name = a.AccountName,
                    Email = a.AccountEmail,
                    Password = a.AccountPassword,
                    Role = a.AccountRole == 1 ? "staff" : "lecturer",
                }).ToList();
                ResetInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void dgAccountList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                var selectedAccount = dgvAccountList.SelectedItem;
                if (selectedAccount is not null)
                {
                    txtID.Text = selectedAccount.GetType().GetProperty("Id").GetValue(selectedAccount).ToString();
                    txtName.Text = selectedAccount.GetType().GetProperty("Name").GetValue(selectedAccount).ToString();
                    txtEmail.Text = selectedAccount.GetType().GetProperty("Email").GetValue(selectedAccount).ToString();
                    txtPassword.Password = selectedAccount.GetType().GetProperty("Password").GetValue(selectedAccount).ToString();
                    txtRole.Text = selectedAccount.GetType().GetProperty("Role").GetValue(selectedAccount).ToString();
                }
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
            accountDetailWindow.ShowDialog();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var row = (dynamic)dgvAccountList.SelectedItem;
                if (row is null)
                {
                    MessageBox.Show("Please select a row to update!", "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                StaticUserInformation.UserInfo = iSystemAccountService.GetAccountById(row.Id);
                StaticWindowOptions.IsEditMode = true;
                StaticWindowOptions.IsCreateMode = false;
                accountDetailWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var deleteAccount = dgvAccountList.SelectedItem;
                if (deleteAccount is null)
                {
                    MessageBox.Show("Please select an account to delete", "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                MessageBoxResult result = MessageBox.Show("Do you want to delete this account?", "Confirmation",
                                                                         MessageBoxButton.OKCancel,
                                                                         MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    SystemAccount systemAccount = new SystemAccount
                    {
                        AccountId = short.Parse(deleteAccount.GetType().GetProperty("Id").GetValue(deleteAccount).ToString()),
                        AccountName = deleteAccount.GetType().GetProperty("Name").GetValue(deleteAccount).ToString(),
                        AccountEmail = deleteAccount.GetType().GetProperty("Email").GetValue(deleteAccount).ToString(),
                        AccountPassword = deleteAccount.GetType().GetProperty("Password").GetValue(deleteAccount).ToString(),
                        AccountRole = deleteAccount.GetType().GetProperty("Role").GetValue(deleteAccount).ToString() == "staff" ? 1 : 2,
                    };
                    iSystemAccountService.DeleteAccount(systemAccount);
                    LoadData();
                }
                else
                    return;
                ResetInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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

        private void Window_Activated(object sender, EventArgs e)
        {
            ResetInput();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var accountList = iSystemAccountService.GetAccounts();
                dgvAccountList.ItemsSource = accountList.Select(a => new
                {
                    Id = a.AccountId,
                    Name = a.AccountName,
                    Email = a.AccountEmail,
                    Password = a.AccountPassword,
                    Role = a.AccountRole == 1 ? "staff" : "lecturer",
                }).ToList();
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
            txtPassword.Password = "";
            txtRole.Text = "";
            dgvAccountList.SelectedItem = null;
            txtSearch.Text = "";
        }

        private void btnReportStatistic_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            reportStatisticWindow.ShowDialog();
            if (reportStatisticWindow.IsActive == false)
                this.Show();
        }
    }
}
