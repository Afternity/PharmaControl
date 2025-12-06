using Microsoft.EntityFrameworkCore;
using PharmaControl.Domain.Data.DbContexts;
using PharmaControl.Domain.Interfaces.OperationInterfaces;
using PharmaControl.Domain.Models;
using PharmaControl.WPF.ContractModels;
using System.Windows;

namespace PharmaControl.WPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для PharmacyStockInfoWindow.xaml
    /// </summary>
    public partial class PharmacyStockInfoWindow 
        : Window,
        IPharmacyStockInfo
    {
        private readonly PharmaControlDbContext _context;
        private readonly Medicine _medicine;
        private readonly Pharmacy _pharmacy;

        public PharmacyStockInfoWindow()
        {
            InitializeComponent();
            _context = new PharmaControlDbContext();
            _medicine = MedicineProfile.Profile;
            _pharmacy = PharmacyProfile.Profile;
        }

        private async void Window_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            var entity = await GetPharmacyStockAsync(
                _medicine,
                _pharmacy);

            QuantityTextBox.Text = entity.Quantity.ToString();
            MinStockLevelTextBox.Text = entity.MinStockLevel.ToString();
            ReorderQuantityTextBox.Text = entity.ReorderQuantity.ToString();
        }

        public async Task<PharmacyStock> GetPharmacyStockAsync(
            Medicine medicine,
            Pharmacy pharmacy)
        {
            try
            {
                using var tokenSourse = new CancellationTokenSource(10000);

                if (medicine == null ||
                    medicine.Id == Guid.Empty ||
                    pharmacy == null ||
                    pharmacy.Id == Guid.Empty)
                {
                    throw new ArgumentNullException(
                        "Лекарство не выбрано или нет доступа к аптеке");
                }

                var entity = await _context.PharmacyStocks
                    .FirstOrDefaultAsync(pharmacyStock =>
                        pharmacyStock.MedicineId == medicine.Id &&
                        pharmacyStock.PharmacyId == pharmacy.Id,
                        tokenSourse.Token);

                if (entity == null)
                {
                    throw new ArgumentException(
                        "На складе аптеки не найден");
                }

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
    }
}
