namespace Lab1Checkout2;

public interface ICatalog
{
    List<Item> ListAvailable();
    List<Item> ListUnavailable();
    Item FindById(string itemId);
}