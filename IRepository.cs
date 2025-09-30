namespace Lab1Checkout2;

public interface IRepository
{
    void SaveItem(Item item);
    Item GetItem(string itemId);
    List<Item> AllItems();
    void SaveRecord(CheckoutRecord record);
    CheckoutRecord GetActiveRecordFor(string itemId);
    List<CheckoutRecord> AllRecords();
}