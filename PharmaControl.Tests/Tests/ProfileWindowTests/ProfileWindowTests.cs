using Microsoft.EntityFrameworkCore;
using PharmaControl.Domain.Data.DbContexts;
using PharmaControl.Domain.Models;
using PharmaControl.WPF.ContractModels;
using PharmaControl.WPF.Windows;
using System.Reflection;
using System.Runtime.Serialization;

namespace PharmaControl.Tests.Tests.ProfileWindowTests
{
    public class ProfileWindowTests : IDisposable
    {
        private PharmaControlDbContext _context;

        private PharmaControlDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<PharmaControlDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new PharmaControlDbContext(options);
        }

        // Вспомогательный метод для создания ProfileWindow без инициализации UI
        private ProfileWindow CreateProfileWindowWithoutUI(Employee employee = null)
        {
            var window = (ProfileWindow)FormatterServices.GetUninitializedObject(typeof(ProfileWindow));

            // Устанавливаем приватные поля через рефлексию
            var contextField = typeof(ProfileWindow).GetField("_context",
                BindingFlags.NonPublic | BindingFlags.Instance);
            contextField?.SetValue(window, _context);

            var employeeField = typeof(ProfileWindow).GetField("_employee",
                BindingFlags.NonPublic | BindingFlags.Instance);
            employeeField?.SetValue(window, employee ?? new Employee());

            return window;
        }

        [Fact]
        public async Task GetEmployeeAsync_ValidEmployee_ReturnsEmployee()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FullName = "Тестовый сотрудник",
                Email = "test@example.com",
                Password = "password123",
                Phone = "+79123456789"
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            var window = CreateProfileWindowWithoutUI();

