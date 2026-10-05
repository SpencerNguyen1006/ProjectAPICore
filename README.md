# ProjectAPICore

A notes feature for records in a project. Notes attach to any record through a project ID, a record type, and an entity ID. The API is an ASP.NET Core 8 Web API backed by SQL Server, and a separate React frontend is in progress.

This is an early-stage practice project for reviewing C#, Web API design, and SQL, and for learning React.

## Tech stack

- **Backend:** ASP.NET Core 8 Web API, Entity Framework Core 8, SQL Server
- **Frontend:** React 19, Vite 8, ESLint
- **Development workflow:** Claude Code, with the project rules in `CLAUDE.md`

## Features

- Create a note for a record (`POST api/notes`)
- List the notes for a record (`GET api/notes/{projectId}/{noteType}/{entityId}`)
- Link an existing file to a note (`POST api/notefiles`)

Not built yet: note update and delete, file upload, authentication, CORS for the frontend, and any UI beyond a placeholder heading.

## Getting started

### Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB, Express, or a full instance)
- Node.js (for the frontend)
- The EF Core CLI: `dotnet tool install --global dotnet-ef`

### Backend

1. Create `appsettings.Development.json` in the project root. This file is gitignored, so your connection string stays off GitHub:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR-SERVER;Database=ProjectAPICoreDb;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```

2. Apply the migrations to your database:

   ```powershell
   dotnet ef database update
   ```

3. Run the API:

   ```powershell
   dotnet run
   ```

   The API listens on `https://localhost:7012` and `http://localhost:5164`.

`appsettings.json` keeps `DefaultConnection` empty on purpose, so the repo never contains a server name or credentials.

### Frontend

```powershell
cd notes-feature-react
npm install
npm run dev
```

## Project layout

```
Controllers/   API controllers (NotesController, NoteFilesController)
Models/        Entities: Note, NoteFile, FileRepository, User
Data/          ApplicationDbContext (EF Core)
Migrations/    EF Core migrations
notes-feature-react/   Vite + React frontend
CLAUDE.md      Instructions for Claude Code
```

## How Claude Code is used

Claude Code helps with this project. `CLAUDE.md` holds the working rules, such as never committing secrets and asking before creating migrations. The `.claude/` folder holds the shared settings and a `review-changes` skill that reviews uncommitted work against those rules. Personal settings (`.claude/settings.local.json` and `CLAUDE.local.md`) are gitignored.

## What's next

- Add a DTO layer and input validation to the API
- Add note update and delete endpoints
- Connect the React frontend to the API, including the CORS or Vite proxy setup
- Add file upload and a `FileRepository` workflow
- Add authentication, so `ModifiedBy` is no longer taken from the request body
