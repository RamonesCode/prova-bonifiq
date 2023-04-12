namespace ProvaPub.Interfaces
{
    public interface IPaymentStrategy
    {
        Task Pay(decimal paymentValue, int customerId);
    }
}
