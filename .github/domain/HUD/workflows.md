# Homelessness Domain Workflows

## 1. Client Intake Workflow

### Goal

Register a new individual or household seeking services.

### Steps

1. Create Client record
2. Verify identity
3. Create Household
4. Complete Initial Assessment
5. Determine homelessness status
6. Determine program eligibility
7. Save intake record
8. Generate audit event

### Actors

- Intake Specialist
- Case Manager

### Outputs

- Client
- Household
- Assessment

### Events

- ClientCreated
- HouseholdCreated
- AssessmentStarted
- AssessmentCompleted

---

## 2. Coordinated Entry Workflow

### Goal

Match clients to appropriate housing resources.

### Steps

1. Complete VI-SPDAT assessment
2. Calculate vulnerability score
3. Add client to prioritization list
4. Review available resources
5. Create referral
6. Track referral status
7. Accept or reject referral

### Outputs

- Prioritization Record
- Referral

### Events

- AssessmentScored
- ReferralCreated
- ReferralAccepted
- ReferralRejected

---

## 3. Rapid Re-Housing Workflow

### Goal

Move clients into permanent housing quickly.

### Steps

1. Verify RRH eligibility
2. Assign Housing Navigator
3. Search available units
4. Engage landlord
5. Approve rental assistance
6. Execute lease
7. Complete move-in
8. Begin stabilization services

### Events

- EligibilityVerified
- UnitMatched
- LeaseSigned
- HousingPlaced

---

## 4. Permanent Supportive Housing Workflow

### Goal

Place chronically homeless individuals into PSH.

### Steps

1. Verify chronic homelessness
2. Verify disability documentation
3. Prioritize household
4. Match available PSH unit
5. Complete enrollment
6. Move client into housing
7. Initiate ongoing services

### Events

- ChronicStatusVerified
- PSHEnrollmentCreated
- HousingPlaced

---

## 5. Case Management Workflow

### Goal

Track ongoing support services.

### Steps

1. Open client record
2. Review goals
3. Document meeting
4. Update service plan
5. Add referrals
6. Schedule follow-up

### Events

- CaseNoteAdded
- GoalUpdated
- ReferralCreated

---

## 6. Program Exit Workflow

### Goal

Close participation in a housing program.

### Steps

1. Validate exit eligibility
2. Complete exit assessment
3. Capture destination
4. Calculate outcomes
5. Close enrollment

### Events

- ExitAssessmentCompleted
- EnrollmentExited

---

## 7. HMIS Reporting Workflow

### Goal

Generate HUD-compliant reports.

### Steps

1. Validate data quality
2. Identify missing data
3. Calculate system measures
4. Generate report package
5. Submit report

### Events

- DataValidationStarted
- DataValidationCompleted
- ReportGenerated
- ReportSubmitted