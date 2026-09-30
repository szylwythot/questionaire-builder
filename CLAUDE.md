# Questionnaire Builder

Mini Google Forms: build, view and list questionnaires. Portfolio project for a .NET + React role.

## TL;DR

- `api/` = ASP.NET Core 10 Minimal API + EF Core 10 + SQLite
- `web/` = React 19 + TypeScript + Vite + MUI v9
- Frontend calls relative `/api/...` URLs. Vite proxies them to `http://localhost:5132`.

## Commands

API (run from `api/`):

```bash
dotnet run                                # http://localhost:5132
dotnet build
dotnet ef migrations add <Name>           # after any model change
dotnet ef database update                 # creates/updates questionnaires.db
```

First time only: `dotnet tool install --global dotnet-ef`

Web (run from `web/`):

```bash
npm install
npm run dev                               # http://localhost:5173
npm run build                             # type-check + bundle
npm run lint
```

Start the API before the web app, or `/api` calls fail.

## Where things live

| What | Where |
|---|---|
| Endpoints | `api/Program.cs` |
| EF entities | `api/Models/` |
| DbContext | `api/Data/AppDbContext.cs` |
| Migrations | `api/Migrations/` (generated, do not hand-edit) |
| React entry | `web/src/main.tsx` -> `web/src/App.tsx` |
| Requirements | `requirements.md` |
| Build order | `implementation_plan.md` (Builder -> Viewer -> Dashboard) |

## Naming

- **C#**: PascalCase for classes, methods, properties (`GetQuestionnaire`, `CreatedAt`)
- **TypeScript**: camelCase for functions and variables (`fetchQuestionnaire`, `isSaving`)
- **React components**: PascalCase (`QuestionEditor`)
- **JSON over the wire**: camelCase. ASP.NET serializes this by default, so TS types use camelCase fields.

## Backend rules

- Prefix every route with `/api`.
- Group endpoints per resource with `app.MapGroup("/api/questionnaires")`.
- Use DTO records for requests and responses. Do not return EF entities directly (navigation properties cause JSON cycles).
- Use `async` EF calls (`ToListAsync`, `FindAsync`, `SaveChangesAsync`).
- Return typed results: `Results.Ok`, `Results.Created`, `Results.NotFound`, `Results.ValidationProblem`.
- After a model change, add a migration and run `dotnet ef database update`.
- Never edit an applied migration.
- `*.db` is gitignored. A fresh clone needs `dotnet ef database update`.
- Delete the `weatherforecast` template code when adding the first real endpoint.

## Frontend rules

- Match existing style: single quotes, no semicolons.
- Never hardcode `localhost` or add CORS. The Vite proxy handles it.
- TypeScript `strict` is on (`web/tsconfig.app.json`). Do not turn it off to silence errors.
- No `any`. Define types for API data in one place and reuse them.
- Keep API calls out of components. Put them in small functions (e.g. `web/src/api/`).
- MUI: named imports from `@mui/material`. Use `sx` for one-off styles.
- MUI: use `slots` / `slotProps`. Never use the removed `components` / `componentsProps` props.
- If unsure about an MUI v9 prop, check the docs. Do not guess from v4/v5 examples.
  - Component API: `https://mui.com/material-ui/api/<component>/` (e.g. `https://mui.com/material-ui/api/text-field/`)
  - Docs index for AI tools: `https://mui.com/material-ui/llms.txt`
  - The page header must show v9. Older-version pages exist and look similar.
- Every form input needs a visible label. The app is all forms, so accessibility matters.

## How to work

- Before a feature, give a 3-step plan. Wait for a "go".
- Do one phase from `implementation_plan.md` at a time. Backend endpoint first, then UI.
- Keep changes small.
- Keep functions under ~40 lines.
- If a request is unclear, ask one focused question.
- Verify before saying "done": `dotnet build` for API changes, `npm run build` + `npm run lint` for web changes.
- Explain *why* for non-obvious code. The user must be able to defend every line in an interview.

## Tests

None yet. When added:

- API: xUnit + `WebApplicationFactory` integration tests in `api.Tests/`
- Web: Vitest + React Testing Library, next to the component
