# SOURCE: system-engineering-skill.md - C:\Users\jr59274\developer\POS-AiAgentSkills\.github\skills\copilot\system-engineering

# System Engineering Skill

Use this skill whenever implementing or reviewing PowerShell scripts, Batch scripts, modules, or automation in this repository.

## Role

591. **Scripting Expert**: Act as a PowerShell and Batch scripting expert for design, reliability, security, and maintainability.
592. **Confluence Enforcement**: Enforce Casey Confluence coding standards as the source of truth for script structure and style.
593. **Production Safety**: Keep scripts production-safe, testable, and automation-friendly.

## Mandatory Standards

Primary standards (authoritative for this repository):
* 594. **Casey Retail Confluence Standard** — `https://atlassian.net`
* 595. **Casey Retail Confluence Standard** — `https://atlassian.net`

### Enforcement Rules
596. **Precedence**: Follow all standards in the Casey documents.
597. **Conflict Resolution**: If any external recommendation conflicts with Casey standards, Casey standards take precedence.
598. **Inaccessible Context Gate**: If a standards page is inaccessible in the current execution context, request the relevant section from the user and pause non-trivial refactors until confirmed.
599. **Net-New Assets**: When generating a net-new script or file, invoke `.github/skills/copilot/system-engineering/new-file-create-skill.md` and follow its procedure.
600. **Deployment Posture**: When performing deployment planning or execution, invoke `.github/skills/copilot/system-engineering/code-deployment-skill.md` and follow all stage gates.

## PowerShell Standards (Confluence-Aligned)

### Script Header and Metadata
601. Include a standard script header block at the top of every script containing:
   * `.SYNOPSIS`
   * `.DESCRIPTION`
   * `.PARAMETER` entries
   * `.EXAMPLE`
   * `Change History` block
602. Include script metadata variables where applicable:
   * `$ScriptName`, `$ScriptLocation`, `$Log`
   * `$ScriptWriteTime`, `$StoreNumber`, `$ComputerType`

### Variable Naming and Comments
603. Use descriptive variable names to improve readability and maintainability.
604. Use single-line and multi-line comments to explain logic and intent.

### Pipelines and Control Structures
605. Use pipelines for concise command chaining.
606. Use control structures for decision and loop logic.
607. Capitalize control structure keywords consistently for readability:
   * `If/Else`, `For`, `Foreach`, `While`, `Try/Catch`, `Do/While`

### Functions and Logging
608. Use functions to encapsulate reusable code.
609. Use standardized logging patterns with level markers:
   * `[BEG]`, `[INFO]`, `[WARN]`, `[ERROR]`, `[END]`

### Organization and Regions
610. Use regions to segment large scripts into logical sections.
611. Use meaningful region names such as `Main Script`, `Variables`, `Functions`, or specific operation names.
612. Reserve regions exclusively for larger blocks, not a few lines of code.

### Alignment
613. Keep code spacing and indentation consistent and vertically readable.
614. Align variable assignment equals signs (`=`) within assignment blocks.

## Batch Standards (Confluence-Aligned)

### Script Header and Metadata
615. Include a standard batch header block with script purpose and Change History.
616. Include key script metadata variables where applicable:
   * `COPYCMD`, `CMPTYPE`, `STORE`, `LOG`
   * Script-specific control flags

### Variable Naming and Comments
617. Use descriptive variable names.
618. Use comment blocks and separators to explain script sections clearly.

### Control Structures and Flow
619. Use consistent, readable control-structure patterns for:
   * `If/Else`
   * `For /F`
   * Loop labels and counters
620. Use labels and subroutines for reusable logic:
   * `Call :Subroutine`
   * `Goto :Label`

### Logging
621. Use timestamped logging patterns with `COMPUTERNAME`, date/time tokens, and clear message text.
622. Write both console and log-file entries when applicable.

## Code Development Flow Gates (Confluence-Aligned)

### Reference Flow
623. Write your code as needed
624. Test in the lab
625. Code review
626. Sign script
627. Change request
628. Release file
629. Email the team

### Execution Rule
630. **Halt Protocol**: Stop at the end of every stage.
631. **Checklist Delivery**: Provide the owner with the specific instruction checklist for the current stage.
632. **Gate Validation**: Do not proceed to the next stage until the owner confirms the go-ahead.

### Stage Gates and Owner Instructions

#### Stage 1: Write your code as needed
633. [ ] Confirm scope boundaries, assumptions, and target files.
634. [ ] Confirm coding standards to apply (PowerShell and/or Batch).
635. [ ] Confirm whether work is complete for implementation scope before moving to lab testing.

#### Stage 2: Test in the lab
636. [ ] Provide or confirm lab environment and test prerequisites.
637. [ ] Run positive and negative-path validation for changed behavior.
638. [ ] Capture test evidence (logs/output/screenshots) and confirm results are acceptable.
639. [ ] Confirm approval to proceed to code review.

#### Stage 3: Code review
640. [ ] Review changes for standards compliance, readability, and maintainability.
641. [ ] Review risk areas: control flow, logging, and script safety.
642. [ ] Confirm all review comments are addressed or explicitly accepted with rationale.
643. [ ] Confirm approval to proceed to script signing.

#### Stage 4: Sign script
644. [ ] Confirm scripts requiring signature are identified.
645. [ ] Confirm signing process/tooling and certificate context are correct.
646. [ ] Verify signature is applied and valid for release use.
647. [ ] Confirm approval to proceed to change request.

#### Stage 5: Change request
648. [ ] Create or update the required change request record.
649. [ ] Attach implementation summary, validation evidence, and rollout/rollback notes.
650. [ ] Confirm required approvals are obtained per team process.
651. [ ] Confirm approval to proceed to release file.

#### Stage 6: Release file
652. [ ] Confirm exact artifact/file version to release.
653. [ ] Confirm destination and release window details.
654. [ ] Verify checksum/signature/version metadata as applicable.
655. [ ] Confirm approval to proceed to team notification.

#### Stage 7: Email the team
656. [ ] Send release communication to the team with scope and impact summary.
657. [ ] Include deployment timing, validation status, and any required actions.
658. [ ] Include rollback/escalation contact details.
659. [ ] Confirm communication was sent and acknowledged as needed.

## Required Checks for Changes

660. [ ] Script header and Change History are present for new or significantly changed scripts.
661. [ ] Required metadata variables are present when the script scenario uses them.
662. [ ] Variable names are descriptive and comments explain non-obvious logic.
663. [ ] Control-structure keyword capitalization is consistent with standards.
664. [ ] Logging pattern is consistent with team format.
665. [ ] Regions are used appropriately in PowerShell scripts.
666. [ ] Assignment alignment is applied within variable blocks.
667. [ ] For Batch scripts, subroutine and loop label flow remains readable and deterministic.
668. [ ] Stage-gate confirmation was recorded for each Code Development Flow step.
669. [ ] For net-new files, New File Create Skill checks are completed and owner confirmations are recorded.

## References

* 670. **PowerShell Coding Standards (Confluence)** — `https://atlassian.net`
* 671. **Batch Coding Standards (Confluence)** — `https://atlassian.net`
* 672. **Code Development Flow (Confluence)** — `https://atlassian.net`
* 673. **New File Create Skill** — `.github/skills/copilot/system-engineering/new-file-create-skill.md`
* 674. **Code Deployment Skill** — `.github/skills/copilot/system-engineering/code-deployment-skill.md`
