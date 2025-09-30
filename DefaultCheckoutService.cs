namespace Lab1Checkout2;

public class DefaultCheckoutService : ICheckoutService
{
    private IRepository _repo;
    private Catalog _catalog;
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

    public Receipt Checkout(string message, string itemId, Borrower borrower, DateTime dueDate)
    {
        
        Receipt r1 = new Receipt(message, itemId, borrower, dueDate);
        return r1;
    }

    public Receipt ReturnItem(string itemId)
    {
        _repo.GetItem(itemId).Status = ItemStatus.AVAILABLE;
        //Possibly implement a removal of dueDate
        
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