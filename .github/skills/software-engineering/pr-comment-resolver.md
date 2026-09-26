# SOURCE: pr-comment-resolver.md - C:\Users\jr59274\developer\POS-AiAgentSkills\.github\skills\copilot\software-engineering# PR Comment Resolver Skill

Use this skill when asked to resolve, triage, or respond to review comments on an open pull request.

## Purpose

462. **Fetch and Enumerate**: Fetch and enumerate all unresolved review comments on a given pull request.
463. **Evaluate Standards**: Evaluate each comment for validity against repository standards and engineering quality.
464. **Propose Resolutions**: Propose a concrete resolution — either a code fix or a justified response comment — for each finding.
465. **Apply & Document**: Apply fixes where appropriate and leave auditable response comments where a fix is not warranted.

## Required References

466. `.github/skills/copilot/software-engineering/engineering-skill.md` — Follow for quality, vulnerability, testing, SOLID, and CI/CD expectations when proposing or applying fixes.
467. `.github/skills/copilot/software-engineering/code-review-skill.md` — Follow comment triage rules for deciding whether to address or explicitly ignore each comment.
468. `.github/skills/copilot/repo-skills/pull-request-skill.md` — Follow for PR update conventions and merge readiness requirements.

## Inputs

* **PR Number or URL**: The pull request to process.
* **Repository**: Inferred from the current workspace context when not explicitly provided.
* **Scope**: Defaults to all unresolved review comments; the user may narrow to a specific reviewer or thread.

## Resolution Workflow

469. **Fetch PR Comments**: 
   * Use the GitHub CLI or GitHub API to retrieve all open review threads and inline comments for the PR.
   * Command reference: `gh pr view <PR_NUMBER> --comments` and `gh api repos/{owner}/{repo}/pulls/{pr}/comments`.
   * Capture: comment author, file path, line reference, comment body, and resolution status.
470. **Classify Findings**: Classify each comment into one of the following categories:
   * **Security**: Vulnerability, OWASP Top 10, injection, auth bypass, secret exposure.
   * **Correctness**: Functional bug, regression risk, broken contract.
   * **Engineering Quality**: SOLID violation, naming, readability, missing test, coverage gap.
   * **CI/CD**: Workflow wiring, deployment guardrail, OIDC integrity, artifact path.
   * **Terraform/IaC**: Module composition, managed identity access, private network posture.
   * **Style / Nit**: Minor formatting, naming preference, optional refactor.
   * **Question / Clarification**: Reviewer seeking context, not requiring a code change.
471. **Evaluate Validity**:
   * Apply `engineering-skill.md` OOP/SOLID checklist and CI/CD/Terraform expert checklists.
   * Apply `code-review-skill.md` triage rules.
   * Security findings are always valid; never ignore them without explicit rationale.
   * Functional regression risk requires engineer/user confirmation to ignore.
   * CI/CD and Terraform identity/network findings require an explicit rationale to ignore.
   * Mark as **Valid**, **Partially Valid**, or **Not Valid** with a concise rationale.
472. **Propose Action Plan**:
   * *Valid - Fix*: Implement a concrete code change, test update, or configuration correction.
   * *Valid - Respond*: The finding is correct but the fix is out of scope or a deliberate design choice; draft a response comment explaining the rationale and risk acceptance.
   * *Partially Valid - Respond + Fix*: Address the core concern in code and clarify scope limits in a response.
   * *Not Valid - Respond*: Draft a response comment explaining why the existing code is correct, citing standards or design intent.
   * *Question - Respond*: Draft a clear, concise answer to the reviewer's question.
473. **Apply Code Fixes**:
   * Edit the affected files following `engineering-skill.md` conventions.
   * Add or update unit tests for logic changes.
   * Run the build and relevant tests to confirm fixes pass.
   * Do not change package versions without explicit user confirmation.
474. **Draft Response Comments**:
   * Keep responses concise and professional.
   * Cite the relevant skill, standard, or design decision when justifying a non-fix.
   * For ignored findings, produce an auditable rationale that can be copied into the PR description.
475. **Summarize Resolution**:
   * Produce a triage summary table:

     | # | File / Location | Classification | Validity | Resolution |
     |---|-----------------|----------------|----------|------------|
     | 1 | src/Foo.cs:42   | Correctness    | Valid    | Fixed - null check added |
     | 2 | iac/main.tf:10  | Terraform/IaC  | Valid    | Fixed - access module wiring corrected |
     | 3 | src/Bar.cs:15   | Style / Nit    | Not Valid| Responded - naming follows repository convention |
   * Highlight any findings deferred or out of scope with an explicit rationale.
   * Present the summary to the engineer/user before committing or posting comments.
