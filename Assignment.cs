namespace DeadlineTracker.Console;

// Represents a single assignment or deadline to be tracked.
public class Assignment
{
    // Unique identifier for the assignment.
    public int Id { get; set; } // allow C# to create getter and setters.
    public string Title { get; set; } = string.Empty; // Field for assignment name initialised to Empty as for safe default value.
    public string ModuleCode { get; set; } = string.Empty; // eg., Informatics = IFM03B3.
    public DateTime DueDate { get; set; } // When is the assignment due.
    public Status Status { get; set; } = Status.NotStarted; // Current progress. Initial value is NotStarted. {From the Status enum}.
    public bool IsOverdue => Status != Status.Done && DueDate.Date < DateTime.Today; // Assignment overdue if the status is not done and its past due date.

}