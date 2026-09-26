## SOURCE: root-cause-analysis-skill.md - C:\Users\jr59274\developer\POS-AiAgentSkills\.github\skills\copilot\software-engineering

## Root Cause Analysis Skill

Use this skill whenever addressing defects, incidents, production issues, regressions, failures, or unexpected system behavior.

### Purpose

Identify and resolve the true cause of an issue rather than masking symptoms.

This skill exists to improve long-term stability, prevent recurring defects, increase diagnostic quality, and ensure fixes are supported by evidence.

### Relationship to Rule 0

Rule 0 from .github/copilot-instructions.md applies in full.

Never assume the root cause.

Never claim a cause without evidence.

Never implement a fix prior to understanding the underlying failure.

Treat theories as theories until confirmed by evidence.

### Trigger Conditions

Apply this skill whenever:

- Fixing a bug.
- Investigating an incident.
- Investigating a production issue.
- Resolving a regression.
- Resolving a failed deployment.
- Investigating test failures.
- Resolving performance issues.
- Addressing customer-reported defects.

### Required Dependencies

- .github/skills/copilot/software-engineering/repository-discovery-skill.md
- .github/skills/copilot/software-engineering/engineering-skill.md
- .github/skills/copilot/software-engineering/code-review-skill.md

### Investigation Workflow

### Step 1 - Define Problem

Document:

- Observed behavior
- Expected behavior
- Environment
- Impact
- Reproduction conditions

Avoid speculation.

### Step 2 - Collect Evidence

Gather:

- Logs
- Error messages
- Stack traces
- Metrics
- Screenshots
- API responses
- Test results

All conclusions must be evidence-based.

### Step 3 - Reproduce Issue

Attempt reproduction.

Document:

- Reproduction steps
- Success rate
- Conditions required

If reproduction is not possible, document why.

### Step 4 - Isolate Failure Point

Identify:

- Component
- Service
- Module
- Workflow stage
- Data source

Determine where failure begins.

### Step 5 - Identify Root Cause

Use evidence to determine:

- Why failure occurred
- What condition triggered failure
- Which code path is responsible

Keep root cause distinct from symptoms.

### Step 6 - Root Cause Validation

Validate:

- Evidence supports findings
- Root cause explains observed behavior
- Root cause explains reproduction pattern

If validation fails, continue investigation.

### Step 7 - Fix Design

Document:

- Proposed fix
- Why fix resolves root cause
- Potential side effects
- Rollback considerations

### Step 8 - Regression Protection

Create:

- Unit tests
- Integration tests
- Validation steps

Ensure future occurrences are detected.

### Failure Classification

Classify the issue.

#### Category A

Code Defect

#### Category B

Configuration Defect

#### Category C

Infrastructure Defect

#### Category D

Data Defect

#### Category E

Process Defect

#### Category F

Requirement Defect

### Five Why Analysis

Perform Five Why analysis when practical.

Example:

Why did the API fail?

Why was validation skipped?

Why was validation missing?

Why was testing inadequate?

Why was the requirement unclear?

Document findings.

### STOP Conditions

Do not implement a fix when:

- Root cause has not been identified.
- Evidence is insufficient.
- Reproduction is incomplete and reason is unknown.
- Multiple root cause candidates exist and remain unverified.

Pause and continue investigation.

### Required Output

Provide:

#### Problem Summary

Document observed behavior.

#### Business Impact

Describe impact.

#### Root Cause

Document verified root cause.

#### Evidence

List supporting evidence.

#### Fix Description

Describe solution.

#### Validation

Describe verification performed.

#### Regression Protection

Describe tests added.

#### Risk Assessment

Identify remaining concerns.

### Definition of Done

Issue resolution is complete only when:

- Problem is understood.
- Root cause is identified.
- Root cause is supported by evidence.
- Fix addresses root cause.
- Validation passes.
- Regression protection exists.
- Remaining risks are documented.

### Notes

Fixing symptoms is not considered resolution.

A defect is not closed until the verified root cause has been addressed and appropriate regression protection has been implemented.