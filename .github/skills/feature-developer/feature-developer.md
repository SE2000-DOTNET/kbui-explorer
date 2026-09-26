# SOURCE: feature-developer-skill.md - C:\Users\jr59274\developer\POS-AiAgentSkills\.github\skills\copilot\software-engineering
# Feature Developer Skill

Use this skill whenever the user asks for end-to-end implementation of a Jira story.

## Purpose

427. **Intake to Delivery**: Drive consistent story delivery from Jira intake through PR creation.
428. **Standard Enforcement**: Enforce branch, engineering, commit, and PR standards via existing repository skills.
429. **Owner Autonomy**: Keep the owner in control through explicit confirmation gates before commit and PR actions.

## Inputs

430. **Jira Ticket Key** (for example: `ICXT5-1737`).
431. **Implementation Constraints**: Optional constraints from the owner (scope limits, rollout constraints, deadlines).

## Required Dependencies

432. `.github/skills/copilot/repo-skills/branching-skill.md`
433. `.github/skills/copilot/software-engineering/engineering-skill.md`
434. `.github/skills/copilot/repo-skills/commit-skill.md`
435. `.github/skills/copilot/repo-skills/pull-request-skill.md`

## Workflow

436. **Analyze Intake**: Confirm the Jira ticket key and open/read the story details.
437. **Understand Requirements**: Read and understand the Jira Acceptance Criteria (AC) and QA steps before coding.
438. **Align Alignment**: Summarize implementation intent back to the owner in concise delivery terms.
439. **Isolate Codebase**: Invoke the branching skill to create a feature branch using repository naming rules.
440. **TDD Implementation Loop**: Implement using a TDD approach combined with the engineering skill:
   * Write or update tests first for expected behavior and edge cases from the AC.
   * Implement minimal production code to satisfy failing tests.
   * Refactor safely while preserving passing tests.
   * Run relevant tests/build and required vulnerability checks for touched ecosystems.
441. **Verify Completion**: Validate completion against Jira AC and QA steps and list test evidence.
442. **Commit Gate**: Ask the owner for explicit commit approval.
443. **Record Progress**: After approval, invoke the commit skill and create commit(s) with the Jira-prefixed message format.
444. **PR Gate**: Ask the owner for explicit PR approval.
445. **Publish Changes**: After approval, invoke the PR skill to prepare and create/update the PR with required reviewers and validation summary.

## Confirmation Gates (Owner Approval)

446. **Commit Guard**: Do not commit until the owner explicitly confirms the commit.
447. **PR Guard**: Do not create/update a PR until the owner explicitly confirms PR creation.
448. **Feedback Loop**: If the owner requests changes after a review, loop back through the engineering workflow and re-validate.

## TDD Delivery Checklist

* 449. **AC Mapping**: Tests directly map to AC behaviors and QA scenarios where practical.
* 450. **Defensive Validation**: At least one negative/error-path test exists for each high-risk behavior change.
* 451. **Integration Sufficiency**: Integration tests are included when unit tests alone cannot prove behavior.
* 452. **Pre-Flight Validation**: Build/tests pass for touched scope before requesting commit approval.
* 453. **Minimal Exclusions**: Any non-testable wiring/config code uses minimal, justified coverage exclusion.

## Output Expectations

454. **Branch**: Created per the branching skill specifications.
455. **Codebase**: Code and tests updated to completely satisfy Jira AC.
456. **Evidence**: Validation evidence summarized clearly.
457. **Commit**: Created only after explicit owner confirmation.
458. **Pull Request**: Created only after explicit owner confirmation, following PR skill requirements.

## Notes

459. If Jira AC or QA steps are missing or ambiguous, pause and ask the owner to clarify before beginning implementation.
460. If the scope exceeds a single story, propose follow-on stories and keep the current implementation strictly bounded.
461. Keep all updates fully traceable to the Jira ticket in branch, commit, and PR metadata.
