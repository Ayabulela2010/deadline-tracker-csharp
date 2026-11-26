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
        case "3":
            MarkAsDone();
            break;
        case "4":
            DeleteAssignment();
            break;
        case "5":
            ListDueThisWeek();
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
    Console.WriteLine("3. Mark assignment as done");
    Console.WriteLine("4. Delete assignment");
    Console.WriteLine("5. Show due this week");
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

    // TryPasrseExact returns true if the input string can be converted to a DateTime using the specified format.
    // CultureInfo.InvariantCulture means "use the same rules for all computers, don't rely on local settings".
    while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd",
           System.Globalization.CultureInfo.InvariantCulture,
           System.Globalization.DateTimeStyles.None, out dueDate))
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
        PrintAssignment(a);
    }
}

void PrintAssignment(Assignment a)
{
    var overdueFlag = a.IsOverdue ? " [OVERDUE]" : "";
    Console.WriteLine($"#{a.Id} {a.Title} ({a.ModuleCode}) - due {a.DueDate:dd MMM yyyy} - {a.Status}{overdueFlag}");
}

// Asks for an Id and looks up the matching assignment.
// Returns null if the input isn't a number or no assignment has that ID.
// "Assignment?" mean "An Assignment or null" (nullable type).
Assignment? AskForAssignment()
{
    Console.Write("Enter assignment Id: ");

    // Convert string Id to int: return true/false
    // hands the converted value to the out parameter "id".
    if (!int.TryParse(Console.ReadLine(), out var id))
    {
        Console.WriteLine("That is not a valid number.");
        return null;
    }

    // FirstOrDefault is LINQ: returns the first item that matches the condition, or null if none match.
    var assignment = assignments.FirstOrDefault(a => a.Id == id);

    if (assignment == null)
    {
        Console.WriteLine("No assignment with that Id.");
    }
    return assignment;
}

// Marks an assignment as done.
void MarkAsDone()
{
    var assignment = AskForAssignment();
    if (assignment == null) return; // nothing found, stop here.

    assignment.Status = Status.Done; // Change assignment status.
    Console.WriteLine("Marked as done.");
}

// Removes an assignment from the list.
void DeleteAssignment()
{
    var assignment = AskForAssignment();
    if (assignment == null) return;

    assignments.Remove(assignment);
    Console.WriteLine("Assignment deleted.");
}

// Shows unfinished assignments due in the next 7 days.
void ListDueThisWeek()
{
    // DateTime. Today is today's date at midnight (no time part).
    var today = DateTime.Today;
    var nextWeek = today.AddDays(7);

    // Where is LINQ's filter. Only items where the condition is true are kept.
    var dueSoon = assignments
        .Where(a => a.Status != Status.Done
                    && a.DueDate.Date >= today
                    && a.DueDate.Date <= nextWeek)
        .OrderBy(a => a.DueDate)
        .ToList(); // ToList run the query and stores the result.

    if (!dueSoon.Any())
    {
        Console.WriteLine("No assignments due within the next 7 days.");
        return;
    }

    foreach (var a in dueSoon)
    {
        PrintAssignment(a);
    }
}



