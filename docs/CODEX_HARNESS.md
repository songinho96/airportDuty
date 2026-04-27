# Codex Harness Guide

This document captures how to use Codex effectively for Airport Survival. `AGENTS.md` should stay short and point here when details are needed.

## Sources
This guide is based on:
- OpenAI Codex best practices: https://developers.openai.com/codex/learn/best-practices
- OpenAI AGENTS.md guide: https://developers.openai.com/codex/guides/agents-md
- OpenAI Codex prompting guide: https://developers.openai.com/codex/prompting
- OpenAI Codex cloud environments: https://developers.openai.com/codex/cloud/environments
- OpenAI harness engineering article: https://openai.com/ko-KR/index/harness-engineering/

## Harness Philosophy
Codex works best when the repository gives it a clear environment, not just a big prompt.

For this game, that means:
- `AGENTS.md` is the map, not the encyclopedia.
- `docs/` is the durable source of project knowledge.
- Core rules are plain C# and testable without Unity scenes.
- Unity scenes, prefabs, and ScriptableObjects must be documented because Codex cannot reliably infer inspector-only intent.
- Every substantial feature should end with checks, docs sync, and a short diff review.

## Prompt Shape
Good prompts for this repo should include:

```text
Goal:
What should change?

Context:
Relevant files, docs, scene names, prefabs, errors, or screenshots.

Constraints:
Architecture, gameplay rules, Unity limits, and what not to change.

Done when:
Specific behavior, tests, docs, and manual verification that prove completion.
```

Example:

```text
Add the first trash-cleaning daytime slice.
Use docs/GAME_DESIGN.md and docs/exec-plans/vertical-slice-001.md.
Keep rules in Assets/Scripts/Core and scene glue in Assets/Scripts/Gameplay.
Done when trash can be picked up, ignored trash triggers one manager scolding, stamina drops, docs are updated, and relevant tests pass.
```

## Planning Rules
Use a short plan for simple changes. Use an execution plan when:
- The work spans multiple systems.
- A feature touches both core rules and Unity scene state.
- The request is fuzzy or likely to change.
- The implementation will take multiple sessions.

Execution plans live in `docs/exec-plans/`. They should include goal, scope, steps, acceptance criteria, risks, and current status.

## Documentation Rules
Keep docs close to reality:
- Gameplay changes update `docs/GAME_DESIGN.md`.
- Architecture changes update `docs/ARCHITECTURE.md`.
- Unity scene/prefab/setup changes update `docs/UNITY_IMPLEMENTATION.md`.
- Milestone changes update `docs/TASKS.md`.
- Big features update their execution plan.
- Use `docs/DOC_SYNC_CHECKLIST.md` before finishing substantial work.

If docs become too large, split them instead of turning `AGENTS.md` into a huge manual.

## Verification Loop
Codex should try to see its own work.

For core C# changes:
- Run edit-mode tests when Unity project files exist.
- If Unity is not available yet, state that tests could not be run and why.

For Unity scene/prefab changes:
- Open the scene manually or through available tooling.
- Verify the user flow, console errors, and visible HUD state.
- Add temporary debug logging only when it helps verify runtime behavior, then keep or remove it deliberately.

For design-only changes:
- Confirm which docs changed.
- Check that `AGENTS.md` still points to the right source of truth.

## Review Loop
Before finishing risky changes, review against `docs/CODE_REVIEW.md`.

Look especially for:
- Gameplay rules hidden in `MonoBehaviour` scripts.
- Inspector-only assumptions not documented.
- Scene or prefab changes without docs.
- Core logic changes without tests.
- New repeated patterns that should become helpers, docs, or a skill.

## Skills
Use repo skills when a workflow becomes repeatable. This repo includes:

- `.agents/skills/airport-survival-feature/SKILL.md`

Use that skill for feature work that touches design, implementation, tests, and docs together.

Create more skills only after the workflow proves useful manually. Good candidates later:
- Unity scene verification workflow.
- Doc gardening workflow.
- Level design validation workflow.
- Release note or build checklist workflow.

## Automations
Do not automate a workflow until it works manually.

Good future automations:
- Weekly doc drift scan.
- Recent-change summary.
- Test failure triage after CI exists.
- Backlog grooming from `docs/TASKS.md`.

## Git And Threads
- Use git checkpoints before large changes once this folder is a git repo.
- Use one Codex thread per coherent task.
- Avoid two live threads editing the same files.
- Use git worktrees for parallel feature work once the project is in git.

## Cloud Codex Notes
When this repo is pushed to GitHub and used with Codex cloud:
- Configure the environment so dependencies and tools are installed before tasks.
- Keep setup commands documented in this repo.
- Remember that setup scripts can use internet access, while agent internet access is off by default unless configured.
- Make sure `AGENTS.md` includes the project-specific build, test, and validation commands once Unity project files exist.
