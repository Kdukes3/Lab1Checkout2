namespace Lab1Checkout2;

public class SystemClock : IClock
{
    public DateTime Today()
    {
        return DateTime.Now;
    }
}