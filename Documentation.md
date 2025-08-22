# KGC - Krupa Gold Company Testing Application

## Overview
A comprehensive WPF application for professional gold and jewelry testing with advanced print functionality. Built with a golden theme and designed for professional use in jewelry testing laboratories.

## Features

### Main Window (MainWindow.xaml)
- **Company Header**: Professional branding with logo placeholder and company information
- **Test Information Section**: 
  - Party Name dropdown (cmbPartyName) with common jewelry stores
  - Item Name dropdown (cmbItemName) with various jewelry types
  - Date and time tracking
  - Remarks text area
- **Program Type Selection**: Radio buttons for different testing programs:
  - Krupa Gold
  - Fine Gold
  - Silver Ornament
  - Other Metal
- **Main Results**: Primary test measurements
  - Purity percentage (txtMain_1)
  - Weight in grams (txtMain_2)
  - Amount in INR (txtMain_3)
- **Detailed Measurements**: 12 metric fields (txtMetric1-txtMetric12)
- **Action Buttons**: Clear, Save, and Print functionality

### Print Window (PrintWindow.xaml)
- **A4 Print Layout**: Professional report design optimized for printing
- **Company Header**: Logo area and complete company information
- **Test Information Display**: All data from main window formatted professionally
- **Main Results Highlight**: Key findings prominently displayed
- **Detailed Measurements Table**: Organized metric display
- **Professional Footer**: Signature areas for technician and lab director
- **Print Controls**: Print preview, print, and close buttons

## Technical Implementation

### Data Model
- `TestData` class for structured data transfer between windows
- Comprehensive validation and error handling
- Proper data formatting for currency and measurements

### Print Functionality
- Native WPF PrintDialog integration
- A4 paper size optimization
- Print preview capability
- Professional document formatting
- Error handling for print operations

### Styling
- Golden theme throughout the application
- Professional color scheme (#FFD700, #FFA500, #B8860B)
- Consistent styling with custom button, textbox, and layout styles
- Print-friendly black and white formatting

## File Structure
```
KGC/
├── KGC.csproj                 # Project configuration
├── App.xaml                   # Application resources and golden theme
├── App.xaml.cs                # Application startup logic
├── MainWindow.xaml            # Main testing interface
├── MainWindow.xaml.cs         # Main window logic and data handling
├── PrintWindow.xaml           # Print report layout
├── PrintWindow.xaml.cs        # Print functionality implementation
├── Resources/                 # Resources folder for assets
└── README.md                  # This documentation
```

## Usage

### Running the Application
1. Open in Visual Studio on Windows
2. Build and run the solution
3. The main window will appear with sample data

### Testing Workflow
1. Enter party and item information
2. Select appropriate program type
3. Enter main results (purity, weight, amount)
4. Add detailed measurements as needed
5. Add remarks if necessary
6. Click "Print Report" to generate professional test report

### Print Workflow
1. Validation ensures required fields are filled
2. Print window opens with formatted report
3. Use "Print Preview" to review before printing
4. Use "Print" to send to printer
5. Professional A4 report is generated

## Design Highlights

### Professional Appearance
- Golden color scheme reflecting jewelry industry
- Professional typography and spacing
- Company branding integration
- Clean, organized layout

### Print Quality
- A4 paper optimization
- Professional header with company details
- Clear sections for all test information
- Signature areas for authentication
- Print-friendly formatting

### User Experience
- Intuitive interface design
- Comprehensive validation
- Clear error messages
- Responsive layout
- Easy data entry workflow

## Requirements Met
✅ Print.xaml Window with professional layout  
✅ Print Preview functionality  
✅ Print Button Integration in MainWindow  
✅ Complete Report Content including all specified fields  
✅ Professional Layout with company header, results table, footer  
✅ Full Print Functionality with preview and actual printing  
✅ Comprehensive Data Handling for all program types  
✅ Golden Theme UI Design Consistency  

This implementation provides a complete, professional-grade testing application suitable for use in jewelry testing laboratories with comprehensive print functionality for generating official test reports.