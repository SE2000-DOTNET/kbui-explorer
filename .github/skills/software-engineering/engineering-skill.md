#SOURCE: engineering-skill.md - C:\Users\jr59274\developer\POS-AiAgentSkills\.github\skills\copilot\software-engineering

# Engineering Skill

Use this skill whenever making code changes in this repository.

## Core Rules

383. **Vulnerability Assessment**: Check package vulnerabilities for touched ecosystems before finalizing code changes.
384. **Upgrade Guidance**: Recommend package updates to the user when vulnerabilities are found.
385. **Version Locking**: Do not change package versions without explicit user confirmation.
386. **Test Coverage**: Add or update unit tests for new or changed logic whenever practical.
387. **Coverage Exclusions**: For code that is not reasonably unit-testable (framework wiring, startup boilerplate, generated/simple DTO/config models), add `[ExcludeFromCodeCoverage]` with a concise justification.
388. **Architecture Principles**: Follow repository coding standards and enforce OOP + SOLID principles in all code changes.
389. **CI/CD Guardrails**: For CI/CD changes, apply GitHub Workflow Expert checks for reusable workflow wiring, auth, artifact paths, and deployment guardrails.
390. **IaC Guardrails**: For Terraform/IaC changes, apply Terraform Expert checks for module composition, managed identity access, and private networking posture.

## Vulnerability Workflow

391. **Scope Touched Ecosystems**: Identify touched package ecosystems (for example: .NET/NuGet, npm).
392. **Execute Checks**: Run vulnerability checks for impacted ecosystems.
393. **Triage Findings**: Summarize findings and recommend safe upgrades.
394. **User Gate**: Ask for confirmation before upgrading package versions.
395. **Apply Updates**: After confirmation, implement upgrades and run restore/build/tests.

## Testing and Coverage Workflow

396. **Behavior Changes**: Add unit tests for behavior changes and edge cases.
397. **Test Approach**: Prefer behavior-focused tests over implementation-detail tests.
398. **Locality**: Keep tests close to changed code areas.
399. **Minimal Scope**: If coverage exclusion is required, keep the exclusion scope as small as possible.
400. **Logic Integrity**: Never use coverage exclusion to avoid testing business logic.

## OOP and SOLID Checklist

* 401. **Single Responsibility**: Each class should have one reason to change.
* 402. **Open/Closed**: Prefer extension over modification for existing stable behavior.
* 403. **Liskov Substitution**: Derived implementations should preserve base expectations.
* 404. **Interface Segregation**: Prefer small, focused interfaces.
* 405. **Dependency Inversion**: Depend on abstractions, inject external dependencies.
* 406. **Encapsulation**: Keep invariants within class boundaries.
* 407. **Readability**: Use clear naming, small methods, and minimal side effects.
* 408. **Validate Before Transform**: For any endpoint or method that normalizes or transforms input (parsing, splitting, mapping), all required-field validation guards must execute before any transformation step. A missing or malformed required field must produce a controlled error response - never an unhandled exception from a downstream parsing or mapping operation.

## CI/CD Expert Checklist

409. **Workflow Chaining**: Preserve reusable workflow chaining in `.github/workflows/` (`ci-cd.yml` -> `test-and-code-coverage.yml` and `deploy-environment.yml` -> `deploy-iac.yml` -> `initialize-tfstate.yml`).
410. **Deterministic Paths**: Keep artifact and report paths deterministic and consistent; prefer `${{ github.workspace }}` absolute paths for generated files.
411. **Immutable Deployments**: Preserve immutable image-tag deployment behavior and guardrails that reject missing or `latest` tags.
412. **OIDC Authentication**: Preserve OIDC-based Azure authentication (`azure/login@v2`) with `id-token: write` where required.
413. **High-Impact Volatility**: Treat changes to environment promotion flow, workflow triggers, or deploy-runner assumptions as high-impact and require explicit validation.

## Terraform Expert Checklist

414. **Root Architecture**: Preserve root module composition and contracts in `iac/main.tf`.
415. **Identity Isolation**: Preserve managed identity access-module patterns:
   * `iac/key_vault/key_vault_access/key_vault_access.tf`
   * `iac/cosmos/cosmos_access/cosmos_access.tf`
416. **Private Posture**: Preserve private access posture for protected services:
   * Cosmos and Key Vault public access disabled with private endpoints.
   * Terraform state storage bootstrap with public network access disabled and private endpoint.
417. **Network Perimeter**: Treat ingress/network exposure changes in container apps as high-impact and require explicit review.
418. **Operational Checks**: Preserve Terraform operational checks (`terraform fmt -check -recursive`, plan/apply flow) and keep provider/backend changes intentional.
419. **Post-Edit Validation**: Run `terraform validate` from the `iac/` directory after any Terraform file edit and before committing. *Note: `terraform fmt -check` validates formatting only — `terraform validate` is required to catch syntax and attribute errors that fmt will not detect.*

## Definition of Done (Code Changes)

420. [ ] Vulnerability check performed and findings shared.
421. [ ] Package upgrades applied only with user confirmation.
422. [ ] Unit tests added/updated where practical.
423. [ ] Any coverage exclusions are justified and minimal.
424. [ ] Build and relevant tests pass for touched scope.
425. [ ] For workflow changes, CI/CD expert checklist reviewed and validated.
426. [ ] For Terraform changes, Terraform expert checklist reviewed and validated.

