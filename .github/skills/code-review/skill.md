SOURCE: code-review-skill.md - C:\Users\jr59274\developer\POS-AiAgentSkills\.github\skills\copilot\software-engineering

# Code Review Skill

Use this skill whenever preparing code for commit or pull request in this repository.

## Purpose

349. Run a structured review pass before commit and before pull request creation.
350. Reference well-known review agents and tools while following local repository standards.
351. Enforce review decisions: address comments or explicitly justify ignoring them.

## Required References

352. Follow .github/skills/copilot/software-engineering/engineering-skill.md for quality, vulnerability, testing, and SOLID expectations.
353. Use well-known review agents/tools where available:
    * GitHub Copilot code review (primary AI reviewer)
    * GitHub Advanced Security / CodeQL alerts (security findings)
    * SonarCloud or equivalent static analysis (if configured)
354. Apply expert review lenses during triage:
    * GitHub Workflow Expert lens for CI/CD and reusable workflow integrity.
    * Terraform Expert lens for IaC composition, identity access modules, and network security posture.
355. Treat these reviews as input to engineering decisions, not as auto-merge approval.

## GitHub Workflow Expert Review (Repository Specific)

356. Validate reusable workflow chaining remains intact:
    * .github/workflows/ci-cd.yml calls .github/workflows/test-and-code-coverage.yml.
    * .github/workflows/ci-cd.yml calls .github/workflows/deploy-environment.yml.
    * .github/workflows/deploy-environment.yml calls .github/workflows/deploy-iac.yml.
    * .github/workflows/deploy-iac.yml calls .github/workflows/initialize-tfstate.yml.
357. Preserve artifact/report path consistency in test workflows; prefer ${{ github.workspace }} absolute paths for generated outputs.
358. Preserve immutable image deployment behavior:
    * Deploy paths must not rely on mutable latest tags.
    * Keep guardrails that fail deployment if image tag is missing or equals latest.
359. Preserve OIDC-based Azure authentication in deployment workflows:
    * azure/login@v2 with id-token: write permissions.
    * Avoid introducing long-lived secrets where OIDC already works.
360. Preserve environment progression intent:
    * main drives Dev deployment.
    * Tagged releases (v*) are the production promotion path (when enabled).
361. Keep self-hosted runner assumptions explicit for deployment jobs and avoid introducing hosted-runner-only dependencies there.

## Terraform Expert Review (Repository Specific)

362. Preserve root-module composition in iac/main.tf:
    * Core modules: log_analytics, cosmos, container_app_environment, key_vault, container_app_api (ember/passport).
    * Access modules: key_vault_access and cosmos_access for managed-identity authorization.
363. Preserve managed identity access model:
    * Container apps expose user-assigned principal IDs.
    * key_vault_access grants secrets access to app identities.
    * cosmos_access assigns Cosmos RBAC/SQL roles to app identities.
364. Preserve private-access posture for data-plane services:
    * Cosmos DB: public_network_access_enabled = false, VNet filter enabled, private endpoint required.
    * Key Vault: public_network_access_enabled = false, private endpoint required.
    * Terraform state storage bootstrap (initialize-tfstate.yml): storage account created with public network disabled and blob private endpoint.
365. Preserve Container Apps networking expectations:
    * Environment uses infrastructure subnet and internal load balancer.
    * Review any ingress change (external_enabled, transport, insecure connections) as high-impact and require explicit justification.
366. Preserve Terraform operational safety:
    * Keep terraform fmt -check -recursive validation.
    * Keep plan/apply flow with explicit var file and tfplan artifacting.
367. Preserve provider and backend stability:
    * Keep azurerm and azapi versions intentional and reviewed before upgrades.
    * Avoid backend/state key changes without migration plan.

## Review Workflow

368. Run local validation for touched scope (build/tests/lint/security checks as applicable).
369. Collect local and automated review comments (IDE diagnostics, static analysis, AI review, security alerts).
370. Triage each comment:
    * Address: implement a fix and re-validate.
    * Ignore: document a concise rationale and confirm risk acceptance with the engineer/user when needed.
371. Re-review changes after fixes.
372. Commit only after triage is complete and validation passes for the touched scope.

## Comment Triage Rules

373. Never ignore security findings without an explicit rationale.
374. Never ignore comments that indicate functional regression risk without engineer/user confirmation.
375. Never ignore CI/CD workflow integrity findings (reusable workflow wiring, deployment guardrails, OIDC permissions) without explicit rationale.
376. Never ignore Terraform identity/network findings (access-module wiring, private endpoint posture, public access toggles) without explicit rationale.
377. Prefer addressing high-confidence correctness, security, and reliability issues.
378. Keep ignored-comment notes concise and auditable in PR description or review summary.

## Definition of Done (Review)

379. Review comments were triaged (addressed or explicitly ignored with rationale).
380. Local engineering checks from .github/skills/copilot/software-engineering/engineering-skill.md were followed.
381. Engineer/user has visibility into unresolved review comments before PR creation.
382. Commit is finalized only after review triage and validation.
