using Microsoft.Extensions.DependencyInjection;
using NguyenHuynhAnhTaiWPF;
using Services.Implementations;
using Services.Interfaces;
using System.Windows;

namespace Exercise
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        ServiceProvider serviceProvider;

        public App()
        {
            //config for DependencyInjection(DI)
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton(typeof(INewsArticleService), typeof(NewsArticleService));
            services.AddSingleton(typeof(ICategoryService), typeof(CategoryService));
            services.AddSingleton(typeof(ISystemAccountService), typeof(SystemAccountService));
            services.AddSingleton(typeof(ITagService), typeof(TagService));
            services.AddSingleton<LoginWindow>();
            services.AddSingleton<MainWindow>();
            serviceProvider = serviceProvider ?? services.BuildServiceProvider(); // tao doi tuong cung cap dich vu
        }

        private void OnStartUp(Object sender, StartupEventArgs e)
        {
            var loginWindow = serviceProvider.GetService<LoginWindow>(); // Lấy dịch vụ, sử dụng
            loginWindow.Show(); //Hiển thị cửa sổ (main window khi start up)
        }
    }

}
