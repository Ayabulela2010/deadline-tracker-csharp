// Allows use of Assignment and Status classes without needing to specify the namespace.
using DeadlineTracker.Console;

var assignments = new List<Assignment>(); // Store all assignments in a list.
var nextId = 1; // counter to give each assignment a unique id, start at 1.
var running = true; // Controls the menu loop. Setting to false ends program.

// The main loop: Show the menu, read a choice, act on it and repeat.
while(running)
{
    ShowMenu();
    var choice = Console.ReadLine(); // Read user input from the console.

    // Pick a segment of code to run based on the user's choice.
    switch (choice)
    {
        case "1":
            AddAssignment();
            break;
        case "2":
            ListAssignments();
            break;
        case "0":
            running = false;
            break;
        default:
            Console.WriteLine("Invalid choice, try again.");
            break;
    }
}

Console.WriteLine("Goodbye!"); // Program is ending.

// ------------------------ Local functions ------------------------

// Prints the menu options.
void ShowMenu()
{
    Console.WriteLine();
    Console.WriteLine("===Deadline Tracker ===");
    Console.WriteLine("1. Add assignment");
    Console.WriteLine("2. List assignments");
    Console.WriteLine("0. Exit");
    Console.Write("Choose an option: ");
}

// Ask the user for details and adds a new assignment to the list.
void AddAssignment()
{
    Console.Write("Title: ");
    var title = Console.ReadLine() ?? string.Empty; // Read title, default to empty string if null.

    Console.Write("Module code: ");
    var moduleCode = Console.ReadLine() ?? string.Empty; // Read module code, default

    // declared first so the out parameter can fill it in the TryParse method.
    DateTime dueDate;
    Console.Write("Due date (yyyy-MM-dd): ");

    // TryParse converts the string to a DateTime, returns true if successful, false if not.
    // "out" stores the converted value.
    while (!DateTime.TryParse(Console.ReadLine(), out dueDate))
    {
        Console.Write("Invalid date. Use yyyy-MM-dd: ");
    }

    assignments.Add(new Assignment
    {
        Id = nextId++, // use current value and then increment for next assignment.
        Title = title,
        ModuleCode = moduleCode,
        DueDate = dueDate,
    });

    Console.WriteLine("assignment added.");
}

// Prints every assignment, soonest due date first.
void ListAssignments()
{
    // Any() is a LINQ method: true if the list has at least one item.
    if (!assignments.Any())
    {
        Console.WriteLine("No assignments yet.");
        return;
    }

    // OrderBy is a LINQ method: sorts the list by the DueDate property.
    foreach (var a in assignments.OrderBy(a => a.DueDate))
    {
        // A ternary expression: condition ? valueIfTrue : valueIffalse.
        var overdueFlag = a.IsOverdue ? " [OVERDUE]" : "";
        Console.WriteLine($"#{a.Id} {a.Title} ({a.ModuleCode}) - due {a.DueDate:dd MMM yyyy} - {a.Status}{overdueFlag}");
    }
}

