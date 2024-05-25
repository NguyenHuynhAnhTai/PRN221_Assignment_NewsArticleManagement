using BusinessObjects;
using BusinessObjects.Entities;
using Services.Interfaces;
using System.Windows;

namespace NguyenHuynhAnhTaiWPF
{
    /// <summary>
    /// Interaction logic for CategoryDetailWindow.xaml
    /// </summary>
    public partial class CategoryDetailWindow : Window
    {
        private readonly ICategoryService iCategoryService;

        public CategoryDetailWindow(ICategoryService iCategoryService)
        {
            InitializeComponent();
            this.iCategoryService = iCategoryService;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtName.Text.Trim() == "" || txtDescription.Text.Trim() == "")
                {
                    MessageBox.Show("Please fill in all information", "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                Category createCategory = new Category
                {
                    CategoryName = txtName.Text.Trim(),
                    CategoryDesciption = txtDescription.Text.Trim()
                };

                iCategoryService.Add(createCategory);

                MessageBox.Show("Add category successfully!", "Success",
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
            if (StaticWindowOptions.IsCreateMode)
            {
                btnCreate.Visibility = Visibility.Visible;
                btnUpdate.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnUpdate.Visibility = Visibility.Visible;
                btnCreate.Visibility = Visibility.Collapsed;
            }
            ResetInput();
            LoadData();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtName.Text.Trim() == "" || txtDescription.Text.Trim() == "")
                {
                    MessageBox.Show("Please fill in all information", "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var category = new Category
                {
                    CategoryId = short.Parse(txtID.Text.Trim()),
                    CategoryName = txtName.Text.Trim(),
                    CategoryDesciption = txtDescription.Text.Trim()
                };

                iCategoryService.Update(category);
                MessageBox.Show("Update category successfully!", "Success",
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
            MessageBoxResult result = MessageBox.Show("Do you want to close this window?", "Confirmation",
                                                                      MessageBoxButton.OKCancel,
                                                                      MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
                this.Hide();
            else
                return;
        }

        private void LoadData()
        {
            try
            {
                var category = StaticCategoryInformation.CategoryInfo;
                if (category is not null && StaticWindowOptions.IsEditMode)
                {
                    txtID.Text = category?.CategoryId.ToString();
                    txtName.Text = category?.CategoryName;
                    txtDescription.Text = category?.CategoryDesciption;
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
            txtName.Text = "";
            txtDescription.Text = "";
        }
    }
}
