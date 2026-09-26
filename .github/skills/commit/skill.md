# SOURCE: commit-skill.md - C:\Users\jr59274\developer\POS-AiAgentSkills\.github\skills\copilot\repo-skills

# Commit Skill

Use this skill whenever creating a git commit in this repository.

## Definitions

* Work Item: A tracked unit of work in the team's work tracking system (for example Jira, Azure DevOps Work Items, GitHub Issues, Linear Issues, YouTrack Issues, or similar).
* Work Item Identifier: The unique identifier associated with a work item, such as ICXT5-1781, AB#12345, #456, ENG-789, or similar.

## Rules

280. **Work Item Derivation**: Derive the work item identifier from the current branch name when possible.
281. **Message Prefixing**: Prefix the commit message with that work item identifier.
282. **Branch Check Fallback**: If the branch name does not contain a work item identifier, ask the user for one before committing.
283. **Commit History Protection**: Do not amend existing commits unless the user explicitly asks.
284. **Pre-Commit Validation**: Do not create a commit until the relevant changes have been validated for the touched scope.

## Branch Parsing

### Expected Branch Examples

* `feature/ICXT5-1781`
* `feature/ICXT5-1781-updates`
* `feature/ABC-1234-fix-terraform-plan`
* `feature/AB#12345-add-acr-login`
* `feature/ENG-789-update-api-contract`

### Expected Work Item Identifier Examples

* `ICXT5-1781`
* `ABC-1234`
* `AB#12345`
* `ENG-789`
* `#456`

## Commit Workflow

285. **Check Current Branch**: Run `git branch --show-current`.
286. **Extract Work Item Identifier**: Extract the work item identifier from the active branch name when present.
287. **Construct Message**: Build the commit message following the `<WORK_ITEM> <summary>` format when a work item identifier exists.
288. **Fallback Prompt**: If no work item identifier can be determined from the branch name and repository conventions require one, ask the user to provide it before committing.
289. **Stage Intended Files**: Stage only the intentional files targeted for the change.
290. **Execution Gate**: Commit the staged files, and push only when explicitly requested.

## Commit Message Format

Preferred format:

* `<WORK_ITEM> <summary>`

Examples:

* `ICXT5-1781 Fix registry puller RG and Key Vault naming`
* `ICXT5-1781 Remove stale Terraform plan vars`
* `ABC-1234 Add ACR login validation`
* `ENG-789 Update API contract for deferred processing`
* `AB#12345 Add deployment automation workflow`

## Notes

291. Keep commit summaries concise and action-oriented.
292. Use sentence-style summaries without trailing punctuation.
293. Preserve the canonical work item identifier format as defined by the team's tracking system.
294. Do not assume a specific work tracking platform; support any system that uses a unique work item identifier.
295. When repository conventions require a work item reference, every commit should include the work item identifier as the message prefix.
296. If a branch contains a valid work item identifier, prefer using that identifier consistently across all commits in the branch.
