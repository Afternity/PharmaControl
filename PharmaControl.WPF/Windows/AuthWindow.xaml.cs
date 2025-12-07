using Microsoft.EntityFrameworkCore;
using PharmaControl.Domain.Data.DbContexts;
using PharmaControl.Domain.Interfaces.MainInterfaces;
using PharmaControl.Domain.Interfaces.NavigationInterfaces;
using PharmaControl.Domain.Models;
using PharmaControl.WPF.ContractModels;
using System.Threading.Tasks;
using System.Windows;

namespace PharmaControl.WPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow 
        : Window,
        IAuth,
        INavigateAuthWindow
    {
        private readonly PharmaControlDbContext _context;

        public AuthWindow()
        {
            InitializeComponent();
            _context = new PharmaControlDbContext();
            EmailTextBox.Text = "ivanova@pharma.ru";
            PasswordTextBox.Text = "pharm123";
        }

        private async void Window_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            var entity = await AuthAsync(
                EmailTextBox.Text,
                PasswordTextBox.Text);

            EmployeeProfile.Profile = entity;
            PharmacyProfile.Profile = entity.Pharmacy;

            await Task.Delay(2000);

            ShowMainWindow();
        }

        private async void AuthButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            var entity = await AuthAsync(
                EmailTextBox.Text,
                PasswordTextBox.Text);

            EmployeeProfile.Profile = entity;
            PharmacyProfile.Profile = entity.Pharmacy;

            await Task.Delay(2000);

            ShowMainWindow();
        }
        public async Task<Employee> AuthAsync(
           string email,
           string password)
        {
            try
            {
                using var tokenSourse = new CancellationTokenSource(10000);

                if (string.IsNullOrWhiteSpace(email) ||
                    string.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentNullException(
                        "Email и пароль не могут быть пустыми");
                }

                var entity = await _context.Employees
                    .Include(employee => employee.Pharmacy)
                    .FirstOrDefaultAsync(employee =>
                        employee.Email == email &&
                        employee.Password == password,
                        tokenSourse.Token);

                if (entity == null)
                {
                    throw new ArgumentException(
                        "Пользовательно не найден");
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

        public void ShowMainWindow()
        {
            var window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
