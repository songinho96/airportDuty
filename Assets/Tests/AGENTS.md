# Test Guidance

This directory owns Unity edit-mode and play-mode tests.

## Rules
- Prefer edit-mode tests for `Assets/Scripts/Core`.
- Use play-mode tests only when scene lifecycle, physics, input, or prefab behavior is required.
- Test game rules by observable behavior, not private implementation details.
- Keep test names descriptive: `Condition_ExpectedResult`.
- If a behavior change cannot be tested yet because Unity project files are missing, update docs or task notes with the missing test.
