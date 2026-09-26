# SOURCE: product-owner-skill.md - ".github\skills\copilot\generic\product-owner-skill.md"

# Product Owner Skill

Quick index of the product owner skill workflow guidelines.

This file defines a focused workflow with enforceable rules, trigger conditions, and expected outputs when no work item is associated with a branch request.

## Purpose

* **Ensure** engineering work starts from a properly scoped work item.
* **Standardize** work item quality with detailed acceptance criteria and QA steps.
* **Enforce** work item status flow aligned to delivery workflow.

## Definitions

* **Work Item**: A tracked unit of work in the team's work tracking system (for example Jira, Azure DevOps Work Items, GitHub Issues, Linear Issues, YouTrack Issues, or similar).
* **Work Item Identifier**: The unique identifier associated with a work item, such as ICXT5-1781, AB#12345, #456, ENG-789, or similar.
* **Tracking System**: The platform used to manage work items, such as Jira, Azure DevOps, GitHub Issues, Linear, YouTrack, or another approved tool.

## Trigger Conditions

* **Branch Creation Request** occurs and no work item identifier is provided or discoverable.
* **Engineer Confirms** no existing work item is available for the requested work.

## Work Item Context Baseline

* **Reference Work Item** should be identified from the team's tracking system when available.
* **Project, Team, Area, Feature, Component, Labels, or Categories** should follow established repository and team conventions when available.
* **Conventions** dictate using the same parent feature, epic, component, area, or categorization as a related work item unless explicitly overridden.
* **Clarification Gate** requires asking the user to confirm parent feature, epic, area, component, or equivalent categorization if they cannot be verified from accessible context.

## Required Questions Before Work Item Creation

1. Should the new work item be added to the current active sprint/iteration or to the backlog?
2. Who is the engineer assignee (if not already known)?
3. What is the confirmed parent feature, epic, component, area, or equivalent categorization (if not already known)?
4. Which work tracking system should be used, if it cannot be determined from context?

## Work Item Creation Workflow

* **Advise Engineer** that a new work item is required before branch creation can proceed.
* **Gather/Confirm** the following required attributes:
  * Summary
  * Business context
  * Parent feature, epic, or equivalent grouping
  * Component, area, category, or equivalent classification
  * Assignee engineer
  * Sprint/iteration placement (active sprint or backlog)
* **Create Work Item** in the appropriate tracking system and project/team context.
* **Add Details** consisting of Acceptance Criteria and QA Steps using the templates below.
* **Set Assignee** to the target engineer.
* **Place Sprint/Iteration** by adding the work item to the current active sprint or iteration if selected; otherwise leave it in the backlog.
* **Transition Status** to the appropriate active state (for example, `In Progress`) before generating any branches.
* **Verify Fields** to ensure all required information is populated before returning the work item identifier:
  * Summary
  * Description
  * Parent feature, epic, or equivalent grouping
  * Component, area, category, or equivalent classification
  * Assignee
  * Sprint/iteration value (when active sprint or iteration is selected)
  * Acceptance Criteria text
  * QA Steps text
* **Return Work Item Identifier** and create the code branch using the format `feature/<WORK_ITEM>` or `feature/<WORK_ITEM>-<short-description>`.

## Pull Request Handoff Workflow

* **Transition Status** of the work item to the appropriate review or pull-request state (for example, `PR Pending`) after the PR is created.
* **Link Work Item** by adding the PR link directly inside the work item using the team's preferred linking mechanism.
* **Confirm Updates** to verify both status and links were successfully applied.

## Work Item Content Template

### Summary

* Use concise format: `<area>: <outcome>`.

### Description

* Problem statement.
* User/business impact.
* Proposed solution scope.
* Out of scope notes.

### Acceptance Criteria (Required)

* Functional behavior is clearly defined with expected outcomes.
* Error and edge-case behavior is explicitly defined.
* Security and authorization expectations are defined where relevant.
* Telemetry and logging expectations are defined where relevant.
* Backward compatibility or migration impact is addressed.
* Documentation or configuration updates are identified if needed.

### QA Steps (Required)

* Provide clear validation steps that can be executed by QA, engineers, or reviewers.
* Include expected results for each validation activity.
* Include negative and edge-case testing where applicable.
* Include environment-specific validation requirements when relevant.

## Notes

1. Do not create a branch until 