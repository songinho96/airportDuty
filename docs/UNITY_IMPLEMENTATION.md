# Unity Implementation Guide

## Target
Unity 2022 LTS or newer.

## Project Setup
Create the Unity project in this repository root if one does not exist yet.

Recommended packages:
- Input System
- Cinemachine
- TextMeshPro
- Unity Test Framework

Optional later:
- URP for stronger night lighting.
- NavMeshComponents for thief movement.

## Scenes

### `MainMenu`
- Start game button.
- Continue button after save/load exists.
- Settings button later.

### `DayScene`
- Airport daytime environment.
- Job selection UI at the start.
- Trash spawns across public areas.
- Main job stations are enabled based on selected job.

### `NightScene`
- Dark airport environment.
- Patrol route markers.
- Thief spawn points.
- Ghost spawn points.

### `ResultsScene` or Overlay
- Day summary.
- Points gained.
- Points decayed.
- Money earned.
- Scold count.
- Stamina remaining.

## Prefabs

### Player
Required components:
- Movement controller.
- Interaction detector.
- Stamina presenter hook.

### Manager
Required components:
- Appearance controller.
- Dialogue trigger.
- Reprimand event bridge.

### Trash
Required components:
- Pickup interaction.
- Lifetime timer.
- Neglect trigger.

### Passenger
Required components:
- Request data.
- Destination or check-in data.
- Suspicious/criminal flag for check-in variants.

### Thief
Required components:
- Patrol or escape behavior.
- Capture interaction.

### Ghost
Required components:
- Avoidance hazard.
- Contact penalty.
- Cannot be captured.

## ScriptableObjects

Suggested assets:
- `GameBalanceConfig`
- `JobConfig`
- `ReprimandConfig`
- `SpawnConfig`
- `DialogueConfig`

## Testing

Add edit-mode tests for:
- Job point gain.
- Inactivity decay.
- Scold count increase.
- Stamina loss from scolding.
- Win condition when all jobs are max level.
- Day job choice affecting practiced categories.

Add play-mode tests later for:
- Trash pickup.
- Manager spawn after neglected trash.
- Thief capture.
- Ghost contact penalty.
