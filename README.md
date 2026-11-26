# Deadline Tracker (C#)

A console app for tracking module assignments and due dates.
Built as a hands-on way to learn C# and .NET.

## Status
**Phase 1 complete:** console app with in-memory storage (`v0.1-console`).
Next: saving data to SQL Server with Entity Framework Core.

## Features
- Add assignments with a title, module code and due date
- List assignments sorted by due date, with overdue ones flagged
- Mark assignments as done
- Delete assignments
- Show assignments due in the next 7 days

## Roadmap
- [x] Phase 1: Console app, in-memory (classes, enums, LINQ)
- [ ] Phase 2: SQL Server + Entity Framework Core

## What I'm learning
See [docs/learning-log.md](docs/learning-log.md).

## Running locally
1. Install the .NET 10 SDK
2. Clone the repo
3. In the project folder, run `dotnet run`

Note: data is lost when the program closes until Phase 2 adds the database.
