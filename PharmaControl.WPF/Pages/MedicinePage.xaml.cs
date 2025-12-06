using Microsoft.EntityFrameworkCore;
using PharmaControl.Domain.Data.DbContexts;
using PharmaControl.Domain.Interfaces.NavigationInterfaces;
using PharmaControl.Domain.Interfaces.OperationInterfaces;
using PharmaControl.Domain.Models;
using PharmaControl.WPF.ContractModels;
using PharmaControl.WPF.Windows;
using System.Windows;
using System.Windows.Controls;

namespace PharmaControl.WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для MedicinePage.xaml
    /// </summary>
    public partial class MedicinePage 
        : Page,
        IReadMedicine,
        INavigateMedicinePage
    {
        private readonly PharmaControlDbContext _context;
        private readonly Pharmacy _pharmacy;
        private readonly Medicine _medicine;

        public MedicinePage()
        {
            InitializeComponent();
            _context = new PharmaControlDbContext();
            _pharmacy = PharmacyProfile.Profile;
            _medicine = MedicineProfile.Profile;    
        }

        public async Task<IList<Medicine>> GetAllAsync(
            Pharmacy model)
        {
            try
            {
                using var tokenSourse = new CancellationTokenSource(10000);

                if (model == null ||
                    model.Id == Guid.Empty)
                {
                    throw new ArgumentNullException(
                        "Аптека не выбрана");
                }

                var entities = await _context.Medicines
                    .Where(medicine =>
                        medicine.Id == model.Id)
                    .ToListAsync(tokenSourse.Token);

                if (entities.Count == 0)
                {
                    throw new ArgumentException(
                        "На данный моментаптека не имеет лекарств.");
                }

                return entities;
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Medicine>();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Medicine>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Medicine>();
            }
        }

        public void ShowMedicineTypeInfoWindow()
        {
            var windows = Application.Current.Windows.OfType<MedicineTypeInfoWindow>()
                .ToList();

            foreach (var win in windows)
                win.Close();

            var window = new MedicineTypeInfoWindow();
            window.Show();
        }

        public void ShowPharmacyStockInfoWindow()
        {
            var windows = Application.Current.Windows.OfType<PharmacyStockInfoWindow>()
                .ToList();

            foreach (var win in windows)
                win.Close();

            var window = new PharmacyStockInfoWindow();
            window.Show();
        }
    }
}
