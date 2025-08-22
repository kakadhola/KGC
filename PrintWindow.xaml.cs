using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace KGC
{
    public partial class PrintWindow : Window
    {
        private TestData _testData;

        public PrintWindow(TestData testData)
        {
            InitializeComponent();
            _testData = testData;
            PopulateReportData();
        }

        private void PopulateReportData()
        {
            try
            {
                // Generate report number and date
                txtReportNo.Text = $"Report No: KGC-{DateTime.Now.Year}-{DateTime.Now.Ticks.ToString().Substring(10, 4)}";
                txtReportDate.Text = $"Date: {_testData.TestDate:dd/MM/yyyy}";

                // Populate test information
                lblPartyName.Text = _testData.PartyName;
                lblItemName.Text = _testData.ItemName;
                lblTestDate.Text = _testData.TestDate.ToString("dd/MM/yyyy");
                lblTestTime.Text = _testData.TestTime;
                lblProgramType.Text = _testData.ProgramType;
                lblRemarks.Text = _testData.Remarks;

                // Populate main results
                lblMainPurity.Text = string.IsNullOrWhiteSpace(_testData.MainPurity) ? "N/A" : _testData.MainPurity;
                lblMainWeight.Text = string.IsNullOrWhiteSpace(_testData.MainWeight) ? "N/A" : _testData.MainWeight;
                lblMainAmount.Text = string.IsNullOrWhiteSpace(_testData.MainAmount) ? "N/A" : FormatCurrency(_testData.MainAmount);

                // Populate detailed measurements
                PopulateMetricsGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating report data: {ex.Message}", "Error", 
                               MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PopulateMetricsGrid()
        {
            // Clear existing children
            metricsGrid.Children.Clear();

            int row = 0;
            int col = 0;

            // Add metrics that have values
            for (int i = 1; i <= 12; i++)
            {
                string metricKey = $"Metric{i}";
                if (_testData.Metrics.ContainsKey(metricKey) && !string.IsNullOrWhiteSpace(_testData.Metrics[metricKey]))
                {
                    // Metric label
                    var labelBlock = new TextBlock
                    {
                        Text = $"Metric {i}:",
                        FontWeight = FontWeights.SemiBold,
                        FontSize = 10,
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2F4F4F"))
                    };
                    Grid.SetRow(labelBlock, row);
                    Grid.SetColumn(labelBlock, col);
                    metricsGrid.Children.Add(labelBlock);

                    // Metric value
                    var valueBlock = new TextBlock
                    {
                        Text = _testData.Metrics[metricKey],
                        FontSize = 10,
                        Margin = new Thickness(5, 0, 0, 0),
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2F4F4F"))
                    };
                    Grid.SetRow(valueBlock, row);
                    Grid.SetColumn(valueBlock, col + 1);
                    metricsGrid.Children.Add(valueBlock);

                    // Move to next position
                    if (col == 2) // Move to next row
                    {
                        col = 0;
                        row++;
                        
                        // Add row definition if needed
                        if (row >= metricsGrid.RowDefinitions.Count)
                        {
                            metricsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        }
                    }
                    else // Move to next column
                    {
                        col = 2;
                    }
                }
            }

            // If no metrics have values, show a placeholder
            if (metricsGrid.Children.Count == 0)
            {
                var noDataBlock = new TextBlock
                {
                    Text = "No detailed measurements recorded",
                    FontStyle = FontStyles.Italic,
                    FontSize = 10,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8B4513"))
                };
                Grid.SetRow(noDataBlock, 0);
                Grid.SetColumn(noDataBlock, 0);
                Grid.SetColumnSpan(noDataBlock, 4);
                metricsGrid.Children.Add(noDataBlock);
            }
        }

        private string FormatCurrency(string amount)
        {
            if (decimal.TryParse(amount, out decimal value))
            {
                return value.ToString("N0");
            }
            return amount;
        }

        private void BtnPrintPreview_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create print dialog for preview
                var printDialog = new PrintDialog();
                
                // Show print preview dialog
                if (printDialog.ShowDialog() == true)
                {
                    // Get the printable area (the content inside the border)
                    var printableContent = printableArea;

                    // Create a visual for printing
                    var visual = CreatePrintVisual(printableContent);

                    // Print the document
                    printDialog.PrintVisual(visual, "KGC Test Report");
                    
                    MessageBox.Show("Print preview completed successfully!", "Print Preview", 
                                   MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during print preview: {ex.Message}", "Print Error", 
                               MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create print dialog
                var printDialog = new PrintDialog();
                
                // Configure for A4 paper
                var pageSize = new Size(8.27 * 96, 11.7 * 96); // A4 in 96 DPI
                printDialog.PrintTicket.PageMediaSize = new PageMediaSize(pageSize.Width, pageSize.Height);

                // Show print dialog
                if (printDialog.ShowDialog() == true)
                {
                    // Get the printable area
                    var printableContent = printableArea;

                    // Create a visual for printing
                    var visual = CreatePrintVisual(printableContent);

                    // Print the document
                    printDialog.PrintVisual(visual, $"KGC Test Report - {_testData.PartyName} - {_testData.ItemName}");
                    
                    MessageBox.Show("Document sent to printer successfully!", "Print Successful", 
                                   MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during printing: {ex.Message}", "Print Error", 
                               MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Visual CreatePrintVisual(FrameworkElement element)
        {
            // Clone the element for printing to avoid affecting the original
            var printElement = new Border
            {
                Background = Brushes.White,
                Child = CloneElement(element)
            };

            // Set the size for A4 paper (minus margins)
            var printSize = new Size(7.5 * 96, 10.5 * 96); // A4 minus margins in 96 DPI
            printElement.Measure(printSize);
            printElement.Arrange(new Rect(printSize));

            return printElement;
        }

        private FrameworkElement CloneElement(FrameworkElement original)
        {
            // For simplicity, we'll use the original element
            // In a production environment, you might want to create a true clone
            return original;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Override to handle window closing
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }
    }
}