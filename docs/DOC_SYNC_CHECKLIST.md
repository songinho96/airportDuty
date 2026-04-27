# Documentation Sync Checklist

Use this before finishing any substantial project change.

## Gameplay
- Did the core loop change?
- Did a job category change?
- Did cleaning, restaurant, check-in, guidance, or security rules change?
- Did manager behavior, scolding, stamina, money, points, decay, or win conditions change?
- Did character roles, tone, or player goals change?

If yes, update `docs/GAME_DESIGN.md`.

## Architecture
- Did script folders or ownership change?
- Did core logic move between plain C# and `MonoBehaviour` code?
- Did dependency direction change?
- Did a new system, service, or manager class become important?

If yes, update `docs/ARCHITECTURE.md`.

## Unity Setup
- Did scenes change?
- Did prefab responsibilities change?
- Did ScriptableObject data or balance config change?
- Did Unity packages, test setup, input, camera, lighting, or build settings change?

If yes, update `docs/UNITY_IMPLEMENTATION.md`.

## Planning
- Did the active milestone change?
- Did a task become done, blocked, removed, or newly important?
- Did a feature require multiple steps or design decisions?

If yes, update `docs/TASKS.md` or the relevant file in `docs/exec-plans/`.

## Final Response
Mention one of these:
- Which docs were updated.
- Or why no docs needed updates.