476. **Commit and Update PR**:
   * Stage and commit fixes following `.github/skills/copilot/repo-skills/commit-skill.md` (prefix with Jira ticket from branch).
   * Post response comments on the PR for non-fix resolutions using: `gh pr review <PR_NUMBER> --comment -b "<response text>"`.
   * Resolve threads for addressed comments where the GitHub API permits.
   * Update the PR description to include the triage summary if the review cycle was substantial.

## Comment Triage Rules (Enforced from `code-review-skill.md`)

477. **Security Block**: Never ignore Security findings without explicit rationale and engineer/user confirmation.
478. **Regression Block**: Never ignore Correctness findings with functional regression risk without engineer/user confirmation.
479. **CI/CD Block**: Never ignore CI/CD integrity findings without explicit rationale.
480. **IaC Block**: Never ignore Terraform/IaC identity/network findings without explicit rationale.
481. **Nits & Questions**: Style / Nit and Question findings may be addressed with a response comment alone.
482. **Auditable Audit Trails**: All ignored comments must have an auditable rationale recorded in the triage summary.

## Repository-Specific Patterns

### IaC: Key Vault Naming Convention
* Input keys should **NOT** carry framework prefixes (e.g., `"DefaultConnection"`, not `"ConnectionStrings_DefaultConnection"`).
* Template merge adds the framework prefix for the env var (e.g., `"ConnectionStrings_${setting.name}"`).
* The secret block and env secret name derivation both use `lower(replace(name, "_", "-"))` on the unprefixed key.
* *Result*: Consistent naming across KV secrets, CA secret blocks, and env var resolution. See `StoreDetailsApi/iac` for working reference.

### Container App Health Probes
* `liveness_probe` — Lightweight app-alive check (no dependencies). Returns `503` if the app process is hung/deadlocked.
* `readiness_probe` — Full dependency check (Cosmos, caches, etc.). Returns `503` if dependencies are unavailable; removes the pod from traffic but doesn't restart it.
* `startup_probe` — App initialization check. Pauses liveness/readiness checks until this passes; gives slow-start apps time to boot.
* *Anti-pattern*: Pointing all three hooks to the same `/health` endpoint causes unnecessary, cascading pod restarts when transient dependency issues occur.

### CI/CD: Retry Loop Validation
* Wait/retry loops in CI workflows must track success/failure state and exit non-zero if the retry budget is exhausted.
* Silent success-on-timeout is an anti-pattern that causes misleading downstream test failures (tests appear to pass when preconditions weren't met).

## Reviewer Assignment Guidance

483. Always add `github-copilot[bot]` as a reviewer for each PR.
484. If using the GitHub CLI, execute: `gh pr edit <PR_NUMBER> --add-reviewer github-copilot[bot]`.
485. If branch protection or permissions block reviewer assignment, note that limitation in the PR description.

## Comment Resolution Workflow

486. **Copilot Automation**: Mark Copilot comments as resolved — use the GraphQL mutation `resolveReviewThread` after fixes are applied and validated.
487. **Human Boundaries**: Post response comments only (no resolve) for comments from human reviewers — they may have follow-up questions or need clarification.
488. **PR Context**: Include a summary comment on the PR linking all fixes to their corresponding comments, validating test/build status.
489. **Done Metric**: Fixes must satisfy the `engineering-skill.md` Definition of Done for the affected scope.
490. **Regression Defense**: Do not introduce new vulnerabilities or SOLID violations while fixing a comment.
491. **Focus Rules**: Keep fixes focused on the reviewed concern; do not refactor unrelated code in the same commit.
492. **Test Strategy**: Update or add unit tests for any logic changed by a fix.
493. **Exclusion Control**: For coverage exclusions necessitated by a fix, keep the scope minimal and justify per `engineering-skill.md`.

## Definition of Done (PR Comment Resolution)

494. [ ] All unresolved review comments have been classified and evaluated.
495. [ ] Valid findings have been fixed or have an auditable response comment.
496. [ ] Not-valid findings have a response comment citing standards or design intent.
497. [ ] Build and relevant tests pass after all fixes are applied.
498. [ ] Triage summary has been reviewed with the engineer/user.
499. [ ] Fixes are committed with the correct Jira prefix following `commit-skill.md`.
500. [ ] Response comments are posted on the PR for non-code resolutions.
501. [ ] PR description is updated to reflect the resolution cycle when appropriate.
