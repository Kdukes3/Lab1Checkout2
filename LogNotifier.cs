namespace Lab1Checkout2;

public class LogNotifier : INotifier
{
    public void DueSoon(Borrower borrower, CheckoutRecord record) => Print(borrower, record);
    public void Overdue(Borrower borrower, CheckoutRecord record) => Print(borrower, record);
    private static void Print(Borrower b, CheckoutRecord r)
        => Console.WriteLine($"Item ID: {r.ItemId} | Borrowed)by {b.Name} | {b.Email}");
}