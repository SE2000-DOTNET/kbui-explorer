SOURCE: software-engineering-instructions.md - ".github\copilot-skills-instructions\software-engineering-instructions.md"

Software Engineering Copilot Instructions

Use all applicable Copilot skills in this repository under .github/skills/copilot/.

Global Rules (Highest Priority)

These rules override all other rules, heuristics, and defaults in this file.

27. No hallucination under any circumstances - never invent facts, URLs, schema fields, config keys, version numbers, file paths, or any other technical detail not confirmed by a cited source.
28. Stick to the facts - every non-trivial technical claim must be traceable to a source (file path, doc title, URL). If you cannot cite it, do not assert it.
29. Ask when in doubt or unclear - when a required fact is missing, ambiguous, or only partially supported, ask the user before proceeding. Do not guess, assume, or extrapolate silently.
30. Use Playwright to access apps when needed - when working with web applications or SSO-protected sources, use Playwright with interactive user login rather than guessing or fabricating content.

Shared Skills (Always Apply)

• .github/skills/copilot/repo-skills/branching-skill.md
• .github/skills/copilot/repo-skills/commit-skill.md
• .github/skills/copilot/repo-skills/pull-request-skill.md
• .github/skills/copilot/generic/product-owner-skill.md

Domain Skills (Apply When Relevant)

• .github/skills/copilot/domain/ember-expert-skill.md - apply for any Project Ember / GK platform work; provides architecture, glossary, pitfalls, and source hierarchy.

Shared Workflow Rules

31. If working against a GitHub repository, follow branching, commit, and PR skills for branch/commit/PR operations.

32. Keep work item handling consistent with shared skills:
• work-item-first branch behavior
• work-item-prefixed commit messages
• work-item-linked PR title and lifecycle

33. If no work item is available when branching is requested, use product-owner skill flow first.

Software Engineering Skills (Required)

• .github/skills/copilot/software-engineering/engineering-skill.md
• .github/skills/copilot/software-engineering/code-review-skill.md
• .github/skills/copilot/software-engineering/pr-comment-resolver.md
• .github/skills/copilot/software-engineering/feature-developer-skill.md

Operating Rules

34. Use work-item-linked flow for branch, commit, and PR steps via shared repo skills.
35. Run software engineering quality checks from engineering-skill.md (tests, vulnerability checks, SOLID, CI/CD/Terraform checks where applicable).
36. Use code-review-skill.md before commit/PR finalization when review triage is required.
37. Use pr-comment-resolver.md for unresolved PR review-comment workflows.
38. Use feature-developer-skill.md for end-to-end feature or requirement implementation.
39. Do not proceed to commit/PR actions until explicit owner approval gates are satisfied.

Branching Instructions

40. Always follow .github/skills/copilot/repo-skills/branching-skill.md when creating branches.
41. Create branches under the feature/ prefix.
42. Create a unique branch number for branch. Ask for a work item identifier every time a branch is created.
43. If a work item identifier is missing, run product-owner flow before creating branch.
44. Create feature branches from latest origin/main state.

Commit Instructions

45. Always follow .github/skills/copilot/repo-skills/commit-skill.md when creating commits.
46. Prefix commit messages with the work item identifier derived from branch when available.
47. Ask the user for unique branch number before committing.
48. Do not amend commits unless explicitly requested.
49. Stage only intended files.

Pull Request Instructions

50. Always follow .github/skills/copilot/repo-skills/pull-request-skill.md when creating/updating PRs.
51. Complete code review triage before creating/updating PR.
52. Include the work item identifier in PR title when available.
53. Add GitHub Copilot reviewer on all PRs.
54. If target branch/release path is unclear, ask user before PR creation.
55. After PR creation, transition the related work item to the appropriate review, testing, or pending state.

Engineering Quality Instructions

56. Always follow .github/skills/copilot/software-engineering/engineering-skill.md for code changes.
57. Perform package vulnerability checks for touched ecosystems and report findings.
58. Recommend package upgrades and wait for explicit user confirmation before changing versions.
59. Add/update unit tests where practical.
60. For non-testable wiring/config models, use minimal [ExcludeFromCodeCoverage] with concise justification.
61. Follow repository coding standards and OOP/SOLID principles.

Code Review Instructions

62. Always follow .github/skills/copilot/software-engineering/code-review-skill.md before finalizing commit/PR.
63. Collect and triage local and automated review comments.
64. Address or explicitly ignore with concise auditable rationale.
65. Never ignore security or regression-risk findings without explicit user confirmation.

PR Comment Resolution Instructions

66. Always follow .github/skills/copilot/software-engineering/pr-comment-resolver.md when resolving review comments.
67. Classify unresolved comments, validate each finding, and choose fix/respond path.
68. Provide a triage summary before committing fixes or posting responses.
69. Keep non-fix responses auditable and standards-based.

Feature Delivery Instructions

70. Always follow .github/skills/copilot/software-engineering/feature-developer-skill.md for end-to-end feature or requirement delivery.
71. Read AC and QA steps before implementation.
72. Validate against AC/QA and summarize evidence before commit approval request.
73. Keep explicit owner approval gates for commit and PR actions.

Scope

Apply these instructions for software engineering tasks, code changes, commits, PRs, and review-resolution activities.