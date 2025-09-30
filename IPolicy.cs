namespace Lab1Checkout2;

public interface IPolicy
{
    bool CanCheckout(Item item);
    DateTime NormalizeDueDate(DateTime proposed);
}