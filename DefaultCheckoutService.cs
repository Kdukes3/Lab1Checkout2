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
        
        Receipt r1 = new Receipt(message, itemId, dueDate);
        return r1;
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