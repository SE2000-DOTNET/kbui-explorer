# KBUI added security

## Enable Access-Control-Allow-Credentials

Leave **Enable Access-Control-Allow-Credentials** **off** for this app.

This UI calls Azure with `BrowserRequestCredentials.Omit`. It does not send cookies or HTTP auth. The API CORS policy also does not call `AllowCredentials()`. It only needs:

- Allowed origins: `https://se2000-dotnet.github.io` (and localhost while developing)
- Methods: GET, POST, OPTIONS
- Headers: `Content-Type` (and `X-Api-Key` only if you actually send a key)

Turning credentials **on** does not make queries work better. It is for cookie/session auth. It also forbids a `*` origin and is stricter than you need.

Turn it **on** later only if you add cookie-based login (Easy Auth cookies) and change the client to send credentials. Bearer tokens in a header are different: allow the `Authorization` header, still without this checkbox.

## Hardening Process

This plan implements:

- **2)** Harden Azure (the real security boundary)
- **3)** Frontend hygiene
- **4)** GitHub / process
- **5)** Protect a sensitive knowledge base (login)

It assumes the current split: public Blazor WebAssembly (this repo) calling `app-q-cpy-hud-dev` from `Rag_Ingestion_Tool`.

### Goal

Treat the **Azure query API** as the security boundary. The GitHub Pages app stays public and secret-free. Anonymous callers should not be able to search, RAG, or chat against HUD content.

### What is already true

| Control | Status |
|---|---|
| Query App Service `RAGINGEST_QUERY_ONLY=true` | In Terraform; `/ingest` returns 409 |
| Separate admin App Service for ingest | In Terraform |
| `https_only` on App Service | On |
| API CORS policy `KbUi` + App Service CORS | Both exist (risk of conflict) |
| `ApiKeyMiddleware` + optional demo key | Exists; **do not put that key in Pages** |
| Rate limit + max question/Top K | Demo hardening flags in Terraform |
| UI allowlist `/health` `/search` `/rag/query` `/chat` | In `QueryApiClient` |
| API key fields hidden on `github.io` | In `Home.razor` |
| Fetch credentials omitted | `BrowserRequestCredentials.Omit` |

Work is to **close the anonymous API**, **tighten CORS**, **harden the static UI**, **lock down GitHub**, then **add real login** if the KB is sensitive (HUD-related, so treat 5 as in-scope).



### Order of work

Do **2A → 2B → 3 → 4 → 5**. Do not start Entra (5) until CORS is a single allowlist and the demo key is not in the browser.

### 2) Harden Azure

#### 2A. CORS (one system, tight list)

**Problem:** App Service CORS and `UseCors("KbUi")` both run. Portal was sending `Access-Control-Allow-Credentials: true`. The UI does **not** need credentials.

**Do this**

1. Pick **one** CORS owner:
   - Prefer **API code** (`CorsSettingsOptions` / `RAGINGEST_Cors__AllowedOrigins`) and clear App Service CORS to empty, **or**
   - Prefer **Portal CORS** and stop sending CORS from the app for the query site.
2. Allowed origins only:
   - `https://se2000-dotnet.github.io`
   - Dev only: `http://localhost:5003`, `https://localhost:5002`
3. Methods: `GET`, `POST`, `OPTIONS`
4. Headers: `Content-Type`; add `Authorization` when Entra is on; add `X-Api-Key` only if demo key stays (phase 5 should remove it for Pages).
5. **Enable Access-Control-Allow-Credentials: Off**
6. Set `RAGINGEST_Cors__AllowedOrigins` in Terraform `var.demo_cors_origins` to the same list (no `*`).
7. After Entra: keep credentials **off** if the client sends a Bearer header (not cookies). Turn credentials **on** only for Easy Auth **cookies** plus `credentials: include` on the client.

**Verify:** OPTIONS `/rag/query` with `Origin: https://se2000-dotnet.github.io` → 200 + one `Access-Control-Allow-Origin`. Same for localhost. Other origins → fail.

**Files / places:** `Rag_Ingestion_Tool` `Program.cs` CORS policy, `IAC/maint.tf` `RAGINGEST_Cors__AllowedOrigins`, Azure Portal CORS on `app-q-cpy-hud-dev`.

#### 2B. Query-only on the server (not only the UI)

**Already:** query app is `QUERY_ONLY`; ingest is 409.

**Do this**

1. Confirm production app setting `RAGINGEST_QUERY_ONLY=true` on `app-q-cpy-hud-dev`.
2. Confirm `/ingest`, job, and index-delete routes 409/404 on **that** host.
3. Restrict the **admin** app (`azurerm_linux_web_app.admin`) with IP allowlist, Entra, or private endpoint. Do not leave it on the public internet with the same openness as query.
4. `/health` currently returns index name and Search endpoint. For a public UI, strip or reduce health (status + mode only) so recon does not get Azure resource names.

**Verify:** curl query host `/ingest` → 409. curl `/health` does not leak keys (it should not today) and, after change, leaks less topology.

#### 2C. Auth (close anonymous query)

**Problem:** CORS does not stop curl. Anyone with the App Service URL can POST `/search` and `/rag/query`.

