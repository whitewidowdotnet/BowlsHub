# OakdaleRolbal

Initial foundation for the Oakdale Rolbal platform.

## Tech Stack

- Backend: ASP.NET Core Web API, MediatR, FluentValidation, EF Core, PostgreSQL provider
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

## Run Frontend

```powershell
cd .\Frontend\oakdalerolbal-web
npm install
npm run dev
```

Open `http://localhost:3000`.

## Checks

```powershell
dotnet build .\OakdaleRolbal.sln
cd .\Frontend\oakdalerolbal-web
npm run lint
npm run build
```

