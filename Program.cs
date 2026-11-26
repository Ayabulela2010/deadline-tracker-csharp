using DeadlineTracker.Console; // Import the namespace where Assignment and Status are defined. 

// Object initialiser: creates the object and sets its properties in one go.
var assignment = new Assignment
{
    Id = 1,
    Title = "Assignment 1",
    ModuleCode = "IFM03B3",
    DueDate = new DateTime(2026, 12, 1) // year, month, day
};

// $"..." is string interpolation: anything in {} is replaced by its value at runtime.
Console.WriteLine($"{assignment.Title} ({assignment.ModuleCode}) is due {assignment.DueDate:dd MMM yyyy}");
Console.WriteLine($"Overdue? {assignment.IsOverdue}"); // Uses the IsOverdue property to check if the assignment is overdue.