**Do not:** put `RAGINGEST_ApiAuth__DemoKey` in `KBUI_Web/wwwroot/appsettings.json` or GitHub Pages.

**Recommended path for this stack: App Service Easy Auth (Entra ID), then Bearer from the WASM app.**

1. Register an Entra **SPA** app for `https://se2000-dotnet.github.io/kbui-explorer/` (and localhost redirect for dev).
2. Register (or reuse) an Entra **API** app for the query App Service; expose scope e.g. `access_as_user`.
3. On `app-q-cpy-hud-dev`: Authentication → Microsoft → require auth for `/search`, `/rag/query`, `/chat`. Leave `/health` anonymous if you still need an uptime probe, or protect health too and use a separate probe.
4. In KBUI: MSAL (`Microsoft.Authentication.WebAssembly.Msal`); attach `Authorization: Bearer` on query calls.
5. Turn off or ignore demo `X-Api-Key` for browser callers once JWT works.
6. Restrict Entra app to your tenant / security group (HUD/org users only).

**Verify:** unauthenticated curl `/rag/query` → 401. Signed-in Pages user succeeds. Token from another app/tenant fails.

#### 2D. Abuse and cost

1. Keep `RAGINGEST_RateLimit__Enabled=true`; tune permit/window (today demo defaults exist in Terraform).
2. Keep `Demo__MaxQuestionLength=500` and `MaxTopK=10` even after Entra (defense in depth).
3. Azure Monitor alert on OpenAI 429s and App Service 401/429 spikes.
4. Budget alert on `openai-cpy-hud-dev` (capacity 30 = 30K TPM, still pay-per-token).
5. Confirm `gpt4omini` Terraform `capacity = 30` so apply does not reset to 1.

**Verify:** burst of unauthenticated or authenticated requests hits 429 before OpenAI spend spikes.

#### 2E. Secrets and data plane

1. OpenAI, Search, Cosmos, demo key: App settings or Key Vault references only; never this repo.
2. Prefer managed identity from App Service to Key Vault / Search if not already.
3. Tighten Search and Cosmos firewalls to the query App Service outbound (and admin app), not `0.0.0.0`.
4. Request log in Cosmos: store question metadata as needed; do **not** log Bearer tokens or API keys.
5. Stop returning full RAG prompt/passages to the public UI in production (see 3B). Debug dumps are a data leak.

**Verify:** no secrets in GitHub; App Service identity can read Key Vault; Search rejects requests from a random IP.

### 3) Frontend hygiene

Repo: **KBUI_Explorer**.

#### 3A. Keep the client allowlist

No change to allowed paths unless a new query route is approved. Do not add ingest URLs “for convenience.”

#### 3B. Reduce leakage in the UI

1. Keep API key inputs **off** on GitHub Pages.
2. Do not ship a real key in `wwwroot/appsettings.json` (`ApiKey` stays `""`).
3. Gate “RAG request / prompt details / hits” behind a debug flag (local only or `?debug=1`), default **hidden on Pages**.
4. Confirm `SetBrowserRequestCredentials(Omit)` stays until cookie auth exists.

#### 3C. Content-Security-Policy

In `KBUI_Web/wwwroot/index.html` add a strict CSP, for example:

- `default-src 'self'`
- `script-src 'self' 'wasm-unsafe-eval'` (Blazor WASM needs this)
- `style-src 'self' 'unsafe-inline'` (Blazor scoped CSS often needs inline)
- `connect-src 'self' https://app-q-cpy-hud-dev.azurewebsites.net https://login.microsoftonline.com https://*.microsoftonline.com`
- `img-src 'self' data:`
- `frame-ancestors 'none'`
- `upgrade-insecure-requests`

After MSAL, extend `connect-src` for your Entra endpoints.

**Verify:** app loads; API calls work; extra script/CDN blocked in DevTools.

#### 3D. No extra telemetry

Do not add Google Analytics, App Insights **browser** SDK, or similar until a privacy review. Server-side App Insights on the API is fine.

#### 3E. HTTPS

Pages is HTTPS. Local: prefer https://localhost:5002. Do not allow `http://` API URLs in production config (`UpdateConnection` already requires http/https; production should be https-only).

### 4) GitHub / process

Repo: **KBUI_Explorer** (repeat similar on **Rag_Ingestion_Tool**).

1. **Branch protection** on `main`: require PR, 1 review, status checks (`ci-cd` build), no force-push, no bypass for admins if policy allows.
2. **GitHub secret scanning** + push protection.
3. **CODEOWNERS** for `.github/workflows`, `wwwroot/appsettings.json`, CORS/auth docs.
4. **Dependabot** for Actions and NuGet.
5. CI: fail if `appsettings.json` contains a non-empty `ApiKey` or `azurewebsites.net` key-like strings (simple grep job).
6. Pages environment: limit who can approve `github-pages` deploys.
7. Document: never commit `terraform.secret.auto.tfvars`, keys, or `.env`.

**Verify:** unprotected push to `main` is blocked; a dummy key in appsettings fails CI.

### 5) Sensitive KB — put the product behind login

