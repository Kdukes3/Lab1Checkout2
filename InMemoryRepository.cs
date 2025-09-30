namespace Lab1Checkout2;

public class InMemoryRepository : IRepository
{
    //Dictionary for Item inventory and Lists for items and records
    private Dictionary<string, Item> _inventory = new Dictionary<string, Item>();
    private List<CheckoutRecord> _records = new List<CheckoutRecord>();
    List<Item> _items = new List<Item>();

    public void SaveItem(Item item)
    {
        _inventory[item.Id] = item;
        //_items.Add(item);
    }

    public Item GetItem(string itemId)
    {
        return _inventory[itemId];
    }

    public List<Item> AllItems()
    {
        foreach (var item in _inventory.Values)
        {
            _items.Add(item);
        }

        return _items;
    }

    public void SaveRecord(CheckoutRecord record)
    {
        _records.Add(record);
    }

    public CheckoutRecord GetActiveRecordFor(string itemId)
    {
        if (_records.Exists(r => r.ItemId == itemId))
        {
            return _records.First(r => r.ItemId == itemId);
        }
        
        return null;
    }

    public List<CheckoutRecord> AllRecords()
    {
        return _records;
    }
}