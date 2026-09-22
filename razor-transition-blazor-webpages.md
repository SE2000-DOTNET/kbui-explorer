# Blazor WebAssembly + GitHub Pages migration plan

This project is currently a Windows MAUI desktop client, not a GitHub Pages app. GitHub Pages can host static web content, but it cannot host a MAUI desktop app directly.

The best path is to convert the UI to a Blazor WebAssembly app and keep the backend query API in Azure or another hosted service.

## Why this is the best approach

- The current app already uses Blazor components in `Components/Pages/Home.razor`.
- The project already models query requests and responses in `Models/QueryModels.cs`.
- The app already calls a remote query API through `Services/QueryApiClient.cs`.
- GitHub Pages supports static browser-hosted content, which is a natural fit for Blazor WebAssembly.
- The backend contract is already well defined and does not need to change much.

## Recommended architecture

### Frontend
- GitHub Pages hosts a public static Blazor WebAssembly app.
- The UI stays browser-based.
- The app calls a backend API over HTTPS.

### Backend
- Azure App Service, Azure Functions, or another hosted API service.
- Exposes the same query endpoints:
  - `GET /health`
  - `POST /search`
  - `POST /rag/query`
  - `POST /chat`
- Handles auth, validation, rate limiting, and secrets.

### Security
- No secrets in the browser or GitHub repo.
- CORS restricted to the GitHub Pages domain.
- Use HTTPS only.
- Store API keys and tokens in Azure Key Vault or server-side environment variables.
- Backend must validate requests and enforce allowed routes.

## Why not host the current MAUI app directly

The project uses:
- `UseMaui=true`
- `OutputType=Exe`
- Windows-specific targeting
- desktop runtime requirements

This means it is a desktop application, not a static website. GitHub Pages cannot run a Windows desktop app in the browser.

## Migration path

### 1. Create a new Blazor WebAssembly app

```powershell
dotnet new blazorwasm -o KBUI_Web
```

This creates a browser app that can be published as static files.

### 2. Reuse the UI logic from the MAUI project

Move or adapt the main query workflow from:
- `Components/Pages/Home.razor`
- `Models/QueryModels.cs`
- `Services/QueryApiClient.cs`

The UX can stay the same: mode selection, answer card, citations, markdown rendering, and query execution.

### 3. Replace MAUI startup code with browser startup

The current MAUI app initializes services in `MauiProgram.cs`.

For the web app, configure a browser-safe service container and HTTP client:

```csharp
builder.Services.AddHttpClient();
```

Then read the backend API URL from configuration instead of storing it in a desktop setting.

### 4. Keep the same backend contract

Keep the same API endpoints and payloads so the frontend can call the same backend query service without reworking the server side.

### 5. Remove secrets from the frontend

The current desktop app includes optional API key handling in `Options/QueryUiOptions.cs` and `Services/QueryApiClient.cs`. That is fine for a desktop app, but not for a public page.

For GitHub Pages, the browser should not contain API keys or sensitive configuration.

### 6. Add a protected backend layer

Make the backend the source of truth for:
- allowed operations
- validation
- auth
- rate limiting
- request logging

### 7. Deploy the frontend to GitHub Pages

Use GitHub Actions to publish the generated static files to GitHub Pages.

Example workflow pattern:

```yaml
name: Deploy to GitHub Pages

on:
  push:
    branches: [main]

jobs:
  deploy:
    runs-on: ubuntu-latest
    permissions:
      contents: read
      pages: write
      id-token: write

    steps:
      - uses: actions/checkout@v4

      - uses: actions/setup-dotnet@v4
        with:
          dotnet-version: 9.0.x

      - run: dotnet publish KBUI_Web/KBUI_Web.csproj -c Release -o site

      - uses: actions/upload-pages-artifact@v3
        with:
          path: site/wwwroot

      - uses: actions/deploy-pages@v4
```

## Security model for the hosted version

### Good secure model
- Public static frontend on GitHub Pages
- Protected backend API on Azure
- HTTPS only
- CORS allowlist restricted to the Pages domain
- Auth or token validation on the API
- Secrets kept in Azure Key Vault or server configuration

### Not secure for public hosting
- Embedding a demo key or secret into the browser app
- Allowing anonymous public access to sensitive query APIs
- Posting secret-bearing config to the repo or the client bundle

## Final recommendation

If the goal is to host this app in GitHub Pages, the right solution is:

1. Keep the backend API as the real service
2. Convert the UI to Blazor WebAssembly
3. Deploy the frontend to GitHub Pages
4. Protect the backend with server-side security controls

This is the best long-term approach because it matches the app’s current architecture while keeping the frontend public and secure.
