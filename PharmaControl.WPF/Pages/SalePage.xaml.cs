using Microsoft.EntityFrameworkCore;
using PharmaControl.Domain.Data.DbContexts;
using PharmaControl.Domain.Interfaces.OperationInterfaces;
using PharmaControl.Domain.Models;
using PharmaControl.WPF.ContractModels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PharmaControl.WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для SalePage.xaml
    /// </summary>
    public partial class SalePage 
        : Page,
        ISale
    {
        private readonly PharmaControlDbContext _context;
        private readonly Pharmacy _parmacy;
        private readonly Employee _employee;
        private readonly Medicine _medicine = new Medicine();
        private readonly PharmacyStock _pharmacyStock = new PharmacyStock();

        public SalePage()
        {
            InitializeComponent();

            _context = new PharmaControlDbContext();
            _parmacy = PharmacyProfile.Profile;
            _employee = EmployeeProfile.Profile;  
        }

        private async void CreateSaleButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            _medicine.Name = MedicineNameTextBox.Text;

            var pharmacyStock = await FindPharmacyStockAsync(
                _parmacy, _medicine);

            var newSale = new Sale()
            {
                Id = Guid.NewGuid(),
                SaleDate = DateTime.UtcNow,
                Amount = int.Parse(AmountTextBox.Text),
                PaymentMethod = (PaymentState)PaymentMethodComboBox.SelectedIndex,
                MedicineId = pharmacyStock.MedicineId,
                PharmacyId = pharmacyStock.PharmacyId,
                PharmacyStock = pharmacyStock
            };

            await CreateAsync(newSale);
        }

        public async Task<PharmacyStock> FindPharmacyStockAsync(
            Pharmacy pharmacy,
            Medicine medicine)
        {
            try
            {
                using var tokenSourse = new CancellationTokenSource();

                if (pharmacy == null ||
                    pharmacy.Id == Guid.Empty  ||
                    medicine.Name == string.Empty)
                {
                    throw new ArgumentNullException(
                        "Аптека или лекарство не выбрано.");
                }

               var entity = await _context.PharmacyStocks
                    .Include(pharmacyStock => pharmacyStock.Medicine)
                    .Include(pharmacyStock => pharmacyStock.Pharmacy)
                    .FirstOrDefaultAsync( pharmacyStock =>
                        pharmacyStock.Pharmacy == pharmacy &&
                        pharmacyStock.Medicine.Name == medicine.Name,
                        tokenSourse.Token );

                if (entity == null)
                    throw new ArgumentNullException(
                        "Лекарство не найдено.");

                return entity;

            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show(ex.Message);
                return new PharmacyStock();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return new PharmacyStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new PharmacyStock();
            }
        }

        public async Task CreateAsync(
            Sale model)
        {
            try
            {
                using var tokenSourse = new CancellationTokenSource(10000);

                if (model == null ||
                    model.Id == Guid.Empty ||
                    model.Amount < 0)
                {
                    throw new ArgumentNullException(
                        "Заполнике данные покупки.");
                }

                await _context.AddAsync(model, tokenSourse.Token);
                await _context.SaveChangesAsync(tokenSourse.Token);

                MessageBox.Show("Оплата произведена");

            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
