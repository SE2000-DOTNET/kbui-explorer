# Local Environment Testing Guardrails
**SOURCE:** `local-environment-testing-guardrails.md` - `C:\Users\jr59274\developer\POS-AiAgentSkills\.github\skills\copilot\system-engineering`

Quick index of local environment execution and testing boundaries and cross-environment rules.

Each section defines enforceable configuration restrictions, protected components, and validation rules.

## Purpose

- Establish clear testing boundaries to ensure environment integrity and prevent cross-environment dependencies.
- Enforce that local testing and execution never relies on changes to other environments to enable, facilitate, or complete testing.

## Core Rules

- Under no circumstances should cross-environment changes be offered, recommended, requested, or implemented to enable or facilitate troubleshooting, validation, or completing local environment testing or execution.
- Protected items include environment-specific assets: IaC, appsettings.json, local.settings.json, connection strings, environment variables, secrets, endpoints, platform settings, and all other environment configurations.
- Changes of this nature can only be made by the software engineer performing the testing, based on their own knowledge of the application and the environment.

## Categories

### Infrastructure

- **Infrastructure as Code (IaC)** - Terraform, Bicep, ARM Templates, CloudFormation, Pulumi.
- **Provisioning** - Resource definitions and provisioning scripts.
- **Configurations** - Environment deployment configurations.

### Application Configuration

- **App Settings** - `appsettings.json`, `appsettings.*.json`.
- **Local Settings** - `local.settings.json`, `host.json`.
- **Launch Settings** - `launchsettings.json`, `web.config`.
- **Startup Files** - Runtime and startup configuration files.

### Connectivity Configuration

- **Databases** - Connection strings and database configurations.
- **Endpoints** - Service, API, storage, and messaging endpoints.
- **Networking** - DNS configurations and external service references.

### Security Configuration

- **Secrets Management** - Environment variables and secret stores.
- **Credentials** - Key Vault references, API keys, certificates.
- **Identity** - Authentication and authorization settings.
- **Access Tokens** - Managed identities, access tokens, encryption configurations.

### Platform Configuration

- **App Services** - Azure Function and App Service settings.
- **Containers** - Container, Kubernetes, and Docker settings.
- **Observability** - Monitoring, logging, and Application Insights configuration.

## Maintenance Notes

1. If local testing or execution cannot be completed due to an environment configuration gap, surface the gap as a finding and recommend raising it as a separate work item.
2. If a defect is discovered during local testing, it must be resolved in application code, not worked around via environment changes.
3. All local testing must remain traceable to the assigned story, feature, defect, or task.
