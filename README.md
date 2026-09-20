# KBUI Explorer

Standalone **Blazor Hybrid (MAUI)** desktop client for **Rag_Ingestion_Tool** query only.

## Safety restrictions

This app **cannot** call ingest or admin APIs:

| Allowed | Blocked |
|---------|---------|
| `GET /health` | `POST /ingest` |
| `POST /search` | `DELETE /index` |
| `POST /rag/query` | `GET /jobs/{id}` |
| `POST /chat` | Any other path |

Client also enforces:

- Absolute `http`/`https` API base URL only (no path on the base URL)
- Max question length **500**
- Max Top K **10**
- Optional `X-Api-Key` for demo-hardened APIs

## Prerequisites

- .NET 9 SDK
- MAUI Windows workload (`maui-windows`)
- Network access to the Azure query API (or a local Rag_Ingestion_Tool query API)

## Run (Windows)

```powershell
cd C:\hperson_tools\KBUI_Explorer
dotnet build -f net9.0-windows10.0.19041.0
dotnet run -f net9.0-windows10.0.19041.0
```

Default API URL: `https://app-q-cpy-hud-dev.azurewebsites.net`

If the Azure query app has demo hardening enabled, paste the demo key into the API key field.

### Optional local query API

To use a local Rag_Ingestion_Tool instead:

```powershell
cd C:\hperson_tools\Rag_Ingestion_Tool
.\scripts\Start-QueryKb.ps1 -IdentityPath identities\cpy-hud-dev.json -UseCache
```

Then set API base URL in the app to `http://127.0.0.1:5088` (and the demo key if using `Start-Demo.ps1`).
