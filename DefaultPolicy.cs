namespace Lab1Checkout2;

public class DefaultPolicy
{
    private IClock _clock;
    private int _maxTime;
    
    public DefaultPolicy(IClock clock, int i)
    {
        _clock = clock;
        _maxTime = i;
    }
    
    public bool CanCheckout(Item item)
    {
        return item != null && item.Status == ItemStatus.AVAILABLE;
    }
    
    public DateTime NormalizeDueDate(DateTime proposed)
    {
        var now = _clock.Today();
        if (proposed <= now) proposed = now.AddDays(1); 
        DateTime cap = now.AddDays(_maxTime);
        if (proposed > cap) proposed = cap;            
        return proposed;
    }
    //Denies checkout request if item is lost
}