# Architecture Review Skill

## Purpose

Evaluate architectural quality, maintainability, scalability, and technical risk.

---

## Trigger Conditions

Use when:

- New services are created
- Major refactors occur
- Architectural decisions are proposed
- Technical debt is reviewed

---

## Review Categories

### SOLID

Assess all SOLID principles.

---

### Coupling

Evaluate service and component coupling.

---

### Cohesion

Evaluate responsibility grouping.

---

### Dependency Direction

Confirm dependencies flow inward.

---

### Security

Review:

- Trust boundaries
- Sensitive data handling
- Least privilege

---

### Observability

Review:

- Logging
- Metrics
- Tracing

---

### Scalability

Review:

- Concurrency
- Resource utilization
- Growth concerns

---

## Severity Levels

### Critical

Immediate remediation required.

### High

Must be addressed before release.

### Medium

Should be addressed.

### Low

Improvement opportunity.

---

## Common Pitfalls

- Circular dependencies
- Shared mutable state
- Architecture drift

---

## Anti-Patterns

- God services
- Cross-layer leakage
- Tight infrastructure coupling

---

## Definition of Done

- Findings documented
- Risks classified
- Remediation plan proposed