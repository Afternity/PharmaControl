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
            this.Close();
        }

        public void ShowSalePagePage()
        {
            _frame.Navigate(new SalePage());
        }
    }
}
