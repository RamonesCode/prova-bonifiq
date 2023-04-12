using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;//package de tests
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;

namespace ProvaPub.Tests
{
    [TestFixture]
    public class CustomerServiceTests
    {
        private CustomerService _customerService;
        private TestDbContext _dbContext;

        [SetUp]
        public void SetUp()
        {
            _dbContext = new TestDbContext(_dbContext);
            _customerService = new CustomerService(_dbContext);
        }

        [Test]
        public async Task CanPurchase_WithValidCustomerIdAndPurchaseValue_ReturnsTrue()
        {
            // Arrange
            var customerId = 1;
            var purchaseValue = 50;

            // Act
            var result = await _customerService.CanPurchase(customerId, purchaseValue);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task CanPurchase_WithInvalidCustomerIdAndPurchaseValue_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var customerId = 0;
            var purchaseValue = 0;

            // Act and Assert
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                await _customerService.CanPurchase(customerId, purchaseValue);
            });
        }

        [Test]
        public async Task CanPurchase_WithNonExistingCustomerId_ThrowsInvalidOperationException()
        {
            // Arrange
            var customerId = 999;
            var purchaseValue = 50;

            // Act and Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await _customerService.CanPurchase(customerId, purchaseValue);
            });
        }

        [Test]
        public async Task CanPurchase_WithCustomerPurchasingTwiceInAMonth_ReturnsFalse()
        {
            // Arrange
            var customerId = 1;
            var purchaseValue = 50;

            // Add a purchase order for this customer in the current month
            var customer = new Customer { Id = customerId };
            var order = new Order { OrderDate = DateTime.UtcNow, Value = purchaseValue };
            customer.Orders = customer.Orders ?? new List<Order>(); // Verifica se customer.Orders é nulo e, se for, atribui uma nova lista vazia.
            customer.Orders.Add(order);
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _customerService.CanPurchase(customerId, purchaseValue);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task CanPurchase_WithNewCustomerPurchasingMoreThan100_ReturnsFalse()
        {
            // Arrange
            var customerId = 2;
            var purchaseValue = 150;

            // Act
            var result = await _customerService.CanPurchase(customerId, purchaseValue);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
