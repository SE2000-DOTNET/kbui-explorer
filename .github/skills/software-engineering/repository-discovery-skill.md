## SOURCE: repository-discovery-skill.md - C:\Users\jr59274\developer\POS-AiAgentSkills\.github\skills\copilot\software-engineering

## Repository Discovery Skill

Use this skill before making code changes in this repository.

### Purpose

Ensure modifications are based on a complete understanding of the existing repository structure, architectural patterns, testing strategy, deployment model, and implementation conventions.

This skill exists to prevent implementation based on assumptions, reduce duplicate solutions, preserve architectural consistency, and improve long-term maintainability.

### Relationship to Rule 0

Rule 0 from .github/copilot-instructions.md applies in full.

Never assume repository behavior.

Never assume architectural intent.

Never create new patterns when an approved pattern already exists.

If repository behavior cannot be verified through source code or documentation, stop and request clarification.

### Trigger Conditions

Apply this skill whenever:

- Implementing a Jira story.
- Fixing a defect.
- Refactoring code.
- Extending an existing feature.
- Creating a new service or module.
- Modifying infrastructure code.
- Modifying CI/CD workflows.
- Modifying shared libraries.
- Working within an unfamiliar repository.

### Required Dependencies

- .github/skills/copilot/software-engineering/story-readiness-skill.md
- .github/skills/copilot/software-engineering/engineering-skill.md
- .github/skills/copilot/software-engineering/architecture-review-skill.md
- .github/skills/copilot/software-engineering/feature-developer-skill.md

### Discovery Workflow

Perform discovery before implementation.

Implementation should not begin during discovery.

### Repository Structure Discovery

Identify:

- Solution files
- Project files
- Startup locations
- Dependency injection configuration
- Shared libraries
- Infrastructure folders
- CI/CD folders
- Test projects

Document findings.

### Architecture Discovery

Identify:

- Application architecture pattern
- Layer boundaries
- Domain organization
- Service boundaries
- Infrastructure dependencies
- Cross-service communication

Determine whether the repository follows:

- Layered Architecture
- Clean Architecture
- Hexagonal Architecture
- Vertical Slice Architecture
- Other documented pattern

Never assume architecture.

### Existing Pattern Discovery

Search for:

- Similar implementations
- Existing services
- Existing APIs
- Existing repositories
- Existing handlers
- Existing validators
- Existing middleware

Prefer extending approved patterns over introducing new ones.

### Dependency Discovery

Identify:

- Internal dependencies
- External dependencies
- NuGet packages
- NPM packages
- Infrastructure dependencies
- Shared services

Document any dependency concerns.

### Security Discovery

Identify:

- Authentication model
- Authorization model
- Secret management approach
- Managed identity usage
- Security validation patterns

Flag deviations for review.

### Data Access Discovery

Identify:

- Database technologies
- Repository patterns
- ORM usage
- Existing migrations
- Data ownership boundaries

Document impacted data areas.

### Testing Discovery

Identify:

- Unit test framework
- Integration test framework
- Existing test patterns
- Mocking approach
- Test project structure

New tests should follow existing conventions whenever practical.

### CI/CD Discovery

Identify:

- Build workflow
- Validation workflow
- Deployment workflow
- Release workflow
- Environment promotion path

Document impacted pipelines.

### Reuse Assessment

Before creating:

- New service
- New helper
- New abstraction
- New utility
- New pipeline

Verify an equivalent pattern does not already exist.

Reuse should be preferred over duplication.

### Risk Identification

Document:

- Architectural risks
- Security risks
- Performance risks
- Dependency risks
- Deployment risks

Escalate significant risks.

### STOP Conditions

Pause implementation when:

- Repository structure is not understood.
- Existing implementation pattern cannot be identified.
- Architectural boundaries are unclear.
- Security controls cannot be verified.
- Target implementation location is uncertain.

Request clarification before proceeding.

### Required Output

Provide:

#### Repository Summary

Summarize repository structure.

#### Architectural Findings

Summarize architecture observations.

#### Existing Pattern Recommendations

