using ProvaPub.Interfaces;
using ProvaPub.Models;

namespace ProvaPub.Services
{

    public class OrderService
    {
        public async Task<Order> PayOrder(IPaymentStrategy paymentMethod, decimal paymentValue, int customerId)
        {
            await paymentMethod.Pay(paymentValue, customerId);
            return new Order()
            {
                Value = paymentValue
            };
        }
    }
}
