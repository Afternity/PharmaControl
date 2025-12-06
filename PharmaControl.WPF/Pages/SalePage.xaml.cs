using PharmaControl.Domain.Data.DbContexts;
using PharmaControl.Domain.Interfaces.OperationInterfaces;
using PharmaControl.Domain.Models;
using PharmaControl.WPF.ContractModels;
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

        public async Task CreateAsync(Sale model)
        {
            try
            {
                using var tokenSourse = new CancellationTokenSource(10000);

                if (model == null ||
                    model.Id == Guid.Empty)
                {
                    throw new ArgumentNullException(
                        "Заполнике данные покупки.");
                }

                await _context.AddAsync(model, tokenSourse.Token);
                await _context.SaveChangesAsync(tokenSourse.Token);

                MessageBox.Show("оплата произведена");

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
