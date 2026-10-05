# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

A notes feature: an ASP.NET Core 8 Web API backed by SQL Server (EF Core), plus a separate Vite + React 19 frontend in `notes-feature-react/`. Notes can be attached to any record in a project and can have files linked to them. The project is early-stage; see "Current state" below before assuming something exists.

## Commands

Backend (run from the repo root; there is no `.sln`, only `ProjectAPICore.csproj`):

```powershell
dotnet build
dotnet run                                  # https://localhost:7012, http://localhost:5164
dotnet ef migrations add <Name>             # after changing a model or the DbContext
dotnet ef database update                   # apply migrations to the local database
```

`dotnet ef` needs the global tool (`dotnet tool install --global dotnet-ef`); there is no local tool manifest.

Frontend (run from `notes-feature-react/`):

```powershell
npm run dev        # Vite dev server
npm run build
npm run lint       # ESLint, JS/JSX only
```

There are no tests in either project.

## Architecture

**Backend.** `Program.cs` registers MVC controllers-with-views and `ApplicationDbContext` (SQL Server, connection string `DefaultConnection` in `appsettings.json`). Routing has two styles side by side:

- API controllers (`NotesController`, `NoteFilesController`) use attribute routing under `api/[controller]` and inject `ApplicationDbContext` directly. There is no service or repository layer, and entities are used as request and response bodies (no DTOs).
- `HomeController` and `Views/` are the untouched MVC template (conventional `{controller}/{action}/{id?}` route). They are not part of the feature.

**Data model** (`Models/`, `Data/ApplicationDbContext.cs`):

- `Note` is polymorphic: `ProjectId` + `TypeName` + `EntityId` identify the record a note belongs to. None of the three is a foreign key, and no Project or entity tables exist. `GET api/notes/{projectId}/{noteType}/{entityId}` queries on exactly this triple.
- `Note.ModifiedBy` is the foreign key to `User` (mapped with `[ForeignKey("ModifiedBy")]`, navigation `ModifiedByUser`).
- `NoteFile` is the join table between `Note` and `FileRepository` (file metadata: path, original name, upload date).
- `Models/FileRepository.cs` is an entity, not a repository-pattern class.

All foreign keys cascade on delete. Relationships are configured only through conventions and attributes; `ApplicationDbContext` has no `OnModelCreating`.

**Frontend.** `notes-feature-react/` is a standalone Vite app with its own `package.json`. It is not served or built by the ASP.NET project, so in development it runs on a different origin from the API.

## Current state

- `App.jsx` renders a heading only; it does not call the API yet.
- The backend has no CORS policy, so the Vite dev server cannot call it until one is added (or a Vite proxy is configured).
- No authentication: `app.UseAuthorization()` is present but nothing is protected, and `ModifiedBy` comes from the request body.
- No file upload endpoint and nothing creates `FileRepository` or `User` rows; `POST api/notefiles` only links existing ids.
- Notes have create and list only (no update or delete).

## Environment notes

- The real connection string lives in `appsettings.Development.json`, which is gitignored. `appsettings.json` keeps `DefaultConnection` empty so the repo never contains the server name or database name. On each machine, put the connection string in your local `appsettings.Development.json`.
- The directory is not a git repository and has no root `.gitignore`; `bin/`, `obj/`, `.vs/` and `notes-feature-react/node_modules/` are build output and dependencies, so exclude them from searches.
- `bin/Debug/net6.0` and `obj/Debug/net6.0` are leftovers from before the project moved to `net8.0`.

## Purpose of this project

This is a practice project for reviewing C#, Web API design, and SQL,
and for learning React. It doubles as a public portfolio piece for
full-stack developer roles.

## How to work with me

- I'm practicing, so explain the _why_ behind code, not just the code.
  Keep explanations short and tie new C# ideas to Java/Groovy when helpful.
- Prefer small steps I can type and understand. Offer to let me write
  the code first and review it, rather than generating everything.
- Occasionally ask me a quick interview-style question about what we
  just built.
- Point out better practices (DTOs, validation, error handling,
  async/await, naming) as we go.

## Session style

- I usually work in short evening sessions of 1 to 2 hours.
- Break work into small tasks that can finish in one session.
- End each task in a working, committable state, and suggest a clear
  commit message in conventional format (feat:, fix:, docs:).

## Portfolio and recruiter visibility

- Keep README.md current: what the project does, tech stack, how to run
  it, and a short list of features and what's next.
- Write clean, readable code and sensible commit history, since
  recruiters read both.
- Keep this AI setup (CLAUDE.md, skills, settings) in the repo and
  mention in the README how Claude Code is used in the workflow.
  Describe it accurately and only claim what I've actually built.

## Rules

- Never commit connection strings, passwords, or secrets. This repo
  is public. Use appsettings.Development.json (gitignored) or user secrets.
- Ask before creating or applying a database migration.
