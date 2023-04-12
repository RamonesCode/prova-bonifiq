using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class CustomerService
    {
        private readonly TestDbContext _testDbContext;

        public CustomerService(TestDbContext testDbContext)
        {
            _testDbContext = testDbContext;
        }

        public CustomerList ListCustomers(int page)
        {
            int pageSize = 10;// 10 clientes por pag.
            int totalCount = _testDbContext.Customers.Count(); // verifica qntd total de clientes
            int skip = (page - 1) * pageSize;
            List<Customer> customers = _testDbContext.Customers.Skip(skip).Take(pageSize).ToList();
            bool hasNext = skip + pageSize <= totalCount;

            return new CustomerList() { HasNext = hasNext, TotalCount = totalCount, Items = customers };
        }

        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        {
            
            if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));

            if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

            //Business Rule: Non registered Customers cannot purchase
            var customer = await _testDbContext.Customers.FindAsync(customerId);
            if (customer == null) throw new InvalidOperationException($"Customer Id {customerId} does not exists");

            //Business Rule: A customer can purchase only a single time per month
            var baseDate = DateTime.UtcNow.AddMonths(-1);
            var ordersInThisMonth = await _testDbContext.Customers.CountAsync(s => s.Id == customerId && s.Orders.Any(w => w.OrderDate >= baseDate));
            if (ordersInThisMonth > 1)
                return false;

            //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
            var haveBoughtBefore = await _testDbContext.Customers.CountAsync(s => s.Id == customerId && s.Orders.Any());
            if (haveBoughtBefore == 0 && purchaseValue > 100)
                return false;

            return true;
        }

    }
}