            // Act
            var result = await window.GetEmployeeAsync(employee);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Тестовый сотрудник", result.FullName);
            Assert.Equal("test@example.com", result.Email);
            Assert.Equal("password123", result.Password);
            Assert.Equal("+79123456789", result.Phone);
        }

        [Fact]
        public async Task GetEmployeeAsync_NullEmployee_ReturnsEmptyEmployee()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var window = CreateProfileWindowWithoutUI();

            // Act
            var result = await window.GetEmployeeAsync(null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task GetEmployeeAsync_EmptyId_ReturnsEmptyEmployee()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var window = CreateProfileWindowWithoutUI();
            var emptyEmployee = new Employee { Id = Guid.Empty };

            // Act
            var result = await window.GetEmployeeAsync(emptyEmployee);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task GetEmployeeAsync_NonExistentEmployee_ReturnsEmptyEmployee()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var window = CreateProfileWindowWithoutUI();
            var nonExistentEmployee = new Employee { Id = Guid.NewGuid() };

            // Act
            var result = await window.GetEmployeeAsync(nonExistentEmployee);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task GetEmployeeParmacyAsync_ValidEmployee_ReturnsPharmacy()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var pharmacy = new Pharmacy
            {
                Id = Guid.NewGuid(),
                Name = "Тестовая аптека",
                Address = "Тестовый адрес",
                PhoneNumber = "+79999999999",
                Email = "pharmacy@test.com",
                OpeningTime = TimeSpan.FromHours(9),
                ClosingTime = TimeSpan.FromHours(21)
            };

            await _context.Pharmacies.AddAsync(pharmacy);
            await _context.SaveChangesAsync();

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                PharmacyId = pharmacy.Id
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            var window = CreateProfileWindowWithoutUI();

            // Act
            var result = await window.GetEmployeeParmacyAsync(employee);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Тестовая аптека", result.Name);
            Assert.Equal("Тестовый адрес", result.Address);
            Assert.Equal("+79999999999", result.PhoneNumber);
            Assert.Equal("pharmacy@test.com", result.Email);
            Assert.Equal(TimeSpan.FromHours(9), result.OpeningTime);
            Assert.Equal(TimeSpan.FromHours(21), result.ClosingTime);
        }

        [Fact]
        public async Task GetEmployeeParmacyAsync_EmployeeWithoutPharmacy_ReturnsEmptyPharmacy()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                // Не устанавливаем PharmacyId
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            var window = CreateProfileWindowWithoutUI();

            // Act
            var result = await window.GetEmployeeParmacyAsync(employee);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task GetEmployeeParmacyAsync_NonExistentPharmacy_ReturnsEmptyPharmacy()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                PharmacyId = Guid.NewGuid() // Несуществующая аптека
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            var window = CreateProfileWindowWithoutUI();

            // Act
            var result = await window.GetEmployeeParmacyAsync(employee);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task GetPharmacySaleCountAsync_ValidData_ReturnsCount()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var pharmacy = new Pharmacy { Id = Guid.NewGuid() };
            await _context.Pharmacies.AddAsync(pharmacy);
            await _context.SaveChangesAsync();

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                PharmacyId = pharmacy.Id
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            // Добавляем продажи
            var sales = new List<Sale>
            {
                new Sale { Id = Guid.NewGuid(), PharmacyId = pharmacy.Id },
                new Sale { Id = Guid.NewGuid(), PharmacyId = pharmacy.Id },
                new Sale { Id = Guid.NewGuid(), PharmacyId = pharmacy.Id }
            };

            await _context.Sales.AddRangeAsync(sales);
            await _context.SaveChangesAsync();

            var window = CreateProfileWindowWithoutUI();

            // Act
            var result = await window.GetPharmacySaleCountAsync(employee, pharmacy);

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public async Task GetPharmacySaleCountAsync_NoSales_ReturnsZero()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var pharmacy = new Pharmacy { Id = Guid.NewGuid() };
            await _context.Pharmacies.AddAsync(pharmacy);
            await _context.SaveChangesAsync();

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                PharmacyId = pharmacy.Id
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            var window = CreateProfileWindowWithoutUI();

            // Act
            var result = await window.GetPharmacySaleCountAsync(employee, pharmacy);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task GetPharmacySaleCountAsync_DifferentPharmacy_ReturnsZero()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var pharmacy1 = new Pharmacy { Id = Guid.NewGuid() };
            var pharmacy2 = new Pharmacy { Id = Guid.NewGuid() };
            await _context.Pharmacies.AddRangeAsync(pharmacy1, pharmacy2);
            await _context.SaveChangesAsync();

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                PharmacyId = pharmacy1.Id
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            // Добавляем продажи только для второй аптеки
            var sales = new List<Sale>
            {
                new Sale { Id = Guid.NewGuid(), PharmacyId = pharmacy2.Id },
                new Sale { Id = Guid.NewGuid(), PharmacyId = pharmacy2.Id }
            };
            await _context.Sales.AddRangeAsync(sales);
            await _context.SaveChangesAsync();

            var window = CreateProfileWindowWithoutUI();

            // Act - запрашиваем количество продаж для первой аптеки
            var result = await window.GetPharmacySaleCountAsync(employee, pharmacy1);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_ValidData_UpdatesSuccessfully()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var originalEmployee = new Employee
            {
                Id = Guid.NewGuid(),
                FullName = "Старое имя",
                Email = "old@email.com",
                Password = "oldpass",
                Phone = "+79111111111"
            };

            await _context.Employees.AddAsync(originalEmployee);
            await _context.SaveChangesAsync();

            var window = CreateProfileWindowWithoutUI();

            var updatedEmployee = new Employee
            {
                Id = originalEmployee.Id,
                FullName = "Новое имя",
                Email = "new@email.com",
                Password = "newpass",
                Phone = "+79222222222"
            };

            // Act
            await window.UpdateEmployeeAsync(updatedEmployee);

            // Assert
            var dbEmployee = await _context.Employees.FindAsync(originalEmployee.Id);
            Assert.NotNull(dbEmployee);
            Assert.Equal("Новое имя", dbEmployee.FullName);
            Assert.Equal("new@email.com", dbEmployee.Email);
            Assert.Equal("newpass", dbEmployee.Password);
            Assert.Equal("+79222222222", dbEmployee.Phone);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_UpdateOnlySomeFields_PreservesOtherFields()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var originalEmployee = new Employee
            {
                Id = Guid.NewGuid(),
                FullName = "Полное имя",
                Email = "email@test.com",
                Password = "password",
                Phone = "+79111111111",
                PharmacyId = Guid.NewGuid()
            };

            await _context.Employees.AddAsync(originalEmployee);
            await _context.SaveChangesAsync();

            var window = CreateProfileWindowWithoutUI();

            var updatedEmployee = new Employee
            {
                Id = originalEmployee.Id,
                FullName = "Обновленное имя",
                Email = "email@test.com", // То же самое
                Password = "password",    // То же самое
                Phone = "+79222222222",   // Изменено
                PharmacyId = originalEmployee.PharmacyId // Должно сохраниться
            };

            // Act
            await window.UpdateEmployeeAsync(updatedEmployee);

            // Assert
            var dbEmployee = await _context.Employees.FindAsync(originalEmployee.Id);
            Assert.NotNull(dbEmployee);
            Assert.Equal("Обновленное имя", dbEmployee.FullName);
            Assert.Equal("email@test.com", dbEmployee.Email);
            Assert.Equal("password", dbEmployee.Password);
            Assert.Equal("+79222222222", dbEmployee.Phone);
            Assert.Equal(originalEmployee.PharmacyId, dbEmployee.PharmacyId);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_NonExistentEmployee_DoesNotThrow()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var window = CreateProfileWindowWithoutUI();

            var nonExistentEmployee = new Employee
            {
                Id = Guid.NewGuid(),
                FullName = "Несуществующий"
            };

            // Act & Assert - не должно быть исключения
            await window.UpdateEmployeeAsync(nonExistentEmployee);
            Assert.True(true); // Если добрались сюда, значит исключения не было
        }

        [Fact]
        public async Task UpdateEmployeeAsync_NullEmployee_DoesNotThrow()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var window = CreateProfileWindowWithoutUI();

            // Act & Assert
            await window.UpdateEmployeeAsync(null);
            Assert.True(true); // Если добрались сюда, значит исключения не было
        }

        [Fact]
        public async Task UpdateEmployeeAsync_EmptyId_DoesNotThrow()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var window = CreateProfileWindowWithoutUI();

            var emptyEmployee = new Employee
            {
                Id = Guid.Empty,
                FullName = "Без ID"
            };

            // Act & Assert
            await window.UpdateEmployeeAsync(emptyEmployee);
            Assert.True(true); // Если добрались сюда, значит исключения не было
        }

        public void Dispose()
        {
            _context?.Dispose();
            EmployeeProfile.Profile = null;
            PharmacyProfile.Profile = null;
        }
    }
}
