# Starting and Using KBUI Explorer

## 1. Start the application

From the project root, run:

```bash
dotnet restore
```

Then start the app:

```bash
dotnet run --project KBUI_Web/KBUI_Web.csproj
```

This starts the Blazor WebAssembly app locally. The browser will open to a localhost URL, typically something like:

- https://localhost:xxxxx
- http://localhost:xxxxx

If the browser does not open automatically, open the URL shown in the terminal.

---

## 2. Connect to the API

Once the app is running:

1. Open the home page.
2. Enter the API base URL for the backend query service.
3. If the API requires it, enter the optional API key.
4. Click "Save & check health".

The app will test connectivity to the backend and display the health status.

If the health check fails, check:

- the API URL is correct
- the backend service is running
- the network can reach the service
- the API key is valid if the service requires one

---

## 3. Ask a question

After the app is connected:

1. Type a question in the question box.
2. Select a mode:
   - Search KB
   - RAG
   - Chat only
3. Set the Top K value if you are using Search or RAG.
4. Click "Run".

---

## 4. Read the results

The app will show results depending on the selected mode:

- Search KB: matching hits and snippets from the knowledge base
- RAG: answer plus sources and citations
- Chat only: chat-style answer without retrieval

If there is an error, the app shows a message at the top of the page.

---

## 5. Important app behavior

This application is intentionally query-only. It can only call approved read endpoints such as:

- /health
- /search
- /rag/query
- /chat

It blocks ingest and admin operations by design.

---

## 6. Running the GitHub-hosted application

This app can also be opened from the GitHub Pages deployment instead of running locally.

Typical GitHub Pages URL format:

```text
https://<your-github-username>.github.io/<your-repository-name>/
```

Example for this repo if it is published under the GitHub user or org named hperson:

```text
https://se2000-dotnet.github.io/kbui-explorer/
```

Replace hperson with the actual GitHub username or organization that owns the repository.

1. Open the GitHub Pages site URL for the project.
2. Wait for the page to load.
3. In the app, enter the backend API base URL.
4. If needed, enter the API key.
5. Click "Save & check health".
6. Ask a question and run the query.

The GitHub-hosted app behaves the same as the local app. The only difference is that the UI is served from GitHub Pages while the backend API still runs elsewhere.

---

## 7. Typical local workflow

```bash
dotnet restore
dotnet run --project KBUI_Web/KBUI_Web.csproj
```

Then in the UI:

1. enter the backend URL
2. click "Save & check health"
3. ask a question
4. choose Search, RAG, or Chat
5. run the query and review the answer
