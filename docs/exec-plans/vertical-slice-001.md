# Execution Plan: Vertical Slice 001

## Goal
Create the first playable slice of Airport Survival in Unity: one daytime shift with cleaning, manager scolding, stamina loss, and point gain.

## Scope
This slice does not need restaurant, check-in, information desk, thieves, ghosts, save/load, or final takeover.

## Steps
1. Create Unity project files in the repository.
2. Implement `JobCategory`, `JobProgress`, `PlayerProgress`, and `ReprimandSystem` in `Assets/Scripts/Core`.
3. Implement `DayResolutionService` for point gain and inactivity decay.
4. Add edit-mode tests for the core rules.
5. Create `DayScene`.
6. Add a controllable player placeholder.
7. Add trash prefab with pickup interaction and neglect timer.
8. Add manager prefab with scolding dialogue.
9. Add HUD for stamina, scold count, money, and job points.
10. Add day-end summary overlay.

## Acceptance Criteria
- Player can start a day.
- Trash appears and can be cleaned.
- Ignored trash triggers manager scolding.
- Scolding increments count and reduces stamina.
- Cleaning grants cleaning points.
- Day can end and show a summary.
- Core rules are covered by edit-mode tests.

## Risks
- Unity scene state can become hard to inspect if rules are hidden in prefabs.
- Core rule tests should be implemented before adding more job minigames.
