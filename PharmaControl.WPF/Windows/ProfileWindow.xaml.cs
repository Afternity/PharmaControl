using Microsoft.EntityFrameworkCore;
using PharmaControl.Domain.Data.DbContexts;
using PharmaControl.Domain.Interfaces.MainInterfaces;
using PharmaControl.Domain.Interfaces.NavigationInterfaces;
using PharmaControl.Domain.Models;
using PharmaControl.WPF.ContractModels;
using System.Windows;

namespace PharmaControl.WPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow
        : Window,
        IProfile,
        INavigateProfileWindow
    {
        private readonly PharmaControlDbContext _context;
        private readonly Employee _employee;

        public ProfileWindow()
        {
            InitializeComponent();
            _context = new PharmaControlDbContext();
            _employee = EmployeeProfile.Profile;
        }

        private async void Window_Loaded(
            object sender, 
            RoutedEventArgs e)
        {
            var profile = await GetEmployeeAsync(
                _employee);

            FullNameTextBox.Text = profile.FullName;
            EmailTextBox.Text = profile.Email;
            PasswordTextBox.Text = profile.Password;
            PhoneTextBox.Text = profile.Phone;

            var pharmacy = await GetEmployeeParmacyAsync(
                _employee);

            PharmacyNameTextBox.Text = pharmacy.Name;
            PharmacyAddressTextBox.Text = pharmacy.Address;
            PharmacyEmailTextBox.Text = pharmacy.Email;
            PharmacyPhoneNumberTextBox.Text = pharmacy.PhoneNumber;
            PharmacyOpeningTimeTextBox.Text = pharmacy.OpeningTime.ToString();
            PharmacyClosingTimeTextBox.Text = pharmacy.ClosingTime.ToString();

            var count = await GetPharmacySaleCountAsync(
                _employee,
                _employee.Pharmacy);

            CountTextBox.Text = count.ToString();
        }

        private async void UpdateProfileButton_Click(
            object sender, 
            RoutedEventArgs e)
        {
            _employee.FullName = FullNameTextBox.Text;
            _employee.Email = EmailTextBox.Text;
            _employee.Password = PasswordTextBox.Text;
            _employee.Phone = PhoneTextBox.Text;
           

            await UpdateEmployeeAsync(_employee);
        }

        private void GoBackButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            ShowMainWindow();
        }

        private void ShowAuthWindowButton_Click(object sender, RoutedEventArgs e)
        {
            ShowAuthWindow();
        }

        public async Task UpdateEmployeeAsync(
           Employee model)
        {
            try
            {
                using var tokenSourse = new CancellationTokenSource(10000);

                if (model == null ||
                    model.Id == Guid.Empty)
                {
                    throw new ArgumentNullException(
                        "Не выбран профиль");
                }

                var entity = await _context.Employees
                    .FirstOrDefaultAsync(employee =>
                        employee.Id == model.Id,
                        tokenSourse.Token);

                if (entity == null)
                {
                    throw new ArgumentException(
                        "Работник не найден");
                }

                entity.FullName = model.FullName;
                entity.Email = model.Email;
                entity.Password = model.Password;
                entity.Phone = model.Phone;

                _context.Employees.Update(
                    entity);
                await _context.SaveChangesAsync(
                    tokenSourse.Token);

                MessageBox.Show("Обновление профиля успешно.");

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

        public async Task<Employee> GetEmployeeAsync(
            Employee model)
        {
            try
            {
                using var tokenSourse = new CancellationTokenSource(10000);

                if (model == null ||
                    model.Id == Guid.Empty)
                {
                    throw new ArgumentNullException(
                        "Не выбран профиль");
                }

                var entity = await _context.Employees
                    .FirstOrDefaultAsync(employee =>
                        employee.Id == model.Id,
                        tokenSourse.Token);

                if (entity == null)
                {
                    throw new ArgumentException(
                        "Профиль не найден");
                }

                return entity;
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show(ex.Message);
                return new Employee();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return new Employee();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new Employee();
            }
        }

        public async Task<Pharmacy> GetEmployeeParmacyAsync(
            Employee model)
        {
            try
            {
                using var tokenSourse = new CancellationTokenSource(10000);

                if (model == null ||
                    model.Id == Guid.Empty)
                {
                    throw new ArgumentNullException(
                        "Не выбран профиль");
                }

                var entity = await _context.Employees
                    .Include(e => e.Pharmacy)
                    .FirstOrDefaultAsync(employee =>
                        employee.Id == model.Id,
                        tokenSourse.Token);

                if (entity == null)
                {
                    throw new ArgumentException(
                        "Аптека не найдена");
                }

                return entity.Pharmacy;
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show(ex.Message);
                return new Pharmacy();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return new Pharmacy();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new Pharmacy();
            }
        }

        public async Task<int> GetPharmacySaleCountAsync(
            Employee employee,
            Pharmacy pharmacy)
        {
            try
            {
                using var tokenSourse = new CancellationTokenSource(10000);

                if (employee == null ||
                    employee.Id == Guid.Empty ||
                    pharmacy == null ||
                    pharmacy.Id == Guid.Empty)
                {
                    throw new ArgumentNullException(
                        "Не выбран профиль или аптека");
                }

                var entitiesCount = await _context.Sales
                    .Where(sales =>
                        sales.PharmacyId == pharmacy.Id)
                    .CountAsync(tokenSourse.Token);

                if (entitiesCount == 0)
                {
                    throw new ArgumentException(
                        "На данный момент в аптеке нет продаж");
                }

                return entitiesCount;
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        public void ShowAuthWindow()
        {
            var authWindow = new AuthWindow();
            authWindow.Show();

            var windows = Application.Current.Windows.OfType<Window>().ToList();

            foreach (var window in windows)
            {
                if (window is not AuthWindow)
                    window.Close();
            }
        }

        public void ShowMainWindow()
        {
            this.Close();
        }
    }
}
