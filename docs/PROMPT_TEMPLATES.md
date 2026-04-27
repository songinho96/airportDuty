# Prompt Templates

Use these when asking Codex to work on Airport Survival.

## Feature Implementation

```text
Goal:
Implement [feature].

Context:
Read AGENTS.md, docs/GAME_DESIGN.md, docs/ARCHITECTURE.md, and [specific plan/doc].

Constraints:
Keep core rules in Assets/Scripts/Core.
Keep Unity scene glue in Gameplay/UI/Systems.
Update docs when behavior or setup changes.

Done when:
[specific playable behavior]
[specific tests/checks]
[specific docs updated]
```

## Design Change

```text
Goal:
Change the design so [new rule or feature].

Context:
Start from docs/GAME_DESIGN.md and docs/TASKS.md.

Constraints:
Do not implement code yet unless the plan is clear.
Keep the scope to design, acceptance criteria, and follow-up tasks.

Done when:
The relevant docs are updated and implementation tasks are clear.
```

## Bug Fix

```text
Goal:
Fix [bug].

Context:
Bug happens when [steps].
Expected: [expected behavior].
Actual: [actual behavior].

Constraints:
Make the smallest high-confidence fix.
Add or update tests if the bug is in core rules.

Done when:
The bug no longer reproduces, tests/checks are run, and docs are updated if behavior changed.
```

## Review

```text
Review the current changes against docs/CODE_REVIEW.md.
Find real bugs, regressions, missing tests, or documentation drift.
Return findings first with file and line references.
```
