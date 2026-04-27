---
name: airport-survival-feature
description: Use when implementing or planning an Airport Survival feature that touches gameplay design, Unity implementation, tests, and docs. Triggers include "add a feature", "change game rules", "make a level", "Unity scene", "manager", "day/night", "job", "trash", "thief", or "ghost".
---

# Airport Survival Feature Skill

Use this workflow for feature work in this repository.

## 1. Gather Context
Read:
- `AGENTS.md`
- `docs/GAME_DESIGN.md`
- `docs/ARCHITECTURE.md`
- `docs/UNITY_IMPLEMENTATION.md`
- `docs/CODEX_HARNESS.md`
- The relevant execution plan under `docs/exec-plans/`, if one exists.

## 2. Classify The Change
Decide whether the change is:
- Design-only
- Core rules
- Unity scene/prefab behavior
- UI/HUD
- Test/review/docs maintenance

## 3. Plan The Slice
For small work, use a short checklist.
For multi-system work, create or update an execution plan in `docs/exec-plans/`.

## 4. Implement By Boundary
- Core rules go in `Assets/Scripts/Core`.
- Scene object behavior goes in `Assets/Scripts/Gameplay`.
- HUD and menus go in `Assets/Scripts/UI`.
- Save/load, input, audio, scene loading, and global config go in `Assets/Scripts/Systems`.
- Tests go in `Assets/Tests`.

## 5. Sync Docs
Use `docs/DOC_SYNC_CHECKLIST.md`.
Update docs in the same change when behavior, architecture, Unity setup, or task priority changes.

## 6. Verify
Run the smallest useful checks available.
When Unity project files exist, run relevant edit-mode tests for core rule changes.
For scene/prefab work, verify the scene flow and console state manually or with available tooling.

## 7. Final Response
Include:
- What changed.
- Which checks ran.
- Which docs were updated, or why docs did not need updates.
- Any blocked verification.
