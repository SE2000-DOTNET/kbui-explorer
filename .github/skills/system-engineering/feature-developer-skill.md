# System Engineering Feature Developer Skill

#SOURCE: `feature-developer-skill.md` - `C:\Users\jr59274\developer\POS-AiAgentSkills\.github\skills\copilot\system-engineering`

Use this skill whenever the user asks for end-to-end implementation of a Jira story for system-engineering work.

## Purpose

675. **Consistent Delivery**: Drive consistent story delivery from Jira intake through deployment readiness.
676. **Standard Enforcement**: Enforce system-engineering, new-file, and deployment standards via existing skills.
677. **Owner Control**: Keep the owner in control through explicit confirmation gates at commit, PR, and deployment stages.

## Inputs

678. **Jira Ticket Key** (for example: `ICXT5-1737`).
679. **Implementation Constraints**: Optional implementation constraints from owner (scope limits, rollout constraints, deadlines).

## Required Dependencies

* 680. `.github/skills/copilot/system-engineering/engineering-skill.md`
* 681. `.github/skills/copilot/system-engineering/new-file-create-skill.md`
* 682. `.github/skills/copilot/system-engineering/code-deployment-skill.md`

## Workflow

683. **Analyze Story**: Confirm the Jira ticket key and open/read the story details.
684. **Understand Requirements**: Read and understand the Jira Acceptance Criteria and QA steps before implementation.
685. **Align Scope**: Summarize implementation intent back to the owner in concise delivery terms.
686. **Execute Scripting**: Implement using the system-engineering skill:
   * Apply PowerShell and Batch coding standards as applicable.
   * Use required script headers, metadata patterns, logging conventions, and alignment rules.
   * Follow code development flow stage gates with owner confirmations.
687. **Net-New Validation**: If implementation introduces net-new files, invoke new-file-create-skill and complete its owner-gated procedure.
688. **Quality Assurance**: Validate completion against Jira AC and QA steps and list test evidence.
689. **Commit Request**: Ask owner for explicit commit approval.
690. **Record Progress**: After approval, create commit(s) with Jira-prefixed message format.
691. **PR Request**: Ask owner for explicit PR approval.
692. **Publish Changes**: After approval, create or update PR with required reviewers and validation summary.
693. **Deployment Gate**: If owner requests deployment planning or execution, invoke code-deployment-skill and follow all stage gates through release validation.

## Confirmation Gates (Owner Approval)

694. **Commit Guard**: Do not commit until owner explicitly confirms commit.
695. **PR Guard**: Do not create/update PR until owner explicitly confirms PR creation.
696. **Deployment Guard**: Do not proceed to the next deployment stage without owner confirmation at each stage gate.
697. **Iterative Feedback**: If owner requests changes after review, loop back through system-engineering workflow and re-validate.

## System Engineering Delivery Checklist

* 698. **Language Mapping**: Script standards map to the appropriate language (PowerShell and/or Batch).
* 699. **Structural Integrity**: Logging and control-structure conventions are consistent with standards.
* 700. **Onboarding Complete**: New-file onboarding process is completed when applicable.
* 701. **Evidence Tracking**: Deployment readiness evidence is prepared when deployment is in scope.
* 702. **Summary Verification**: Validation evidence is captured and summarized before requesting commit/PR approvals.

## Output Expectations

703. System-engineering implementation and validation completed against Jira AC.
704. New-file process completed where required.
705. Commit created only after owner confirmation.
706. PR created only after owner confirmation.
707. Deployment flow (if requested) executed with owner confirmation at every stage.

## Notes

708. If Jira AC or QA steps are missing or ambiguous, pause and ask owner to clarify before implementation.
709. If scope exceeds a single story, propose follow-on stories and keep current implementation bounded.
710. Keep all updates traceable to the Jira ticket in commit, PR, and deployment records.

