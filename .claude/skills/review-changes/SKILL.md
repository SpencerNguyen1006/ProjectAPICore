---
name: review-changes
description: Review uncommitted git changes for bugs, null handling, secrets, and CLAUDE.md rule violations. Only runs when the user invokes it. Reports a short list of findings, each with a suggested fix.
disable-model-invocation: true
allowed-tools: Bash(git status:*), Bash(git diff:*), Bash(git log:*), Read, Grep, Glob
---

# Review uncommitted changes

Review the work the user has not committed yet. Do not edit any files. Report findings and suggested fixes, then stop and let the user decide what to apply.

## Steps

1. **Check that this is a git repository.** Run `git status --short`. If git reports "not a git repository", stop and tell the user. Do not run `git init`.

2. **Collect the changes.** Run these and read the output:
   - `git status --short` for the list of changed, staged, and untracked files.
   - `git diff` for unstaged edits.
   - `git diff --cached` for staged edits.
   - For untracked files (`??` in the status output), read them directly, because `git diff` does not show them.
   - Skip `bin/`, `obj/`, `.vs/`, and `notes-feature-react/node_modules/`.

3. **Read the rules.** Read `CLAUDE.md` in the repo root and check the changes against it. The rules that matter most here:
   - Never commit connection strings, passwords, or secrets. Local values belong in `appsettings.Development.json` (gitignored) or user secrets.
   - The connection string in `appsettings.json` is the shared value for the author's machine. Only flag it if the diff changes it or adds a second one.
   - Ask before creating or applying a database migration. A new file under a `Migrations/` folder, or a model or DbContext change with no migration, is a finding.
   - Keep the README current when features change.
   - Notes have create and list only. Do not flag missing update or delete endpoints as bugs.
   - No authentication exists yet. Do not flag that as a new bug unless the diff makes it worse.

4. **Check each changed file for:**
   - **Bugs:** logic errors, wrong status codes, off-by-one errors, unawaited async calls, missing `SaveChangesAsync`, wrong query filters (for example, a note query that drops `ProjectId`, `TypeName`, or `EntityId`), and React state or effect mistakes.
   - **Null handling:** nullable reference types that are dereferenced without a check, `FirstOrDefault` results used without a null check, and request body fields that can be null and are not validated. For React, look for props or fetch results that are read before they exist.
   - **Security:** hard-coded secrets, passwords, tokens, or connection strings; secrets in logs or error responses; files written with user-supplied paths; and SQL built by string concatenation.
   - **Rule violations:** anything from step 3.

5. **Keep the review focused.** Report only findings you can point to in the diff, with a file and line. Do not report style preferences, speculative problems, or things that were already in the code before these changes, unless the change makes them worse. If there are no real findings, say so.

## Output format

Start with one line: how many files you reviewed and whether the changes are staged, unstaged, or untracked.

Then list findings, most serious first, at most 8. Use this format for each:

```
1. [severity] file.cs:42 - What is wrong, in one sentence.
   Fix: The suggested change, in one or two sentences.
```

Severity is one of `critical` (secret exposed, data loss, or security hole), `high` (bug or broken behavior), `medium` (null-handling gap or rule violation), or `low` (cleanup).

End by asking whether the user wants you to apply any of the fixes. Do not apply them without a yes.
