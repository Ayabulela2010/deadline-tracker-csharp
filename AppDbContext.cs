// Gives us DbContext, DbSet and the UseSqlServer method.
using Microsoft.EntityFrameworkCore;

namespace DeadlineTracker.Console;

// DbContext is the connection to the database. You ask it for data and tell it to save changes.
public class AppDbContext : DbContext
{
    // Each DbSet represents a table in the database.
    // Create a table called Assignments with a single column per property.
    // on the assignment class (Id, Title, ModuleCode, DueDate, Status).
    public DbSet<Assignment> Assignments { get; set; }

    // EF Core calls this to find out how to connect to the database.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // The @ before the string means backslashes are literal,
        // so we can write SQLEXPRESS06 without escaping the "\".
        // Trusted_Connection=True logs in with my Windows account (no password needed).
        // TrustServerCertificate=True avoids certificate errors on a local server.
        options.UseSqlServer(@"Server=localhost\SQLEXPRESS06;Database=DeadlineTrackerDb;Trusted_Connection=True;TrustServerCertificate=True");
    }
}