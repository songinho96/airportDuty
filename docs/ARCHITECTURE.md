# Architecture

## Principle
The game should be easy for Codex and humans to inspect. Core rules must be testable without loading Unity scenes.

## Folder Layout

```text
Assets/
  Scripts/
    Core/
    Gameplay/
    UI/
    Systems/
  Scenes/
  Prefabs/
  ScriptableObjects/
  Tests/
```

## Core
Plain C# classes. No `MonoBehaviour`, no scene references, no Unity object lifecycle.

Owns:
- Day/night state transitions.
- Job categories and levels.
- Point gain and decay.
- Scold count and stamina penalties.
- Score calculation.
- Win condition.

Suggested classes:
- `GameClock`
- `PlayerProgress`
- `JobProgress`
- `ReprimandSystem`
- `DayResolutionService`
- `ScoreCalculator`

## Gameplay
Unity scene behavior and interaction code.

Owns:
- Player movement.
- Trash pickup interactions.
- Restaurant task objects.
- Check-in passenger interactions.
- Information desk guidance interactions.
- Thief and ghost behaviors.
- Manager appearance triggers.

## UI
Unity UI and HUD presentation.

Owns:
- Stamina display.
- Money display.
- Job point and level display.
- Scold counter.
- Day/night transition panels.
- Job selection screen.

## Systems
Cross-cutting Unity services.

Owns:
- Scene loading.
- Save/load.
- Audio.
- Input binding.
- Camera mode.
- Difficulty configuration.

## Data
Use ScriptableObjects for tunable values:
- Job point rewards.
- Decay thresholds.
- Stamina penalties.
- Trash spawn rates.
- Passenger mistake rates.
- Thief and ghost spawn rates.

## Dependency Direction
`Core` must not depend on `Gameplay`, `UI`, or `Systems`.

Allowed direction:

```text
Core <- Gameplay <- UI
Core <- Systems
```

Scene scripts may call core logic. Core logic must not know about scenes, prefabs, cameras, or UI.
