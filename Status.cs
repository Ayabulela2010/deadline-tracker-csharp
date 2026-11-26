namespace DeadlineTracker.Console; // Same as package in Java.

// Fixed set of possible statuses.
public enum Status
{
    NotStarted, // Default: nothing done yet.
    InProgress, // Currently working on it.
    Done // Finished and handed in.
}