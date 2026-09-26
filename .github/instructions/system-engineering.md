# SOURCE: system-engineering-instructions.md - ".github\copilot-skills-instructions\system-engineering-instructions.md"
# System Engineering Copilot Instructions

Use all applicable Copilot skills in this repository under `.github/skills/copilot/`.

## Global Rules (Highest Priority)

These rules override all other rules, heuristics, and defaults in this file.

74. **No hallucination under any circumstances** — never invent facts, URLs, schema fields, config keys, version numbers, file paths, or any other technical detail not confirmed by a cited source.
75. **Stick to the facts** — every non-trivial technical claim must be traceable to a source (file path, doc title, URL). If you cannot cite it, do not assert it.
76. **Ask when in doubt or unclear** — when a required fact is missing, ambiguous, or only partially supported, ask the user before proceeding. Do not guess, assume, or extrapolate silently.
77. **Use Playwright to access apps when needed** — when working with web applications or SSO-protected sources, use Playwright with interactive user login rather than guessing or fabricating content.

## Shared Skills (Always Apply)

* `.github/skills/copilot/repo-skills/branching-skill.md`
* `.github/skills/copilot/repo-skills/commit-skill.md`
* `.github/skills/copilot/repo-skills/pull-request-skill.md`
* `.github/skills/copilot/generic/product-owner-skill.md`

## Domain Skills (Apply When Relevant)

* `.github/skills/copilot/domain/ember-expert-skill.md` — apply for any Project Ember / GK platform work; provides architecture, glossary, pitfalls, and source hierarchy.


## System Engineering Skills (Required)

* `.github/skills/copilot/system-engineering/engineering-skill.md`
* `.github/skills/copilot/system-engineering/new-file-create-skill.md`
* `.github/skills/copilot/system-engineering/code-deployment-skill.md`
* `.github/skills/copilot/system-engineering/feature-developer-skill.md`


## Shared Workflow Rules

78. **GitHub Repositories**: If working against a GitHub repository, follow branching, commit, and PR skills for branch/commit/PR operations.
79. **Work Item Standards**: Maintain consistent traceability across branches, commits, pull requests, and delivery activities when a work item, feature request, requirement, issue, change request, or tracking identifier is available.
80. **Branching Fallback**: If no work item, requirement, or tracking identifier is available when branching is requested, use product-owner flow first.

## Operating Rules
81. **PowerShell & Batch**: Use `engineering-skill.md` for PowerShell/Batch standards and owner-gated stage flow.
82. **Net-New Files**: Use `new-file-create-skill.md` whenever introducing net-new files that require RemoteLink and transfer-script onboarding.
83. **Deployment Orchestration**: Use `code-deployment-skill.md` for deployment planning/execution and enforce stop-and-owner-confirm gates at every stage.
84. **Feature or Requirement Execution**: Use `feature-developer-skill.md` for end-to-end implementation of approved work items, requirements, features, enhancements, defects, tasks, or change requests within system-engineering scope.
85. **Approval Gates**: Do not proceed to the next stage, commit, PR, or deployment action until explicit owner confirmation is captured.


## Scope

Apply these instructions for system engineering scripting, deployment orchestration, net-new file rollout, and release validation work.
