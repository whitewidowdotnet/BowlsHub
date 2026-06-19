# BowlsLive

Initial foundation for the BowlsLive platform.

## Tech Stack

- Backend: ASP.NET Core Web API on .NET 10, ASP.NET Identity, JWT bearer auth, MediatR, FluentValidation, EF Core, PostgreSQL provider
- Frontend: React 19, Next.js 16 App Router, TypeScript, Tailwind CSS, shadcn/ui

## Solution Layout

- `Backend/BowlsLive.Api`
- `Backend/BowlsLive.Application`
- `Backend/BowlsLive.Domain`
- `Backend/BowlsLive.Infrastructure`
- `Backend/BowlsLive.Persistence`
- `Frontend/bowlslive-web`

## Run Backend

```powershell
dotnet restore .\BowlsLive.sln
dotnet run --project .\Backend\BowlsLive.Api\BowlsLive.Api.csproj
```

Backend URLs are shown in console output.
The API creates its database schema automatically on first run.

## Run Frontend

```powershell
cd .\Frontend\bowlslive-web
npm install
npm run dev
```

Open `http://localhost:3000`.

Create `Frontend\bowlslive-web\.env.local` from `Frontend\bowlslive-web\.env.example` when you want to override the default API URL.

## Auth Endpoints

- `POST /api/auth/register`
- `POST /api/auth/login`
- `GET /api/auth/me` with `Authorization: Bearer <token>`

## Checks

```powershell
dotnet build .\BowlsLive.sln
cd .\Frontend\bowlslive-web
npm run lint
npm run build
```

