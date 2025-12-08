using Microsoft.EntityFrameworkCore;
using PharmaControl.Domain.Data.DbContexts;
using PharmaControl.Domain.Models;
using PharmaControl.WPF.ContractModels;
using PharmaControl.WPF.Windows;
using System.Reflection;
using System.Runtime.Serialization;

namespace PharmaControl.Tests.Tests.AuthWindowTests
{
    public class AuthWindowTests : IDisposable
    {
        private PharmaControlDbContext _context;

        private PharmaControlDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<PharmaControlDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new PharmaControlDbContext(options);
        }

        // Вспомогательный метод для создания AuthWindow без инициализации UI
        private AuthWindow CreateAuthWindowWithoutUI()
        {
            var window = (AuthWindow)FormatterServices.GetUninitializedObject(typeof(AuthWindow));

            // Устанавливаем приватные поля через рефлексию
            var contextField = typeof(AuthWindow).GetField("_context",
                BindingFlags.NonPublic | BindingFlags.Instance);
            contextField?.SetValue(window, _context);

            return window;
        }

        [Fact]
        public async Task AuthAsync_ValidCredentials_ReturnsEmployee()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var pharmacy = new Pharmacy
            {
                Id = Guid.NewGuid(),
                Name = "Тестовая аптека"
            };
            await _context.Pharmacies.AddAsync(pharmacy);
            await _context.SaveChangesAsync();

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FullName = "Иванова Анна",
                Email = "ivanova@pharma.ru",
                Password = "pharm123",
                Phone = "+79123456789",
                PharmacyId = pharmacy.Id
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            var window = CreateAuthWindowWithoutUI();

            // Act
            var result = await window.AuthAsync("ivanova@pharma.ru", "pharm123");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Иванова Анна", result.FullName);
            Assert.Equal("ivanova@pharma.ru", result.Email);
            Assert.NotNull(result.Pharmacy);
            Assert.Equal("Тестовая аптека", result.Pharmacy.Name);
        }

        [Fact]
        public async Task AuthAsync_InvalidCredentials_ReturnsEmptyEmployee()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var pharmacy = new Pharmacy { Id = Guid.NewGuid() };
            await _context.Pharmacies.AddAsync(pharmacy);
            await _context.SaveChangesAsync();

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Email = "ivanova@pharma.ru",
                Password = "pharm123",
                PharmacyId = pharmacy.Id
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            var window = CreateAuthWindowWithoutUI();

            // Act
            var result = await window.AuthAsync("wrong@email.com", "wrongpass");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Guid.Empty, result.Id); // Возвращает пустого сотрудника
        }

        [Fact]
        public async Task AuthAsync_EmptyEmail_ReturnsEmptyEmployee()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var window = CreateAuthWindowWithoutUI();

            // Act
            var result = await window.AuthAsync("", "password");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task AuthAsync_EmptyPassword_ReturnsEmptyEmployee()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var window = CreateAuthWindowWithoutUI();

            // Act
            var result = await window.AuthAsync("test@email.com", "");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task AuthAsync_UserNotFound_ReturnsEmptyEmployee()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var window = CreateAuthWindowWithoutUI();

            // Act
            var result = await window.AuthAsync("nonexistent@email.com", "password");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task AuthAsync_CaseSensitiveEmail_ReturnsEmptyEmployee()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var pharmacy = new Pharmacy { Id = Guid.NewGuid() };
            await _context.Pharmacies.AddAsync(pharmacy);
            await _context.SaveChangesAsync();

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Email = "Ivanova@Pharma.ru", // с большой буквы
                Password = "pharm123",
                PharmacyId = pharmacy.Id
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            var window = CreateAuthWindowWithoutUI();

            // Act
            var result = await window.AuthAsync("ivanova@pharma.ru", "pharm123"); // с маленькой

            // Assert - email должен быть чувствителен к регистру
            Assert.NotNull(result);
            Assert.Equal(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task AuthAsync_PasswordCaseSensitive_ReturnsEmptyEmployee()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var pharmacy = new Pharmacy { Id = Guid.NewGuid() };
            await _context.Pharmacies.AddAsync(pharmacy);
            await _context.SaveChangesAsync();

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Email = "ivanova@pharma.ru",
                Password = "Pharm123", // с большой буквы
                PharmacyId = pharmacy.Id
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            var window = CreateAuthWindowWithoutUI();

            // Act
            var result = await window.AuthAsync("ivanova@pharma.ru", "pharm123"); // с маленькой

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task AuthAsync_WithWhitespace_ReturnsEmptyEmployee()
        {
            // Arrange
            _context = CreateInMemoryContext();

            var pharmacy = new Pharmacy { Id = Guid.NewGuid() };
            await _context.Pharmacies.AddAsync(pharmacy);
            await _context.SaveChangesAsync();

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Email = "ivanova@pharma.ru",
                Password = "pharm123",
                PharmacyId = pharmacy.Id
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            var window = CreateAuthWindowWithoutUI();

            // Act - email с пробелами
            var result = await window.AuthAsync("  ivanova@pharma.ru  ", "pharm123");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Guid.Empty, result.Id);
        }

        public void Dispose()
        {
            _context?.Dispose();
            EmployeeProfile.Profile = null;
            PharmacyProfile.Profile = null;
        }
    }
}
