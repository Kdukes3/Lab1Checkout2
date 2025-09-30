namespace Lab1Checkout2;

public interface INotifier
{
    void DueSoon(Borrower borrower, CheckoutRecord record);
    void Overdue(Borrower borrower, CheckoutRecord record);
}