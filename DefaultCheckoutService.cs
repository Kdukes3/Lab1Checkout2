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

    public void Checkout(string message, string itemId, Borrower borrower, DateTime dueDate)
    {

        var r1 = _repo.GetItem(itemId);
        if (r1 == null)
        {
            throw new InvalidOperationException("Item '" + itemId + "' not found.");
        }

        if (!_policy.CanCheckout(r1))
        {
            throw new InvalidOperationException("Item '" + itemId + "' is not available for checkout.");
        }
        
        var normalized = _policy.NormalizeDueDate(dueDate);
        r1.Status = ItemStatus.CHECKED_OUT;
        _repo.SaveItem(r1);
        
        var record = new CheckoutRecord(r1.Id, borrower, _clock.Today(), normalized);
        _repo.SaveRecord(record);
        
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