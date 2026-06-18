# OakdaleRolbal

Initial foundation for the Oakdale Rolbal platform.

## Tech Stack

- Backend: ASP.NET Core Web API on .NET 10, ASP.NET Identity, JWT bearer auth, MediatR, FluentValidation, EF Core, PostgreSQL provider
- Frontend: React 19, Next.js 16 App Router, TypeScript, Tailwind CSS, shadcn/ui

## Solution Layout

- `Backend/OakdaleRolbal.Api`
- `Backend/OakdaleRolbal.Application`
- `Backend/OakdaleRolbal.Domain`
- `Backend/OakdaleRolbal.Infrastructure`
- `Backend/OakdaleRolbal.Persistence`
- `Frontend/oakdalerolbal-web`

## Run Backend

```powershell
dotnet restore .\OakdaleRolbal.sln
dotnet run --project .\Backend\OakdaleRolbal.Api\OakdaleRolbal.Api.csproj
```

Backend URLs are shown in console output.
The API creates its database schema automatically on first run.

## Run Frontend

```powershell
cd .\Frontend\oakdalerolbal-web
npm install
npm run dev
```

Open `http://localhost:3000`.

Create `Frontend\oakdalerolbal-web\.env.local` from `Frontend\oakdalerolbal-web\.env.example` when you want to override the default API URL.

## Auth Endpoints

- `POST /api/auth/register`
- `POST /api/auth/login`
- `GET /api/auth/me` with `Authorization: Bearer <token>`

## Checks

```powershell
dotnet build .\OakdaleRolbal.sln
cd .\Frontend\oakdalerolbal-web
npm run lint
npm run build
```

