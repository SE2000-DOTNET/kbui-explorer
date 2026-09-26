# Homelessness Platform Architecture

## Overview

The platform supports HUD-funded homelessness programs, housing navigation, case management, reporting, and HMIS integration.

---

## Architectural Style

Domain Driven Design (DDD)

Architecture Pattern:

- Presentation Layer
- Application Layer
- Domain Layer
- Infrastructure Layer

---

## Bounded Contexts

### Client Management

Responsibilities

- Clients
- Households
- Demographics

Services

- Client Service
- Household Service

---

### Assessment Management

Responsibilities

- CE Assessments
- Vulnerability Scores
- Prioritization

Services

- Assessment Service

---

### Program Management

Responsibilities

- ESG
- RRH
- PSH

Services

- Enrollment Service
- Eligibility Service

---

### Housing Management

Responsibilities

- Housing Units
- Landlords
- Placements

Services

- Housing Service
- Landlord Service

---

### Case Management

Responsibilities

- Service Plans
- Case Notes
- Goals

Services

- Case Management Service

---

### Compliance & Reporting

Responsibilities

- HUD Reporting
- Data Quality
- System Performance Measures

Services

- Reporting Service
- Compliance Service

---

## Core Microservices

### Client Service

APIs

- Create Client
- Update Client
- Search Client

Database Tables

- Clients
- Households

---

### Assessment Service

APIs

- Create Assessment
- Score Assessment
- Retrieve Assessment

Database Tables

- Assessments
- AssessmentResults

---

### Enrollment Service

APIs

- Start Enrollment
- Exit Enrollment
- Transfer Enrollment

Database Tables

- ProgramEnrollments

---

### Housing Service

APIs

- Search Units
- Reserve Unit
- Record Placement

Database Tables

- HousingUnits
- Placements
- Landlords

---

### Reporting Service

APIs

- Generate PIT
- Generate HIC
- Generate SPM
- Export HUD Reports

---

## Security Model

Authentication

- Azure AD
- External Identity Provider

Authorization

RBAC

Roles:

- Intake Specialist
- Case Manager
- Housing Navigator
- Program Manager
- HMIS Administrator
- System Administrator

---

## Audit Requirements

All actions generate audit records.

Examples

- Client Created
- Client Updated
- Assessment Completed
- Enrollment Modified
- Housing Placement Created

---

## Event Bus

Domain Events

- ClientCreated
- AssessmentCompleted
- ReferralCreated
- EnrollmentStarted
- EnrollmentExited
- HousingPlaced
- ReportGenerated

Messaging Technologies

- Azure Service Bus
- RabbitMQ
- Kafka

---

## Integrations

### HMIS

Functions

- Client Synchronization
- Enrollment Synchronization
- HUD Reporting

### Document Storage

- Azure Blob Storage
- SharePoint

### Notifications

- Email
- SMS
- Teams

---

## Recommended Technology Stack

Frontend

- React
- Next.js
- TypeScript

Backend

- .NET 9
- ASP.NET Core

Database

- SQL Server
- PostgreSQL

Cloud

- Azure

Reporting

- Power BI

Identity

- Microsoft Entra ID