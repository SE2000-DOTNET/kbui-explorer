## SOURCE: story-readiness-skill.md - C:\Users\jr59274\developer\POS-AiAgentSkills\.github\skills\copilot\software-engineering

## Story Readiness Skill

Use this skill before beginning implementation of any Jira story in this repository.

### Purpose

Ensure development work begins only when sufficient requirements, acceptance criteria, QA expectations, dependencies, and business context are available.

This skill exists to prevent implementation against ambiguous, incomplete, or poorly-defined Jira stories.

### Relationship to Rule 0

Rule 0 from .github/copilot-instructions.md applies in full.

Never infer missing requirements.

Never assume expected behavior.

Never create design decisions to compensate for missing story details.

When information is missing, stop and request clarification.

### Trigger Conditions

Apply this skill whenever:

- A Jira story is provided for implementation.
- A Jira story is provided for estimation.
- A Jira story is provided for technical analysis.
- A branch is requested for work associated with a Jira story.
- A feature implementation request references an existing Jira ticket.

### Required Dependencies

- .github/skills/copilot/generic/product-owner-skill.md
- .github/skills/copilot/software-engineering/feature-developer-skill.md

### Story Readiness Assessment

Validate all sections before implementation begins.

#### Business Context

Verify:

- Problem statement exists.
- Business value exists.
- Desired outcome is defined.
- User impact is identified.

Questions:

- Why is this work needed?
- What problem is being solved?
- How will completion be measured?

#### Acceptance Criteria

Verify:

- Acceptance Criteria exist.
- Acceptance Criteria are testable.
- Acceptance Criteria are measurable.
- Acceptance Criteria contain expected behavior.
- Error-path expectations are defined.

Reject criteria such as:

- "Works correctly"
- "Functions as expected"
- "Handle errors appropriately"

Require explicit expected results.

#### QA Validation

Verify:

- QA steps exist.
- Positive validation paths exist.
- Negative validation paths exist.
- Expected results are documented.

#### Scope Validation

Verify:

- In-scope behavior is identified.
- Out-of-scope behavior is identified.
- Boundaries of implementation are clear.

#### Dependency Validation

Verify:

- External system dependencies are identified.
- Service dependencies are identified.
- Database dependencies are identified.
- Infrastructure dependencies are identified.

#### Security Validation

Verify whether the story impacts:

- Authentication
- Authorization
- Secrets
- Identity
- Sensitive data

If yes:

Flag for additional review.

### Story Classification

Classify the story.

#### Level 1

Low Risk

Examples:

- UI text change
- Logging update
- Simple bug fix

#### Level 2

Moderate Risk

Examples:

- Feature enhancement
- API modification
- Business-rule update

#### Level 3

High Risk

Examples:

- Cross-service behavior
- Database modification
- Infrastructure modification

#### Level 4

Critical Risk

Examples:

- Authentication changes
- Authorization changes
- Public endpoint changes
- Security-sensitive functionality

### STOP Conditions

Do not begin implementation if:

- Business purpose is missing.
- Acceptance Criteria are missing.
- Acceptance Criteria are ambiguous.
- QA steps are missing.
- Required dependencies are unknown.
- Security requirements are unclear.
- Expected outcomes are not defined.

Pause implementation.

Request clarification.

### Required Output

Provide:

#### Story Readiness Result

Status:

- Ready
- Partially Ready
- Not Ready

#### Risk Level

- Level 1
- Level 2
- Level 3
- Level 4

#### Gaps Identified

List all missing details.

#### Recommendation

- Proceed
- Clarify Requirements
- Architecture Review Required
- Product Owner Review Required

### Definition of Done

Story may be considered Ready only when:

- Business objective is understood.
- Acceptance Criteria are testable.
- QA validation exists.
- Dependencies are known.
- Scope is defined.
- Security implications are understood.
- No STOP conditions remain active.

### Notes

A Jira story being assigned or marked In Progress does not indicate readiness.

Readiness must be independently verified.

No implementation should begin until this skill reports a status of Ready.