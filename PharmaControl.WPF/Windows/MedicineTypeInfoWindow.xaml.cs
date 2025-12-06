using Microsoft.EntityFrameworkCore;
using PharmaControl.Domain.Data.DbContexts;
using PharmaControl.Domain.Interfaces.OperationInterfaces;
using PharmaControl.Domain.Models;
using PharmaControl.WPF.ContractModels;
using System.Windows;

namespace PharmaControl.WPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для MedicineTypeInfoWindow.xaml
    /// </summary>
    public partial class MedicineTypeInfoWindow 
        : Window,
        IMedicineTypeInfo
    {
        private readonly PharmaControlDbContext _context;
        private readonly Medicine _medicine;

        public MedicineTypeInfoWindow()
        {
            InitializeComponent();
            _context = new PharmaControlDbContext();
            _medicine = MedicineProfile.Profile;
        }

        public async Task<MedicineType> GetMedicineTypeAsync(
            Medicine model)
        {
            try
            {
                using var tokenSourse = new CancellationTokenSource(10000);

                if (model == null ||
                    model.Id == Guid.Empty)
                {
                    throw new ArgumentNullException(
                        "Лекарство не выбрано");
                }

                var entity = await _context.Medicines
                    .FirstOrDefaultAsync(medicine =>
                        medicine.Id == model.Id,
                        tokenSourse.Token);

                if (entity == null)
                {
                    throw new ArgumentException(
                        "Лекарство не найден");
                }

                return entity.MedicineType;
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show(ex.Message);
                return new MedicineType();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return new MedicineType();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new MedicineType();
            }
        }
    }
}
