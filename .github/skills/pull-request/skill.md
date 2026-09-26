# SOURCE: pull-request-skill.md - ".github\skills\copilot\repo-skills\pull-request-skill.md"

# Pull Request Skill

Use this skill whenever creating, preparing, or reviewing a pull request in this repository.

## Definitions

* Work Item: A tracked unit of work in the team's work tracking system (for example Jira, Azure DevOps Work Items, GitHub Issues, Linear Issues, YouTrack Issues, or similar).
* Work Item Identifier: The unique identifier associated with a work item, such as ICXT5-1781, AB#12345, #456, ENG-789, or similar.

## Rules

290. Derive the work item identifier from the current branch name when possible.
291. Build the pull request title as a short summary of Acceptance Criteria scope, prefixed by the work item identifier when available.
292. If the branch name does not contain a work item identifier and the repository convention requires one, ask the user for it before creating the pull request.
293. Follow the repository branching convention from .github/skills/copilot/repo-skills/branching-skill.md.
294. Follow the repository commit convention from .github/skills/copilot/repo-skills/commit-skill.md before opening a pull request.
295. Invoke .github/skills/copilot/software-engineering/code-review-skill.md before creating or updating a pull request.
296. Do not create or update a pull request until review comments are triaged (addressed or explicitly ignored with rationale) and relevant scope has been validated.
297. Merge to main only through pull requests.
298. Require a minimum of 1 reviewer for all pull requests, with GitHub Copilot as an additional reviewer.
299. Add GitHub Copilot as a reviewer on all pull requests.
300. If the correct target branch or release path is not clearly confirmed from accessible repository context, ask the user before creating the pull request.
301. After PR creation, update the related work item to the appropriate review or pull-request state according to the team's workflow.
302. Before creating/updating a PR targeting main, verify whether main is ahead of the feature branch; if so, merge latest main into the feature branch and resolve conflicts before PR creation.
303. Enable auto-merge for PRs after required checks/review policies are satisfied.
304. Use the PR title as the merge title and merge comment/body.

## Title Format

Preferred format:

* <WORK_ITEM>: <short AC summary>

Examples:

* ICXT5-1781: Enforce latest-main branch creation before feature work
* TASK-123: Add endpoint validation with deferred integration contract

## Pull Request Workflow

305. Run git branch --show-current.
306. Extract the work item identifier from the branch name when present.
307. Target main unless the user explicitly confirms a different release flow.
308. Sync with latest main and check divergence:
    * git fetch origin main
    * Compare branch against origin/main.
309. If main is ahead of the branch, merge origin/main into the feature branch before PR creation.
310. Resolve merge conflicts, run relevant validation for resolved areas, and commit merge resolution if needed.
311. Run .github/skills/copilot/software-engineering/code-review-skill.md and .github/skills/copilot/software-engineering/engineering-skill.md checks for touched scope.
312. Triage local/automated review comments: address issues or document concise rationale for ignored comments.
313. Review triage outcomes with the engineer/user, then finalize commit(s).
314. Build the PR title as <WORK_ITEM>: <short AC summary> when a work item identifier exists; otherwise use a concise summary of the work.
315. Build PR description from high-level details of all commits in scope (group related commits; avoid low-level diff noise).
316. Include the validation performed for the touched scope.
317. Highlight any deployment, release, configuration, or environment impacts.
318. Ensure at least 1 reviewer is requested and include GitHub Copilot as an additional reviewer.
319. Create or update the pull request only when requested.
320. Enable auto-merge on the PR.
321. Set merge title and merge comment/body to exactly match the PR title.
322. After PR creation, update the related work item according to the team's workflow and add the PR link to the work item when supported by the tracking system.

## Final Pre-PR Recommendation

323. Before creating the pull request, recommend that the engineer deploy the branch to Dev and validate end-to-end behavior.
324. If the team chooses to deploy from a branch for validation, clearly note this is temporary and must be followed by PR merge to main and redeployment from main.
325. Capture validation evidence (key logs, API responses, or screenshots) and summarize outcomes in the PR description.

## Reviewer Assignment Guidance

326. Always add github-copilot[bot] as a reviewer for each PR.
327. If using GitHub CLI, use: gh pr edit <PR_NUMBER> --add-reviewer github-copilot[bot].
328. If branch protection or permissions block reviewer assignment, note that limitation in the PR description.

## Description Guidance

Include these sections when useful:

329. Commit Highlights
330. Validation
331. Risks or deployment notes
332. Follow-up actions

### Description Construction Rule

333. Derive content from all commits in the PR branch and provide concise high-level bullets for each logical change group.

## POS Strategy Notes

334. A branch must be merged to main before a work item moves past In Progress, unless the team explicitly agrees to a special case.
335. POS team reviewers are expected to be added on pull requests.
336. Deployments to Dev or higher environments must come from main.
337. Temporary deployment to Dev from a branch is only for validating pipeline or environment changes and must be followed by a PR to main and redeployment from main.
338. QA and UAT releases should be controlled by approval checks on the release environment.

## Release Guidance

339. Production releases should be triggered by a version tag in the format v*.*.* on main.
340. The version tag description should contain work item identifiers and summaries in the format <WORK_ITEM>: <summary>.
341. The version tag represents the commit that is in production.
342. If the user is handling a production fix while active development is ongoing, confirm that the fix branch was created from the latest version tag and that the team wants to follow the documented production-fix flow.

## Branch and Target Guidance

343. Prefer a PR title that matches the summary used for the related work item.
344. If a non-main target is required, ask the user which target branch to use before creating the pull request.
345. Do not guess release promotion flow when the user is requesting a production fix or special-case release.

## Notes

346. Keep PR summaries concise and operationally useful.
347. Call out workflow, infrastructure, secret, or environment changes explicitly.
348. If validation was limited by environment or permissions, state that clearly in the PR description.