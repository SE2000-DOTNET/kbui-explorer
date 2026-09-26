# SOURCE: branching-skill.md - ".github\skills\copilot\repo-skills\branching-skill.md"

# Branching Skill

Use this skill whenever creating a git branch in this repository.

## Definitions

* Work Item: A tracked unit of work in the team's work tracking system (for example Jira, Azure DevOps Work Items, GitHub Issues, Linear Issues, YouTrack Issues, or similar).
* Work Item Identifier: The unique identifier associated with a work item, such as ICXT5-1781, AB#12345, #456, ENG-789, or similar.

## Rules

259. **Prefix Restriction**: Create new branches exclusively under the `feature/` prefix.
260. **Explicit Work Item Requirement**: Always require the user to explicitly provide the work item identifier before creating a branch. Never assume or infer the identifier from the current branch, commit, or context.
261. **Work Item Fallback**: If the user does not provide a work item identifier, prompt them to supply one. If they cannot provide a work item identifier, invoke `.github/skills/copilot/generic/product-owner-skill.md` to create a new work item.
262. **Missing Work Item Guidance**: In missing-work-item scenarios, advise the engineer to create a new work item and complete the Product Owner flow before branch creation.
263. **Descriptive Suffix**: Keep the remainder of the branch name short and descriptive when additional text is needed.
264. **Branch Protection**: Do not rename or recreate the current branch unless the user explicitly asks.
265. **Base Branch State**: Always fetch the latest `main` from remote and create feature branches only from the latest `origin/main` state.

## Branch Format

Required patterns:

* `feature/<WORK_ITEM>`
* `feature/<WORK_ITEM>-<short-description>`

### Examples

* `feature/ICXT5-1781`
* `feature/ICXT5-1781-fix-terraform-plan`
* `feature/ABC-1234-add-acr-login`
* `feature/AB#12345-add-deployment-automation`
* `feature/ENG-789-update-api-contract`

## Branch Workflow

266. **Prompt for Work Item**: Prompt the user for a work item identifier every time a branch is created. Do not use or infer it from any other context.
267. **Trigger Fallback**: If not provided, run the Product Owner flow to create a required work item.
268. **Validate Work Item Quality**: In the Product Owner flow, verify the work item contains Acceptance Criteria, QA Steps, assignee, and iteration/sprint placement as requested before branch creation.
269. **State Verification**: Ensure the work item is assigned to the engineer and moved to the appropriate active state (for example, `In Progress`) according to the team's workflow before creating the branch.
270. **Workspace Safety Check**: Ensure the working tree is safe for branch switching (no unintended uncommitted work that would be disrupted).
271. **Fetch Remote Main**: Fetch latest remote main via `git fetch origin main`.
272. **Checkout Main**: Switch to local main via `git checkout main`.
273. **Fast-Forward Main**: Fast-forward local main to the latest remote state via `git pull --ff-only origin main`.
274. **Construct Name**: Build the final branch name using the `feature/` prefix.
275. **Branch Creation**: Create the branch from the updated main state with `git checkout -b <branch-name>`.
276. **Push Gate**: Push the new branch to the remote repository only when explicitly requested.

## Notes

277. Prefer lowercase characters for descriptive suffixes.
278. Use hyphens to separate words cleanly within the descriptive suffix.
279. The work item identifier segment must preserve its canonical format as defined by the team's tracking system.
280. Do not assume a specific work tracking platform; support any system that uses a unique work item identifier.
281. When a repository-specific branch naming convention exists, use the work item identifier format required by that convention.