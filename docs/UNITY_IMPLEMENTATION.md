# Unity Implementation Guide

## Target
Unity 2022 LTS or newer.

## Project Setup
Create the Unity project in this repository root if one does not exist yet.

Recommended Unity template:
- 3D Core for the first prototype.
- Switch to URP later only if night lighting needs more polish.

Recommended packages:
- Input System
- Cinemachine
- TextMeshPro
- Unity Test Framework

Optional later:
- URP for stronger night lighting.
- NavMeshComponents for thief movement.

## Editor Workflow

Use Unity Editor for scene and asset work. Use a Codex-enabled code editor for scripts, docs, tests, commits, and reviews.

Recommended setup:
- Unity Editor: scenes, prefabs, inspector values, Play Mode checks.
- VS Code or Cursor: C# scripts, docs, Codex IDE extension.
- Rider: optional later if C# navigation and refactoring become heavy.
- Codex App: larger feature work, multi-file edits, documentation sync, review, git operations.

Set the external script editor:
1. Open Unity.
2. Go to `Unity > Settings` or `Unity > Preferences`.
3. Open `External Tools`.
4. Set `External Script Editor` to VS Code, Cursor, or Rider.
5. Double-click a C# script from Unity to confirm the editor opens.

There is no official Unity Editor Codex panel assumed for this project. Codex should operate through the Codex App, CLI, or IDE extension while Unity remains the runtime/editor verification tool.

## First Local Setup Checklist

After creating/opening the Unity project:
- Confirm Unity version in this file.
- Open Package Manager and install or confirm recommended packages.
- Confirm `Assets/Scripts/Core` compiles.
- Open Unity Test Runner and run edit-mode tests.
- Create or confirm the first scene listed in `docs/exec-plans/vertical-slice-001.md`.
- Commit Unity-generated project settings that should be shared.
- Do not commit `Library/`, `Temp/`, `Obj/`, `Logs/`, or local user settings.

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
- First slice can be generated from `Airport Survival > Build Vertical Slice 001 Scene`.
- Generated slice includes placeholder player movement, trash cleanup, manager scolding, HUD, and day summary.

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

First slice scripts:
- `SimplePlayerController`
- `PlayerInteractor`

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

First slice script:
- `TrashItem`

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

## Vertical Slice 001 Manual Check
After Unity project creation:
1. Run edit-mode tests.
2. Build the generated scene from `Airport Survival > Build Vertical Slice 001 Scene`.
3. Press Play in `DayScene`.
4. Move with WASD or arrow keys.
5. Clean trash with `E`.
6. Let at least one trash item expire and confirm manager dialogue includes the scold count.
7. Click `End Day` and confirm the day summary appears.
