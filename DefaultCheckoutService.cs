namespace Lab1Checkout2;

public class DefaultCheckoutService : ICheckoutService
{
    private IRepository _repo;
    private ICatalog _catalog;
    private DefaultPolicy _policy;
    private IClock _clock;
    public DefaultCheckoutService(InMemoryRepository repo, DefaultPolicy policy, IClock clock)
    {
        _repo = repo;
        _catalog = new Catalog(repo);
        _policy = policy;
        _clock = clock;
    }
    
    public Catalog GetCatalog()
    {
        return _catalog;
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