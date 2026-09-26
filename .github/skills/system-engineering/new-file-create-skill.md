# New File Create Skill

**SOURCE:** `new-file-create-skill.md` - `C:\Users\jr59274\developer\POS-AiAgentSkills\.github\skills\copilot\system-engineering`

Use this skill whenever creating a brand new script or file that must be sent to stores.

## Purpose
* **711.** Ensure net-new files are included in the correct RemoteLink send task.
* **712.** Ensure files transferred to BOS are also copied to the correct endpoint (POS, CFT, FUEL) during maintenance.
* **713.** Keep new-file rollout aligned with code review and release process.

## Authoritative Reference
* **714.** Confluence: Introducing New Files
  * https://atlassian.net

## Overview
* **715.** New store-bound files must be added to an appropriate RemoteLink Send task so server-to-store sync includes the new file.
* **716.** Terminal transfer scripts must also be updated so files are copied from BOS to final endpoint locations.

## Procedure (Confluence-Aligned)
* **717.** Log in to RemoteLink using standard, non-elevated credentials.
* **718.** Log in to the caseys.local domain.
* **719.** In File Explorer, navigate to the destination path for the new file on the server.
* **720.** Identify an existing file in that location that RemoteLink already sends.
* **721.** Query RemoteLink for tasks sending that existing file and identify the Send task.
* **722.** In RemoteLink, open Configure -> Tasks and select the identified Send task.
* **723.** Open Edit Script.
* **724.** Locate the existing Send File command for the example file.
* **725.** Copy and paste that Send File command to create a new line.
* **726.** Edit the duplicated command:
  * **Source Path/Mask:** full server path for the new file.
  * **Target Path/Mask:** destination path for the store/client.
* **727.** Save task changes.

## Terminal Transfer Update (Required)
* **728.** Create a copy of the appropriate transfer file in your working folder:
  * `CFTXfer.ps1` in Utility for CFT.
  * `CGSFuelXfer.ps1` or `CGSPOSTXfer.cmd` in Office/Utils for POS or FUEL.
* **729.** Add a new file-copy line with correct source and destination paths for the net-new file.
* **730.** Include the transfer-script change in the same code review and release cycle as the new file.

## Stage Gates and Owner Instructions
* **731.** Stop after RemoteLink task update and request owner confirmation of Source/Target mappings.
* **732.** Stop after transfer-script update and request owner confirmation of endpoint routing (POS/CFT/FUEL).
* **733.** Stop before release and request owner confirmation that review and release requirements are satisfied.

## Required Checks
* **734.** RemoteLink Send task includes the new file with correct Source and Target masks.
* **735.** Terminal transfer script includes a valid copy step for the new file.
* **736.** Paths are validated for server, BOS, and endpoint target.
* **737.** Change is included in code review and release process.

## Completion Criteria
* **738.** New file is synced server-to-store via RemoteLink.
* **739.** New file is transferred BOS-to-endpoint during maintenance.
* **740.** Owner confirms readiness for release flow continuation.
