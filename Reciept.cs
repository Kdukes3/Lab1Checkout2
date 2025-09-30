namespace Lab1Checkout2;

public class Receipt
{
    public string Message { get; set; }
    public string ItemId { get; set; }
    public DateTime Timestamp { get; set; }

    public Receipt(string message, string itemId, DateTime timestamp)
    {
        Message = message;
        ItemId = itemId;
        Timestamp = timestamp;
    }
}