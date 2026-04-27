# Core Rules Guidance

This directory owns plain C# game rules.

## Rules
- Do not add `MonoBehaviour` classes here.
- Do not depend on Unity scenes, prefabs, cameras, UI, input, or inspector-only state.
- Keep rules deterministic and easy to cover with edit-mode tests.
- Add or update tests in `Assets/Tests` when changing behavior.
- Update `docs/GAME_DESIGN.md` when gameplay rules change.
- Update `docs/ARCHITECTURE.md` when ownership or dependencies change.

## Preferred Shape
- Use small domain classes with explicit inputs and return values.
- Keep tunable values injectable through constructors or config objects.
- Avoid hidden static state.
