# Parent Copilot Instructions

**SOURCE:** copilot-instructions.md - `\copilot\copilot-instructions.md`

Apply these parent instructions to all work in this repository.

## Rule 0 - Highest-Priority Global Rule: Absolute No Hallucination

This rule overrides every other rule, heuristic, default, and convention in this repository, in any skill, in any instruction file, and in model defaults. If anything conflicts with Rule 0, **Rule 0 wins**.

1. **Never invent or guess facts.** Do not fabricate URLs, endpoints, schema fields, namespaces, casing, credentials, ports, version numbers, file paths, commit SHAs, environment names, API behaviors, package APIs, configuration keys, or any other technical detail.
2. **State unknowns explicitly.** When a fact required to answer is missing, unverified, stale, or only partially supported by sources you can cite, say so plainly to the user. Use language like *"not confirmed,"* *"not in the sources I have,"* or *"I'd need to verify against <source> before answering."* Never fill the gap with a plausible-sounding guess.
3. **Cite the source for every non-trivial technical claim.** Name the source (file path, doc title, URL, page number). If you cannot cite, you cannot assert.
4. **When in doubt, re-fetch.** Reopen the relevant doc, repo file, or page (with the user's interactive login if behind SSO) before answering. Do not paraphrase from memory if it matters.
5. **Ask, don't assume.** If a user's question depends on a missing fact, ask the user or surface the gap as an open question - do not pick a value and proceed.
6. **No silent extrapolation.** Do not extend a documented pattern into an undocumented one without flagging the extrapolation as your own inference and asking the user to confirm.
7. **Secrets are never invented.** Do not fabricate usernames, passwords, tokens, connection strings, or vault paths. Always use placeholders and tell the user where the real value lives.

If at any point you find yourself about to assert something you cannot trace back to a cited source, stop and convert the assertion into either a cited fact, a stated unknown, or a question to the user.

---

Domain-specific reinforcement of Rule 0 lives in:
* `.github/skills/copilot/domain/` - Project Ember / GK platform domain expert: architecture, glossary, pitfalls, source hierarchy. Apply any time GK or Project Ember is in scope.


## Skill Taxonomy

All skills in this repo are organized under `.github/skills/copilot/` and must remain categorized as follows:

### 1. Generic skills:
* `.github/skills/copilot/generic/`

### 2. Repo workflow skills:
* `.github/skills/copilot/repo-skills/`

### 3. Domain skills:
* `.github/skills/copilot/domain/`

### 4. Software engineering skills:
* `.github/skills/copilot/software-engineering/`

### 5. System engineering skills:
* `.github/skills/copilot/system-engineering/`

### 6. Quality Assurance skills:
* `.github/skills/copilot/quality-assurance/`

Do not place a skill in the wrong category. If a skill spans multiple categories, keep one canonical location and reference it from other docs.

## Model/Agent Separation Rules

Use the same category model regardless of agent/model.

### 1. Copilot track:
* Skills under `.github/skills/copilot/` are the source of truth for Copilot-guided workflows.

### 2. Other model tracks (if added later):
* Create a parallel top-level model folder (for example `.github/skills/<model-name>/`) and preserve the same category split:
  * `generic/`
  * `repo-skills/`
  * `software-engineering/`
  * `system-engineering/`
* Do not mix model-specific skills into another model's folder.

## Required Documentation Sync

Whenever any skill file is added, removed, moved, or changed, update all of the following in the same change:

1. `README.md`
2. `.github/copilot-skills-instructions/software-engineering-instructions.md`
3. `.github/copilot-skills-instructions/system-engineering-instructions.md`
4. `.github/copilot-skills-instructions/quality-assurnace-instructions.md`

* Preserve category ordering (Repo Skills, Generic, Software Engineering, System Engineering).

## README Update Procedures

If the change affects category placement or naming, ensure all references are updated across the repo.

### 1. Adding a new skill:
* Add a bullet point under the appropriate category (Repo Skills, Generic, Software Engineering, or System Engineering).
* Use the format: `- .github/skills/copilot/<category>/<skill-name>.md - <one-line description>`.
* Keep descriptions concise and outcome-focused.

### 2. Updating an existing skill (e.g., renaming, moving files within a category):
* Update the skill entry path and/or description to reflect the change.
* Do not change the category unless the skill is being moved to a different category folder.

### 3. Deleting a skill:
* Remove the bullet point entirely from the README.

### 4. Moving a skill to a different category:
* Remove the bullet from the old category section.
* Add the bullet to the new category section with updated path.

### 5. Maintenance and consistency:
* Ensure all paths use absolute repository paths (e.g., `.github/skills/copilot/<category>/...`).
* Keep descriptions consistent with the skill's actual scope and purpose.

## Commit Gate (Mandatory)

Before committing skill changes, verify documentation sync is complete.

1. If a commit is attempted without required updates to the three files above, stop.
2. Prompt the user that required documentation files are out of sync.
3. Offer to update those files automatically before proceeding with the commit.
4. Only proceed after user confirmation.

## Scope

These parent instructions apply to all contributors and coding agents working in this repository.

## Instruction Set Selection

When making code or documentation changes in this repository:
* For changes to generic, repo-workflow, or software-engineering skills and content, apply updates to `.github/copilot-skills-instructions/software-engineering-instructions.md`.
* For changes to system-engineering skills and content or applicable changes are made to generic or repo-workflow, apply updates to `.github/copilot-skills-instructions/system-engineering-instructions.md`.
