#SOURCE: code-deployment-skill.md - C:\Users\jr59274\developer\POS-AiAgentSkills\.github\skills\copilot\system-engineering

# Code Deployment Skill

Use this skill whenever planning, validating, approving, or executing code deployment activities for system-engineering work.

## Purpose

502. **Standardized Releases**: Standardize release execution from review through post-release validation.
503. **Guardrail Enforcement**: Enforce deployment guardrails from Confluence guidance and linked references.
504. **Granular Approval**: Require owner approvals at every stage before proceeding.

## Authoritative References

* 505. **Code Deployment Process** — `https://atlassian.net`
* 506. **Standard Code Signing** — `https://atlassian.net`
* 507. **Introducing New Files** — `https://atlassian.net`
* 508. **Code Development Flow** — `https://atlassian.net`
* 509. **Update Process** — `https://atlassian.net`
* 510. **Pre-Production Deployment Steps** — `https://atlassian.net`
* 511. **Deployment History** — `https://atlassian.net`

## Deployment Stages

Use these ordered stages for deployment. Stop after each stage and wait for owner confirmation.

1. Submit Code for Review
2. Code Signing
3. Change Request
4. Net New Files (if applicable)
5. Release to Production
6. Notify Everyone
7. Test the Release

## Hard Stop Rule

519. **Halt Execution**: At the end of each stage, stop execution immediately.
520. **Present Evidence**: Present stage evidence to the owner for review.
521. **Explicit Confirmation**: Request explicit owner confirmation to proceed.
522. **Strict Gate**: Do not start the next stage until owner confirmation is received.

## Stage Gates and Owner Instructions

### Stage 1: Submit Code for Review
523. [ ] Confirm files are placed in the POS Payments Reviews folder: `\\elnasa\PosPayments\Reviews\<initials>`
524. [ ] Confirm review request message is posted in the POS Payments Teams Code Review channel with:
   * Current production file path
   * Path to review copy
   * What changed and why
   * Where/how lab testing was performed
525. [ ] Confirm review is approved before proceeding.
526. *Optional Ready-to-Run Diff Command*: `code --diff "<prod-file-path>" "<review-file-path>"`
527. **Gate**: Stop and wait for owner review and explicit confirmation before Stage 2.

### Stage 2: Code Signing
528. [ ] Determine whether file type requires signing (*PowerShell scripts require signing*).
529. [ ] Confirm correct certificate context and timestamp authority matching `http://digicert.com`.
530. [ ] Confirm signature is successfully applied.
531. [ ] If Carbon Black prompts during signing, confirm approval is explicitly granted.
532. **Gate**: Stop and wait for owner review and explicit confirmation before Stage 3.

#### Recommended Signing Commands
533. Set script path:
   ```powershell
   \$scriptToSign = "<full-path-to-script.ps1>"
   ```
534. Get code-signing cert:
   ```powershell
   \$cert = (dir cert:\currentuser\my\ -CodeSigningCert)
   ```
535. Sign script:
   ```powershell
   Set-AuthenticodeSignature -FilePath \(scriptToSign -Certificate\)cert -TimestampServer http://digicert.com
   ```

#### Certificate Bootstrap (if needed)
536. Retrieve expected cert by thumbprint.
537. Add to `CurrentUser` Root and `TrustedPublisher` stores.
538. Verify both stores contain the cert.

### Stage 3: Change Request
539. [ ] Confirm change request is created in ServiceNow.
540. [ ] Confirm required fields are complete and high quality:
   * Assignment Group / Change Owner / Category
   * Short Description / Detailed Description
   * Approver / Reason for Change
   * Implementation Plan / Test Plan
   * Remediation/Backout Plan / Risk and Impact
541. [ ] Confirm schedule is set and approvals are obtained.
542. [ ] Confirm change status is ready before release.
543. [ ] Confirm change is closed after implementation.
544. **Gate**: Stop and wait for owner review and explicit confirmation before Stage 4.

