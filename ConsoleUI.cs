namespace Lab1Checkout2;

public static class ConsoleUI
{
    private static ICheckoutService _service;
    private static ICatalog _catalog;
    private static INotifier _notifier;
    private static IClock _clock;
    private static InMemoryRepository _repo;

    //program starts running
    public static void Run() 
    {
        _clock = new SystemClock();
        _repo = new InMemoryRepository();
        var policy = new DefaultPolicy(_clock, 14);
        _service = new DefaultCheckoutService(_repo, policy, _clock);
        _catalog = _service.GetCatalog();
        _notifier = new LogNotifier();

        while (true) //Main loop for user
        {
            ShowMenu();
            Console.Write("Select a number: ");
            string user_input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(user_input)) continue;
            if (user_input == "0")
            {
                break;
            }

            try
            {
                //Deciding option for menu
                switch (user_input)
                {
                    case "1": AddItems(); break;
                    case "2": ListAvailable(); break;
                    case "3": ListUnavailable(); break;
                    case "4": DoCheckout(); break;
                    case "5": DoReturn(); break;
                    case "6": ShowDueSoon(); break;
                    case "7": ShowOverdue(); break;
                    case "8": MarkLost(); break;
                    default: Console.WriteLine("Invalid input. Try again."); break;
                }
            }
            catch (Exception ex) //For catching error within switch
            {
                Console.WriteLine("Error " + ex.Message);
            }
        }
    }

    public static void ShowMenu() //Print main menu
    {
        Console.WriteLine("1) Add items to inventory");
        Console.WriteLine("2) List available items");
        Console.WriteLine("3) List unavailable items");
        Console.WriteLine("4) Check out item");
        Console.WriteLine("5) Return item");
        Console.WriteLine("6) Due soon (next 24h)");
        Console.WriteLine("7) Overdue");
        Console.WriteLine("8) Mark item LOST");
        Console.WriteLine("0) Exit");
    }

    private static string user_Prompt(string label) //Check for non-empty inputs
    {
        Console.Write(label);
        while (true)
        {
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input)) return input.ToString();
        }
    }

    private static void AddItems() //Menu option 1
    {
        Console.WriteLine("Add items to inventory");
        while (true)
        {
            Console.WriteLine("Enter each field on its own line: ID, Name, Category, Condition");
            var id = user_Prompt("ID: ");
            var name = user_Prompt("Name: ");
            var category = user_Prompt("Category: ");
            var condition = user_Prompt("Condition: ");
            

            var contin = user_Prompt("Continue? [Y/n]: ");
            if (contin == "y")
            {
                _repo.SaveItem(new Item(id, name, category, condition,
                                ItemStatus.AVAILABLE));
                continue;
            }
            if (contin == "n")
            {
                _repo.SaveItem(new Item(id, name, category, condition,
                    ItemStatus.AVAILABLE));
                break;
            }
        }
    }

    private static void ListAvailable() //Menu option 2
    {
        Console.WriteLine("List of available items");
        var items = _catalog.ListAvailable();
        if (items.Count == 0)
        {
            Console.WriteLine("No items found");
            return;
        }

        foreach (var i in items)
        {
            Console.WriteLine(i.Name);
        }
    }

    private static void ListUnavailable() //Menu option 3
    {
        Console.WriteLine("List of unavailable items");
        var items = _catalog.ListUnavailable();
        if (items.Count == 0)
        {
            Console.WriteLine("No items found");
            return;
        }

        var lost = new List<Item>();
        var outed = new List<Item>();
        foreach (var i in items)
        {
            if (i.Status == ItemStatus.LOST) lost.Add(i);
            else if (i.Status == ItemStatus.CHECKED_OUT) outed.Add(i);
        }

        if (lost.Count > 0)
        {
            Console.WriteLine("LOST:");
            foreach (var i in lost) Console.WriteLine(i.Name);
        }

        if (outed.Count > 0)
        {
            Console.WriteLine("CHECKED_OUT:");
            foreach (var i in outed) Console.WriteLine(i.Name);
        }
    }


    private static void DoCheckout() //Menu option 4
    {
        Console.WriteLine("Check out item");
        var id = user_Prompt("Item ID: ");
        var name = user_Prompt("Your name: ");
        var email = user_Prompt("Your email: ");
        var dueText = user_Prompt("Due date: ");
        _repo.GetItem(id).Status = ItemStatus.CHECKED_OUT;
        DateTime dueDate = DateTime.Parse(dueText);
        Console.WriteLine("-> Your receipt:");
        Console.WriteLine(DateTime.Now + "| " +" Checkout " +  "| " + dueDate);
    }

    private static void DoReturn() //Menu option 5
    {
        Console.WriteLine("Return item");
        var id = user_Prompt("Item ID: ");
        var r = _service.ReturnItem(id);
        Console.WriteLine("-> Your receipt:");
        Console.WriteLine(r.ToString());
    }

    private static void ShowDueSoon() //Menu option 6
    {
        Console.WriteLine("Due soon (next 24h)");
        var soon = _service.FindDueSoon(TimeSpan.FromHours(24));
        if (soon == null)
        {
            Console.WriteLine("Due soon not found");
            return;
        }

        foreach (var r in soon)
        {
            _notifier.DueSoon(r.Borrower, r);
        }
    }

    private static void ShowOverdue() //Menu option 7
    {
        Console.WriteLine("Overdue");
        var overdue = _service.FindOverdue();
        if (overdue == null)
        {
            Console.WriteLine("Overdue not found");
            return;
        }

        foreach (var r in overdue)
        {
            _notifier.Overdue(r.Borrower, r);
        }
    }

    private static void MarkLost()//Menu option 8
    {
        Console.WriteLine("Mark lost LOST");
        var id = user_Prompt("Item ID: ");
        _service.MarkLost(id.ToString());
        Console.WriteLine("Item marked lost");
    }
}
