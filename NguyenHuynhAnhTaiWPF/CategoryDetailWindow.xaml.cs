using BusinessObjects;
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
using static NguyenHuynhAnhTaiWPF.NewsArticleDetailWindow;

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                ResetInput();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                ResetInput();
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
            var category = StaticCategoryInformation.CategoryInfo;
            txtID.Text = category?.CategoryId.ToString();
            txtName.Text = category?.CategoryName;
            txtDescription.Text = category?.CategoryDesciption;
        }
        private void ResetInput()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtDescription.Text = "";
        }
    }
}
