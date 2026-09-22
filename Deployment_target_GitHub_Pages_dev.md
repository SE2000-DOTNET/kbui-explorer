# Deployment target: GitHub Pages (Development/Test)

## Overview
This is the first deployment target for the Blazor WebAssembly frontend. GitHub Pages is a good fit for testing and validation because the app is a static client-side app and already publishes successfully as a static site.

## Project status
The app was verified to build successfully with:

```bash
dotnet publish KBUI_Web/KBUI_Web.csproj -c Release
```

Result: Build succeeded.

## Why GitHub Pages is appropriate
- The project is a Blazor WebAssembly app.
- The app is hosted as static content in the browser.
- GitHub Pages can host the generated static frontend output.
- This is a good test target before moving to Azure Static Web Apps for QA or production.

## Relevant project files
- [KBUI_Web/KBUI_Web.csproj](KBUI_Web/KBUI_Web.csproj)
- [KBUI_Web/wwwroot/index.html](KBUI_Web/wwwroot/index.html)
- [KBUI_Web/wwwroot/appsettings.json](KBUI_Web/wwwroot/appsettings.json)
- [.github/workflows/ci-cd.yml](.github/workflows/ci-cd.yml)

## Deployment model
- Frontend: GitHub Pages
- Backend/API: external service, such as Azure-hosted API
- Browser calls: the app calls the configured API via the `QueryUi:ApiBaseUrl` setting

## Important caveat
GitHub Pages is a static hosting target only. The frontend cannot run server-side .NET code in GitHub Pages. Any sensitive configuration or credentials must not be embedded in the browser. The API must be reachable from the browser and should allow CORS for the GitHub Pages domain.

## GitHub setup steps
1. Push the repository to GitHub.
2. Open the repository in GitHub.
3. Go to Settings > Pages.
4. Set Source to GitHub Actions.
5. Ensure the branch is `main`.
6. The GitHub Actions workflow in `.github/workflows/ci-cd.yml` will publish the site and deploy it.

## Expected result
The site should become available at a URL like:

```text
https://<your-username>.github.io/<repository-name>/
```

## Future migration path
When the team is ready for QA or production-grade hosting, the same Blazor frontend can be moved to Azure Static Web Apps with a relatively small deployment change. This is a clean next step and avoids a full rewrite.

## Summary
This is the correct deployment target 1 for early testing and validation. It is fast to deploy and matches the static nature of the application.
