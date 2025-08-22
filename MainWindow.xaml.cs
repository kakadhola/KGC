using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace KGC
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            // Set current date and time
            dpTestDate.SelectedDate = DateTime.Now;
            txtTestTime.Text = DateTime.Now.ToString("HH:mm:ss");

            // Initialize Party Names dropdown
            cmbPartyName.Items.Add("ABC Jewelers");
            cmbPartyName.Items.Add("XYZ Gold House");
            cmbPartyName.Items.Add("Premium Ornaments Ltd");
            cmbPartyName.Items.Add("Royal Jewelry Store");
            cmbPartyName.Items.Add("Diamond Palace");

            // Initialize Item Names dropdown
            cmbItemName.Items.Add("Gold Ring");
            cmbItemName.Items.Add("Gold Necklace");
            cmbItemName.Items.Add("Gold Bracelet");
            cmbItemName.Items.Add("Gold Earrings");
            cmbItemName.Items.Add("Silver Ring");
            cmbItemName.Items.Add("Silver Chain");
            cmbItemName.Items.Add("Mixed Metal Ornament");

            // Set default values for demonstration
            cmbPartyName.Text = "ABC Jewelers";
            cmbItemName.Text = "Gold Ring";
            txtMain_1.Text = "91.6";
            txtMain_2.Text = "15.25";
            txtMain_3.Text = "45000";
            txtRemarks.Text = "Sample test remarks for demonstration purposes.";

            // Set some sample metric values
            txtMetric1.Text = "12.50";
            txtMetric2.Text = "8.75";
            txtMetric3.Text = "3.20";
            txtMetric4.Text = "1.15";
            txtMetric5.Text = "0.85";
            txtMetric6.Text = "2.45";

            // Add event handlers for program type changes
            rbKrupaGold.Checked += ProgramType_Changed;
            rbFineGold.Checked += ProgramType_Changed;
            rbSilverOrnament.Checked += ProgramType_Changed;
            rbOtherMetal.Checked += ProgramType_Changed;
        }

        private void ProgramType_Changed(object sender, RoutedEventArgs e)
        {
            // Update UI based on selected program type
            var selectedRadioButton = sender as RadioButton;
            if (selectedRadioButton?.IsChecked == true)
            {
                UpdateUIForProgramType(selectedRadioButton.Name);
            }
        }

        private void UpdateUIForProgramType(string programType)
        {
            // Adjust UI elements based on program type
            switch (programType)
            {
                case "rbKrupaGold":
                    // Show all gold-related fields
                    break;
                case "rbFineGold":
                    // Adjust for fine gold testing
                    break;
                case "rbSilverOrnament":
                    // Adjust for silver testing
                    break;
                case "rbOtherMetal":
                    // Adjust for other metals
                    break;
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            // Clear all input fields
            cmbPartyName.Text = "";
            cmbItemName.Text = "";
            txtMain_1.Text = "";
            txtMain_2.Text = "";
            txtMain_3.Text = "";
            txtRemarks.Text = "";

            // Clear all metric fields
            txtMetric1.Text = "";
            txtMetric2.Text = "";
            txtMetric3.Text = "";
            txtMetric4.Text = "";
            txtMetric5.Text = "";
            txtMetric6.Text = "";
            txtMetric7.Text = "";
            txtMetric8.Text = "";
            txtMetric9.Text = "";
            txtMetric10.Text = "";
            txtMetric11.Text = "";
            txtMetric12.Text = "";

            // Reset to default program type
            rbKrupaGold.IsChecked = true;

            // Reset date and time
            dpTestDate.SelectedDate = DateTime.Now;
            txtTestTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(cmbPartyName.Text) || 
                string.IsNullOrWhiteSpace(cmbItemName.Text) ||
                string.IsNullOrWhiteSpace(txtMain_1.Text))
            {
                MessageBox.Show("Please fill in all required fields (Party Name, Item Name, and Purity).", 
                               "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Save logic here (could be to database, file, etc.)
            MessageBox.Show("Test data saved successfully!", "Save Successful", 
                           MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            // Validate required fields before printing
            if (string.IsNullOrWhiteSpace(cmbPartyName.Text) || 
                string.IsNullOrWhiteSpace(cmbItemName.Text) ||
                string.IsNullOrWhiteSpace(txtMain_1.Text))
            {
                MessageBox.Show("Please fill in all required fields before printing.", 
                               "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Create and populate test data object
            var testData = new TestData
            {
                PartyName = cmbPartyName.Text,
                ItemName = cmbItemName.Text,
                TestDate = dpTestDate.SelectedDate ?? DateTime.Now,
                TestTime = txtTestTime.Text,
                MainPurity = txtMain_1.Text,
                MainWeight = txtMain_2.Text,
                MainAmount = txtMain_3.Text,
                Remarks = txtRemarks.Text,
                ProgramType = GetSelectedProgramType(),
                Metrics = new Dictionary<string, string>
                {
                    {"Metric1", txtMetric1.Text},
                    {"Metric2", txtMetric2.Text},
                    {"Metric3", txtMetric3.Text},
                    {"Metric4", txtMetric4.Text},
                    {"Metric5", txtMetric5.Text},
                    {"Metric6", txtMetric6.Text},
                    {"Metric7", txtMetric7.Text},
                    {"Metric8", txtMetric8.Text},
                    {"Metric9", txtMetric9.Text},
                    {"Metric10", txtMetric10.Text},
                    {"Metric11", txtMetric11.Text},
                    {"Metric12", txtMetric12.Text}
                }
            };

            // Open print window
            var printWindow = new PrintWindow(testData);
            printWindow.Owner = this;
            printWindow.ShowDialog();
        }

        private string GetSelectedProgramType()
        {
            if (rbKrupaGold.IsChecked == true) return "Krupa Gold";
            if (rbFineGold.IsChecked == true) return "Fine Gold";
            if (rbSilverOrnament.IsChecked == true) return "Silver Ornament";
            if (rbOtherMetal.IsChecked == true) return "Other Metal";
            return "Krupa Gold"; // default
        }
    }

    // Data model for test information
    public class TestData
    {
        public string PartyName { get; set; } = "";
        public string ItemName { get; set; } = "";
        public DateTime TestDate { get; set; }
        public string TestTime { get; set; } = "";
        public string MainPurity { get; set; } = "";
        public string MainWeight { get; set; } = "";
        public string MainAmount { get; set; } = "";
        public string Remarks { get; set; } = "";
        public string ProgramType { get; set; } = "";
        public Dictionary<string, string> Metrics { get; set; } = new Dictionary<string, string>();
    }
}