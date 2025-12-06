using Microsoft.EntityFrameworkCore;
using PharmaControl.Domain.Data.DbContexts;
using PharmaControl.Domain.Interfaces.OperationInterfaces;
using PharmaControl.Domain.Models;
using PharmaControl.WPF.ContractModels;
using System.Windows;
using System.Windows.Controls;

namespace PharmaControl.WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для MedicineTypePage.xaml
    /// </summary>
    public partial class MedicineTypePage 
        : Page,
        IReadMedicineType
    {
        private readonly PharmaControlDbContext _context;
        private readonly Pharmacy _pharmacy;
        public MedicineTypePage()
        {
            InitializeComponent();
            _context = new PharmaControlDbContext();
            _pharmacy = PharmacyProfile.Profile;
        }

        public async Task<IList<MedicineType>> GetAllAsync(
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

                var medicineTypes = await _context.PharmacyStocks
                    .Where(pharmacyStock =>
                        pharmacyStock.PharmacyId == model.Id)
                    .Include(ps => ps.Medicine) 
                        .ThenInclude(m => m.MedicineType) 
                    .Select(ps => ps.Medicine.MedicineType) 
                    .Distinct() 
                    .ToListAsync(tokenSourse.Token);

                if (medicineTypes.Count == 0)
                {
                    throw new ArgumentException(
                        "На данный момент аптека не имеет лекарств.");
                }

                return medicineTypes;
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show(ex.Message);
                return new List<MedicineType>();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return new List<MedicineType>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<MedicineType>();
            }
        }
    }
}
