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

namespace NguyenHuynhAnhTaiWPF
{
    /// <summary>
    /// Interaction logic for CategoryManagementWindow.xaml
    /// </summary>
    public partial class CategoryManagementWindow : Window
    {
        private readonly ICategoryService iCategoryService;
        private readonly CategoryDetailWindow categoryDetailWindow;

        public CategoryManagementWindow(ICategoryService iCategoryService,
                                        CategoryDetailWindow categoryDetailWindow)
        {
            InitializeComponent();
            this.iCategoryService = iCategoryService;
            this.categoryDetailWindow = categoryDetailWindow;
        }

        private void dgCategoryList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var category = dgvCategoryList.SelectedItem as Category;
            txtID.Text = category?.CategoryId.ToString();
            txtName.Text = category?.CategoryName;
            txtDescription.Text = category?.CategoryDesciption;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            StaticWindowOptions.IsEditMode = false;
            StaticWindowOptions.IsCreateMode = true;
            categoryDetailWindow.ShowDialog();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var row = (Category)dgvCategoryList.SelectedItem;
            if (row is null)
            {
                MessageBox.Show("Please select a row to update!", "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            StaticCategoryInformation.CategoryInfo = row;
            StaticWindowOptions.IsEditMode = true;
            StaticWindowOptions.IsCreateMode = false;
            categoryDetailWindow.ShowDialog();
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

        private void Window_Activated(object sender, EventArgs e)
        {
            ResetInput();
            LoadData();
        }

        public void LoadData()
        {
            var categoryList = iCategoryService.GetCategories();
            dgvCategoryList.ItemsSource = categoryList;
        }

        public void ResetInput()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtDescription.Text = "";
        }
    }
}
