﻿using Services.Interfaces;
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
    /// Interaction logic for ReportStatisticWindow.xaml
    /// </summary>
    public partial class ReportStatisticWindow : Window
    {
        private readonly INewsArticleService iNewsArticleService;

        public ReportStatisticWindow(INewsArticleService iNewsArticleService)
        {
            InitializeComponent();
            this.iNewsArticleService = iNewsArticleService;
        }

        private void btnCloseReport_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to close this window?", "Confirmation",
                                                                      MessageBoxButton.OKCancel,
                                                                      MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
                this.Hide();
            else
                return;
        }

        private void btnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime startDate = dpStartDate.SelectedDate ?? DateTime.MinValue;
                DateTime endDate = dpEndDate.SelectedDate ?? DateTime.MaxValue;

                if (endDate < startDate)
                {
                    MessageBox.Show("End date must be greater than start date", "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                } 
                
                var reportData = iNewsArticleService.GetNewsArticles()
                .Where(d => d.CreatedDate >= startDate && d.CreatedDate <= endDate)
                .OrderByDescending(d => d.CreatedDate)
                .Select(a => new
                {
                    Id = a.NewsArticleId,
                    Title = a.NewsTitle,
                    Category = a.Category?.CategoryName,
                    Tag = string.Join(", ", a.Tags.Select(t => t.TagName)),
                    a.NewsStatus,
                    Content = a.NewsContent,
                    a.CreatedDate,
                    CreatedBy = a.CreatedBy?.AccountName,
                    a.ModifiedDate,
                }).ToList();

                dgReportData.ItemsSource = reportData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
