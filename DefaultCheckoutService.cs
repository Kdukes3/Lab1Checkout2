namespace Lab1Checkout2;

public class DefaultCheckoutService : ICheckoutService
{
    public DefaultCheckoutService(InMemoryRepository repo, DefaultPolicy policy, IClock clock)
    {
        throw new NotImplementedException();
    }
    
    public Catalog GetCatalog()
    {
        throw new NotImplementedException();
    }

    public Receipt Checkout(string itemId, Borrower borrower, DateTime dueDate)
    {
        throw new NotImplementedException();
    }

    public Receipt ReturnItem(string itemId)
    {
        throw new NotImplementedException();
    }

    public void MarkLost(string itemId)
    {
        throw new NotImplementedException();
    }

    public List<CheckoutRecord> ListActiveLoans()
    {
        throw new NotImplementedException();
    }

    public List<CheckoutRecord> FindDueSoon(TimeSpan window)
    {
        throw new NotImplementedException();
    }

    public List<CheckoutRecord> FindOverdue()
    {
        throw new NotImplementedException();
    }
}