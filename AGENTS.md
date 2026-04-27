# Airport Survival Agent Guide

This repository is for a Unity game prototype called "Airport Survival".

## Read First
- `docs/GAME_DESIGN.md` for the game fantasy, core loop, characters, jobs, and progression.
- `docs/ARCHITECTURE.md` for Unity code boundaries and folder ownership.
- `docs/UNITY_IMPLEMENTATION.md` for scene, prefab, ScriptableObject, and testing conventions.
- `docs/CODEX_HARNESS.md` for how to use Codex well in this repository.
- `docs/TASKS.md` for the current build plan.

## Engineering Rules
- Keep core rules in plain C# classes under `Assets/Scripts/Core`.
- Keep `MonoBehaviour` scene glue under `Assets/Scripts/Gameplay`, `Assets/Scripts/UI`, or `Assets/Scripts/Systems`.
- Do not hide important game rules inside inspector-only setup. Mirror those rules in docs or ScriptableObjects.
- Add edit-mode tests for scoring, reprimand, job decay, and day/night state transitions.
- Prefer small vertical slices over broad unfinished systems.
- Use one Codex thread per coherent task. Avoid running two live threads that edit the same files unless they are in separate git worktrees.
- For complex or fuzzy feature ideas, plan first and write or update an execution plan under `docs/exec-plans/`.
- When the same Codex mistake happens twice, write the lesson into the relevant doc or this file.

## Documentation Sync
When gameplay, architecture, Unity setup, scenes, prefabs, tests, or plans change, update the matching docs in the same change.

- Gameplay rules, characters, jobs, scoring, progression: `docs/GAME_DESIGN.md`
- Code boundaries, folders, dependencies, ownership: `docs/ARCHITECTURE.md`
- Unity scenes, prefabs, ScriptableObjects, packages, testing setup: `docs/UNITY_IMPLEMENTATION.md`
- Current milestone, backlog, next steps: `docs/TASKS.md`
- Multi-step feature work and implementation decisions: `docs/exec-plans/*.md`

Before finishing any substantial change, check `docs/DOC_SYNC_CHECKLIST.md`. If no documentation update is needed, mention that in the final response.

## Game Direction
- The game should feel like a tense, funny airport work survival sim.
- Daytime is task management: cleaning, restaurant work, check-in, and guidance.
- Nighttime is security horror: catch thieves, avoid ghosts.
- The manager is the main pressure source. Getting scolded costs stamina and increases the scold count.
- Skilled play means earning points while being scolded as little as possible.

## Verification
When Unity project files exist, run relevant Unity edit-mode tests after core logic changes. For UI or scene changes, open the affected scene and verify the main flow manually.

Before finishing:
- State which checks were run.
- State which docs were updated, or why docs did not need updates.
- For risky changes, review the diff against `docs/CODE_REVIEW.md`.
