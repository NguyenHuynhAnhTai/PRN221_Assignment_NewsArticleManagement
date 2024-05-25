using BusinessObjects.Entities;
using Services.Interfaces;
using System.Windows;

namespace NguyenHuynhAnhTaiWPF
{
    /// <summary>
    /// Interaction logic for AddTagWindow.xaml
    /// </summary>
    public partial class AddTagWindow : Window
    {
        private readonly ITagService iTagService;

        public AddTagWindow(ITagService iTagService)
        {
            InitializeComponent();
            this.iTagService = iTagService;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var tagList= iTagService.GetTags();
                if (txtName.Text.Trim() == "" || txtNote.Text.Trim() == "")
                {
                    MessageBox.Show("Please fill in all information", "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (tagList.Exists(x => x.TagName == txtName.Text.Trim()))
                {
                    MessageBox.Show("Tag name already exists!", "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var addTag = new Tag();
                addTag.TagId = tagList.Count;
                addTag.TagName = txtName.Text;
                addTag.Note = txtNote.Text;

                iTagService.Add(addTag);

                MessageBox.Show("Add tag successfully!", "Success",
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
            MessageBoxResult result = MessageBox.Show("Are you sure you want to close this window?", "Close",
                                                                      MessageBoxButton.YesNo,
                                                                      MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                this.Hide();
            else
                return;
        }

        public void ResetInput()
        {
            txtName.Text = "";
            txtNote.Text = "";
        }
    }
}
