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
        var item = _repo.GetItem(itemId);
        var rec = _repo.GetActiveRecordFor(itemId);
        rec.ReturnDate = _clock.Today();
        _repo.SaveRecord(rec);
        item.Status = ItemStatus.AVAILABLE;
        _repo.SaveItem(item);
        return new Receipt("Return", item.Id, _clock.Today());
    }

    public void MarkLost(string itemId)
    {
        var item = _repo.GetItem(itemId);
        item.Status = ItemStatus.LOST;
        _repo.SaveItem(item);
    }

    public List<CheckoutRecord> ListActiveLoans()
    {
        List<CheckoutRecord> active = new List<CheckoutRecord>();
        
        foreach (var a in _repo.AllRecords())
        {
            if (a.ReturnDate < DateTime.Today)
            {
                active.Add(a);
            }
        }
        return active;
    }

    public List<CheckoutRecord> FindDueSoon(TimeSpan window)
    {
        var now = _clock.Today();
        var upper = now.Add(window);
        var list = new List<CheckoutRecord>();
        foreach (var r in ListActiveLoans())
        {
            if (r.DueDate >= now && r.DueDate <= upper) list.Add(r);
        }
        return list;
    }
    public List<CheckoutRecord> FindOverdue()
    {
        var now = _clock.Today();
        var list = new List<CheckoutRecord>();
        foreach (var r in ListActiveLoans())
        {
            if (r.DueDate < now) list.Add(r);
        }
        return list;
    }

}