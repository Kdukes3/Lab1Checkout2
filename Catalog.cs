namespace Lab1Checkout2;

public class Catalog : ICatalog
{
    private IRepository _repo;

    public Catalog(IRepository repo)
    {
        _repo = repo;
    }
    public List<Item> ListAvailable()
    {
        var result = new List<Item>();
        foreach (var i in _repo.AllItems())
        {
            if (i.Status == ItemStatus.AVAILABLE) result.Add(i);
        }
        return result;
    }

    public List<Item> ListUnavailable()
    {
        var result = new List<Item>();
        foreach (var i in _repo.AllItems())
        {
            if (i.Status != ItemStatus.AVAILABLE) result.Add(i);
        }
        return result;
    }

    public Item FindById(string itemId)
    {
        return _repo.GetItem(itemId);
    }
    
}