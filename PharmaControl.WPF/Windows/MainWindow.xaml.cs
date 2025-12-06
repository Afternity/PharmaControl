using System.Windows;
using System.Windows.Controls;
using PharmaControl.WPF.Pages;
using PharmaControl.Domain.Interfaces.NavigationInterfaces;

namespace PharmaControl.WPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
        : Window,
        INavigateMainWindow
    {
        private readonly Frame _frame;

        public MainWindow()
        {
            InitializeComponent();
            _frame = MainFrame;
        }

        private void ShowProfileWindowButton_Click(object sender, RoutedEventArgs e)
        {
            ShowProfileWindow();
        }

        private void ShowMedicinePageButton_Click(object sender, RoutedEventArgs e)
        {
            ShowMedicinePage();
        }

        private void ShowMedicineTypePageButton_Click(object sender, RoutedEventArgs e)
        {
            ShowMedicineTypePage();
        }

        private void ShowPharmacyPageButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPharmacyPage();
        }

        private void ShowSalePageButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSalePagePage();
        }
        public void ShowMedicinePage()
        {
            _frame.Navigate(new MedicinePage());
        }

        public void ShowMedicineTypePage()
        {
            _frame.Navigate(new MedicineTypePage());
        }

        public void ShowPharmacyPage()
        {
            _frame.Navigate(new PharmacyPage());
        }

        public void ShowProfileWindow()
        {
            var window = new ProfileWindow();
            window.Show();
        }

        public void ShowSalePagePage()
        {
            _frame.Navigate(new SalePage());
        }
    }
}