HUD content should not stay “public UI + anonymous API.”

**Target architecture**

```text
User → Entra sign-in
     → GitHub Pages (or later Static Web Apps / App Service UI)
     → Bearer token
     → app-q-cpy-hud-dev (Easy Auth / JWT)
     → Azure Search / OpenAI / Cosmos
```

**Implementation steps**

1. Complete 2C (API rejects anonymous query).
2. Add MSAL to `KBUI_Web`; login button; hide Run until signed in.
3. Optional: replace GitHub Pages with **Azure Static Web Apps** or host the WASM app on the **same App Service** so you get built-in auth and no public Pages origin. That is the strongest variant of 5.
4. Entra: require your tenant + a security group; disable personal Microsoft accounts.
5. Remove localhost from **production** CORS; keep it only on a **dev** slot/app.
6. Turn off demo API key for production once JWT is live.
7. Legal/privacy: questions may be HUD-related; confirm logging and retention.

**Verify:** incognito Pages shows login, not a working query box. After login, RAG works. Token copy to curl from another user fails if group-restricted.

### Suggested delivery slices

| Slice | Scope | Done when |
|---|---|---|
| S1 | 2A CORS + credentials off | Local + Pages OPTIONS succeed; foreign origin fails |
| S2 | 2B health trim + admin lock | Query host cannot ingest |
| S3 | 3B hide prompt dumps on Pages + 3C CSP | No RAG request dump on github.io |
| S4 | 4 branch protection + secret scan | `main` cannot be pushed raw |
| S5 | 2C + 5 Entra | Anonymous `/rag/query` is 401; signed-in UI works |
| S6 | 2D alerts + 2E Key Vault/firewalls | Budget + 429 alerts fire in a test |

### Cannot finish now (needs portal / org admin)

These items cannot be completed from the KBUI_Explorer or Rag_Ingestion_Tool codebases alone. They need Azure Portal, Entra tenant admin, or GitHub org/repo admin.

Code and query-app deploy already covered: slim query `/health`, app CORS origins setting, CSP, hide prompt dumps on Pages, HTTPS-only API URLs, CI empty-ApiKey check, Dependabot, CODEOWNERS (team must exist), `gpt4omini` TPM 30.

| Plan item | Still to do | Who / where |
|---|---|---|
| **2A** One CORS owner | Keep **Enable Access-Control-Allow-Credentials** **off**. Use **either** App Service Portal CORS **or** `RAGINGEST_Cors__AllowedOrigins` (already set on `app-q-cpy-hud-dev`), not both. If using Portal, allowed origins only: `https://se2000-dotnet.github.io`, `http://localhost:5003`, `https://localhost:5002` (no path, no trailing slash). Clear the other list so the browser does not get duplicate `Access-Control-Allow-Origin`. | Azure Portal → `app-q-cpy-hud-dev` → API → CORS |
| **2B** Lock the **admin** ingest app | Do not leave `app-a-cpy-hud-dev` as open as the query app. Restrict with IP allowlist, Entra, and/or private endpoint. Query host `/ingest` should stay 409. | Azure networking / Entra on the **admin** Web App |
| **2C / 5 / S5** Entra login | Register SPA (Pages + localhost) and API apps. Easy Auth or JWT on `/search`, `/rag/query`, `/chat`. MSAL in KBUI. Restrict to tenant/security group. Do **not** put a demo API key in GitHub Pages. Anonymous `curl` to `/rag/query` is still possible until this is done. | Entra tenant admin + query App Service Authentication |
| **2D** Cost and 429 alerts | Azure Monitor alerts on OpenAI 429s and App Service 401/429 spikes. Budget alert on `openai-cpy-hud-dev`. | Azure Monitor / Cost Management |
| **2E** Secrets and data plane | Key Vault references (or managed identity) for OpenAI/Search/Cosmos keys. Search and Cosmos firewalls limited to App Service outbound, not `0.0.0.0`. Do not log Bearer tokens or API keys. | Azure RBAC, Key Vault, Search/Cosmos firewall |
| **4** GitHub org settings | Branch protection on `main` (PR + review + CI). Secret scanning and push protection. Limit who can approve the `github-pages` environment. Create or fix the CODEOWNERS team `@se2000-dotnet/maintainers` (or change the handle). | GitHub org/repo admin — not delivered by a code PR alone |

**Done when**

- Foreign origins fail CORS; Pages and local localhost origins succeed; credentials checkbox is off.
- Admin ingest app is not anonymously reachable from the public internet.
- Unauthenticated `/rag/query` returns 401; signed-in org user can RAG from the UI.
- Budget and 429 alerts fire in a test.
- Search/Cosmos reject traffic from a random IP; no secrets in GitHub.
- Direct push to `main` is blocked; a dummy `ApiKey` in appsettings fails CI (CI check is already in the workflow).

### Out of scope for this plan

- Putting `X-Api-Key` in the Pages bundle
- Enabling Access-Control-Allow-Credentials without cookie login
- Using CORS as the only access control

Remaining work is the portal/org-admin list above. Highest impact next: **2A (one CORS owner)** then **2C/5 (Entra)** so anonymous query is closed.
