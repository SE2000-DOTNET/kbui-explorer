# Blazor WebAssembly Migration Plan

## Objective

Convert the existing Windows MAUI desktop client into a GitHub Pages-ready Blazor WebAssembly frontend while preserving the current backend query contract and security model.

## Background

The current app is a MAUI desktop client targeting `net9.0-windows10.0.19041.0`. It calls a remote query API and renders results using Blazor components. Since GitHub Pages can only host static web content, the MAUI desktop app cannot be deployed directly there. The right migration is to replace the desktop shell with a browser-hosted Blazor WebAssembly app and keep the real query service hosted on Azure or another secure platform.

## Goals

- Migrate the current UI to a browser-hosted Blazor WebAssembly app.
- Preserve the existing query API contract: `/health`, `/search`, `/rag/query`, and `/chat`.
- Make the frontend deployable to GitHub Pages.
- Keep all secrets and sensitive configuration out of the browser bundle.
- Maintain the current user experience for query, RAG, and chat flows.

## Non-goals

- Rewriting the backend service contract.
- Making the app desktop-only again.
- Hosting a MAUI Windows app on GitHub Pages.
- Embedding API keys or secrets in public frontend code.

## Scope

### In scope

- Create a new Blazor WebAssembly frontend project.
- Reuse or adapt the UI logic in `Components/Pages/Home.razor`.
- Reuse request/response model contracts from `Models/QueryModels.cs`.
- Reuse service logic from `Services/QueryApiClient.cs` as a guide for API usage.
- Publish generated static files to GitHub Pages.
- Protect the backend with server-side auth and validation.

### Out of scope

- Full redesign of the backend query service.
- Replacing the remote query backend with a local browser-only implementation.
- Packaging a desktop MAUI release from the new web app.

## Proposed architecture

### Frontend

- Blazor WebAssembly application.
- Runs in the browser.
- Calls a secure backend API over HTTPS.
- Reads configuration via `appsettings.json` or environment configuration.

### Backend

- Azure-hosted query API or equivalent.
- Enforces auth, allowed routes, and rate limits.
- Keeps secrets in Azure Key Vault or secure server settings.
- Accepts requests from the web frontend only via approved CORS policy.

## Workstreams

### 1. Frontend scaffold

- Create a new Blazor WebAssembly project.
- Remove default template components as needed.
- Wire in the query UI and markdown rendering.
- Configure API base URL settings.

### 2. API adaptation

- Reuse existing request/response models.
- Abandon desktop-specific settings in favor of browser-safe configuration.
- Replace any hard-coded base URL logic with configuration-driven values.

### 3. Security hardening

- Do not place API keys in the browser bundle.
- Keep backend auth and validation server-side.
- Restrict CORS to the GitHub Pages domain.
- Use HTTPS and backend-side secret storage.

### 4. Deployment

- Publish the Blazor WebAssembly app to a Pages-compatible output.
- Use GitHub Actions to build and deploy static output.
- Validate that the app loads and calls the backend correctly.

## Milestones

### Milestone 1: Scaffold the web app

- Create a baseline Blazor WebAssembly project.
- Confirm it builds successfully.

### Milestone 2: Port the query UI

- Recreate the search, RAG, and chat flows in the web app.
- Adjust styling and markdown rendering for browser use.

### Milestone 3: Harden security

- Remove secret-bearing frontend config.
- Enforce server-side auth and allowed routes.

### Milestone 4: Deploy to GitHub Pages

- Publish generated static output.
- Validate the live deployment against the API.

## Risks and mitigations

### Risk: Browser-side secrets are exposed

Mitigation: keep all secrets server-side; never embed keys in the frontend.

### Risk: API contract mismatch

Mitigation: keep the backend endpoint contract stable and validate payloads with the existing models.

### Risk: GitHub Pages deployment misses static output settings

Mitigation: publish via `dotnet publish` and upload the generated `wwwroot` output as Pages artifact.

### Risk: CORS blocks frontend requests

Mitigation: configure a strict allowlist for the GitHub Pages domain.

## Acceptance criteria

- A new Blazor WebAssembly project exists and builds successfully.
- The frontend can call the backend query API using the same contract patterns as the MAUI app.
- No secrets are stored in the browser UI code.
- GitHub Actions can publish the static output to GitHub Pages.
- The deployed frontend can render search, RAG, and chat responses from the remote API.

## Recommended next action

Scaffold the Blazor WebAssembly app in a sibling project folder and validate the default build before moving the UI logic over from the MAUI app.
