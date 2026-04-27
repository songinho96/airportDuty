# Code Review Guide

Use this checklist when reviewing Codex changes in this repository.

## Findings First
Prioritize real bugs, regressions, missing tests, and risky patterns.

## Gameplay Correctness
- Do day/night rules still match `docs/GAME_DESIGN.md`?
- Does manager scolding always increment the count and reduce stamina?
- Does scolding dialogue include the current scold count?
- Do ignored jobs decay only through documented rules?
- Does the airport takeover condition still require every job at max level?

## Unity Boundaries
- Is core game logic still in plain C# under `Assets/Scripts/Core`?
- Are `MonoBehaviour` scripts limited to scene glue, input, UI, and object behavior?
- Are prefab and inspector assumptions documented?
- Are ScriptableObject configs used for tunable values instead of hard-coded scene constants?

## Tests And Verification
- Are edit-mode tests added or updated for core rule changes?
- Are play-mode or manual scene checks described for Unity behavior?
- Does the final response state which checks ran?
- If checks could not run, is the reason explicit?

## Maintainability
- Is the change small enough for the requested task?
- Does it follow existing naming and folder ownership?
- Did repeated friction become a doc rule, helper, or skill?
- Did docs stay synchronized?

## Review Output
When reporting issues, include:
- File path.
- Tight line reference when possible.
- Severity.
- Why the issue matters.
- What should change.
