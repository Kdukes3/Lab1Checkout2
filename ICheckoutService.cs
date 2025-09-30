namespace Lab1Checkout2;

public interface ICheckoutService
{
    Catalog GetCatalog();
    void Checkout(string message, string itemId, Borrower borrower, DateTime dueDate);
    Receipt ReturnItem(string itemId);
    void MarkLost(string itemId);
    List<CheckoutRecord> ListActiveLoans();
    List<CheckoutRecord> FindDueSoon(TimeSpan window);
    List<CheckoutRecord> FindOverdue();
}