### Stage 4: Net New Files
545. [ ] If the release includes net-new files, invoke `.github/skills/copilot/system-engineering/new-file-create-skill.md`.
546. [ ] Confirm RemoteLink task mapping and transfer-script updates are complete.
547. [ ] Confirm owner approval to continue the release.
548. **Gate**: Stop and wait for owner review and explicit confirmation before Stage 5.

### Stage 5: Release to Production
549. [ ] Confirm signed and approved artifact path is correct.
550. [ ] Confirm production parent directory path is correct and ends with `\`.
551. [ ] Confirm release command is executed from `N:\RW\RWS\CMD`:
   ```powershell
   .\CGSReleaseFile.ps1 "<Full\Path\To\Signed\Approved\File.ext>" "<Path\To\Production\Parent\Directory\>"
   ```
552. [ ] Confirm release completion and ensure there are no blocking errors.
553. **Gate**: Stop and wait for owner review and explicit confirmation before Stage 6.

### Stage 6: Notify Everyone
554. [ ] Send email to `retailxissues@caseys.com`.
555. *Subject line must be*: `Store Technology Updates`
556. *Include in body*:
   * Files changed
   * What changed
   * Deployment scope (all stores or subset)
   * Destination paths/targets
557. [ ] Confirm message has been sent.
558. **Gate**: Stop and wait for owner review and explicit confirmation before Stage 7.

### Stage 7: Test the Release
559. [ ] Identify the RemoteLink job responsible for deployment.
560. [ ] Run it against a lab machine when possible.
561. [ ] Confirm the file lands in the exact expected path.
562. [ ] Perform a final functional smoke test.
563. [ ] Confirm release acceptance or rollback decision.
564. **Gate**: Stop and wait for owner final review and explicit closure confirmation.

## Expanded Pre-Production Checklist (Reference-Aligned)

*Use this when deployment involves coordinated windows or service operations.*

565. **Schedule Alignment**: Confirm release day/time with the product/business owner.
566. **Broadcast**: Send advance notification to the impacted distribution list.
567. **Task Disabling**: Schedule disable-task automation before the release window.
568. **UAT Gate**: Confirm final UAT deployment/pipeline success before production push.
569. **Pre-Release Verification**: Run pre-release smoke checks (security init, scheduler access, core behaviors).
570. **Scheduler Adjustments**: On release day, move conflicting scheduled jobs to the next evening when needed.
571. **Branch Synchronization**: Prepare PRs for environment branch sync, but complete them only after production success.

## Operational Reliability Guardrails (History-Derived)

*Apply these safeguards based on recurring deployment-history issues.*

572. **User Isolation**: Verify active users are out of impacted applications before stopping services.
573. **IIS Sequencing**: Stop service monitors before IIS; start IIS before service monitors when bringing services back online.
574. **Backup Redundancy**: Validate backup steps include both server and client directories.
575. **Service Stopping**: Confirm critical services actually halt completely before deployment continues.
576. **Payload Scope**: Validate required dependency files (for example, additional DLLs) are included in the release payload.
577. **Post-Mortem**: Capture issues and takeaways in a deployment-history log after each release.

## Post-Release Store-Update Awareness

*When deployment depends on store update execution behavior:*

578. Ensure update prompts are actioned within expected windows.
579. Ensure EOS and register prerequisites are communicated.
80. Escalate to the Help Desk on stuck update execution before attempting manual intervention.

## Required Checks

581. [ ] Stage gate confirmations are recorded for all required stages.
582. [ ] Required approvals are complete before production release.
583. [ ] Signing requirements are met for PowerShell artifacts.
584. [ ] Net-new file process is completed when applicable.
585. [ ] Notification and release validation are complete.
586. [ ] Change request lifecycle is closed after successful implementation.

## Completion Criteria

587. Deployment executed per the approved plan.
588. Stakeholders notified.
589. Post-release validation passed.
590. Change record and release notes are finalized.
