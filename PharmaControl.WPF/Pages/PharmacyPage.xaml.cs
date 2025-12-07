using Microsoft.EntityFrameworkCore;
using PharmaControl.Domain.Data.DbContexts;
using PharmaControl.Domain.Interfaces.OperationInterfaces;
using PharmaControl.Domain.Models;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PharmaControl.WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для PharmacyPage.xaml
    /// </summary>
    public partial class PharmacyPage 
        : Page,
        IReadPharmacy
    {
        private readonly PharmaControlDbContext _context;

        public PharmacyPage()
        {
            InitializeComponent();
            _context = new PharmaControlDbContext();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var pharmacies = await GetAllAsync();
            PharmaciesDataGrid.ItemsSource = pharmacies;
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var entity = new Medicine()
            {
                Name = NameTextBox.Text
            };

            var entities = await GetAllByMedicineAsync(
                entity);

            PharmaciesDataGrid.ItemsSource = entities;
        }

        public async Task<IList<Pharmacy>> GetAllAsync()
        {
            try
            {
                using var tokenSourse = new CancellationTokenSource(10000);

                var entities = await _context.Pharmacies
                    .AsNoTracking()
                    .ToListAsync(tokenSourse.Token);

                if (entities.Count == 0)
                {
                    throw new ArgumentException("На данный момент аптеки не найдены.");
                }

                return entities;
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Pharmacy>();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Pharmacy>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Pharmacy>();
            }
        }

        public async Task<IList<Pharmacy>> GetAllByMedicineAsync(Medicine model)
        {
            try
            {
                using var tokenSourse = new CancellationTokenSource(10000);

                if (string.IsNullOrWhiteSpace(model.Name))
                {
                    throw new ArgumentNullException("Название лекарства не выбрано");
                }

                var entities = await _context.PharmacyStocks
                    .AsNoTracking()
                    .Include(pharmacyStock => pharmacyStock.Pharmacy)
                    .Include(pharmacyStock => pharmacyStock.Medicine)
                    .Where(pharmacyStock =>
                         (string.IsNullOrWhiteSpace(model.Name)
                         || pharmacyStock.Medicine.Name.Contains(model.Name)))
                    .Select(pharmacy => pharmacy.Pharmacy)
                    .Distinct()
                    .ToListAsync(tokenSourse.Token);

                if (entities.Count == 0)
                {
                    throw new ArgumentException("На данный момент ни одна аптека не имеет данных лекарств.");
                }

                return entities;
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Pharmacy>();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Pharmacy>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Pharmacy>();
            }
        }
    }
}
