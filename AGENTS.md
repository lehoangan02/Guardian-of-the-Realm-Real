# AGENTS.md — Guardian of the Realm

This is the **single source of truth for AI coding agents** (Claude Code, Codex, Antigravity/Gemini) working in this repo. `CLAUDE.md` and `GEMINI.md` only point here. Humans: start at [README.md](README.md) and [docs/README.md](docs/README.md).

## Project in one paragraph
Guardian of the Realm is a **mixed-reality tower-defense game** (Kingdom Rush-style) for **Meta Quest 3** (and future Meta VR Glasses), built in **Unity 6 LTS** with the **Meta XR SDK** (OpenXR). A miniature battlefield is placed on a real table (or the floor) via passthrough. The game is played **with bare hands only — no controllers, ever**: players pick up and drop heroes, build towers, and cast abilities with gestures (cloud/rain, thunder, meteor, punch). It is our entry for the **Meta VR Start Developer Competition 2026 (Gaming track), deadline Nov 18 2026 12:00 PM PST**.

## Hard rules (do not break)
1. **Hands-only.** Every feature must be fully usable with hand tracking. Never require a controller, never add a controller-only path. See [docs/design/interactions.md](docs/design/interactions.md).
2. **Seated, short sessions.** Design for a seated player at a table; a satisfying session in ≤10 minutes; fast cold start; clean pause/resume.
3. **Performance budget:** stable 72 fps on Quest 3 (never below 60). No per-frame allocations in gameplay loops, no `Find*`/`GetComponent` in `Update`.
4. **Do not edit scenes you don't own.** Scene ownership is listed in [docs/tech/git-workflow.md](docs/tech/git-workflow.md). Prefer changing prefabs, ScriptableObjects and scripts.
5. **Never hand-edit `.meta` files, `Library/`, `ProjectSettings/` GUIDs, or `Packages/packages-lock.json`.** Let Unity generate them. Always commit an asset together with its `.meta`.
6. **Licenses:** only CC0 / royalty-free-commercial / CC-BY assets. Every third-party asset (art, audio, fonts) must be logged in [docs/assets/credits.md](docs/assets/credits.md) in the same commit.
7. **Don't commit or push unless the human asks.** Work on a feature branch, never directly on `main`.
8. **Don't guess Meta/Unity APIs.** Use the Meta agent skills (`hz-*`) and official docs; SDK APIs change between versions. State the SDK version you checked against.

## Where things are
| Need | Read |
|---|---|
| Docs map | [docs/README.md](docs/README.md) |
| Competition rules, dates, submission checklist | [docs/competition/brief.md](docs/competition/brief.md) |
| Game design (GDD) | [docs/design/gdd.md](docs/design/gdd.md) |
| Gesture / hand interaction spec | [docs/design/interactions.md](docs/design/interactions.md) |
| Code architecture | [docs/tech/architecture.md](docs/tech/architecture.md) |
| C# & Unity conventions | [docs/tech/conventions.md](docs/tech/conventions.md) |
| Git, LFS, branches, scene ownership | [docs/tech/git-workflow.md](docs/tech/git-workflow.md) |
| Unity MR project setup (Quest 3) | [docs/setup/unity-mr-quest3.md](docs/setup/unity-mr-quest3.md) |
| MCP servers (Unity, Meta device) | [docs/setup/mcp.md](docs/setup/mcp.md) |
| Agent tooling & Meta agent skills | [docs/setup/agent-tooling.md](docs/setup/agent-tooling.md) |
| Art assets (Kenney, KayKit) | [docs/assets/art-assets.md](docs/assets/art-assets.md) |
| Audio direction & sourcing | [docs/assets/audio-guide.md](docs/assets/audio-guide.md) |
| Roadmap / milestones | [docs/process/roadmap.md](docs/process/roadmap.md) |
| Session handoff log | [docs/process/handoff.md](docs/process/handoff.md) |
| Decisions (ADRs) | [docs/decisions/](docs/decisions/) |

## Repo layout
```
/                      Unity project root (Assets/, Packages/, ProjectSettings/)
Assets/_Project/       ALL our own content (scripts, prefabs, scenes, SOs, audio, materials)
Assets/ThirdParty/     imported packs (Kenney, KayKit, audio packs) — do not modify in place
docs/                  human + agent documentation (Markdown only)
```
Details in [docs/tech/conventions.md](docs/tech/conventions.md).

## How to work (every session)
1. Read this file, then [docs/process/handoff.md](docs/process/handoff.md) (latest entries) and the relevant doc for your task.
2. Check `git status` and the current branch. Create a branch `feat/<short-name>` / `fix/<short-name>` if on `main`.
3. Make small, focused changes. Follow [docs/tech/conventions.md](docs/tech/conventions.md).
4. **Verify** (see below). If you cannot verify (e.g. needs the headset), say so explicitly.
5. **Before ending:** append an entry to [docs/process/handoff.md](docs/process/handoff.md) and update any doc your change made stale. If you made an architectural/tooling decision, add an ADR in `docs/decisions/`.

## Verification
- **Compile:** Unity console has no errors (via Unity MCP `read_console`, or ask the human).
- **Tests:** run EditMode/PlayMode tests via Unity MCP or Unity Test Runner when they exist for the touched code.
- **Editor run:** Meta XR Simulator or Quest Link (Windows) play mode for interaction changes.
- **Device:** gesture, passthrough, anchor and performance changes must be checked on Quest 3 by a human before merging.

## Tooling available to agents
- **Unity MCP (CoplayDev "MCP for Unity")** — scene/GameObject/asset/script/console/test tools against the running Unity Editor of the person you're working with. Setup: [docs/setup/mcp.md](docs/setup/mcp.md).
- **Meta VR CLI (`metavr`) + MCP** — device install, logs, screenshots, perf traces.
- **Meta agent skills (`hz-*`)** — authoritative procedures for Meta XR Core SDK, Interaction SDK, MRUK, perf debugging, store submission. Prefer them over memory.

## Team
| Person | Agent | Notes |
|---|---|---|
| Le Hoang An | Claude Code | |
| Nguyen Duc Thinh | Claude Code | |
| Tran Duc An | Codex | Antigravity is the team's backup agent |

Agent-specific notes: [CLAUDE.md](CLAUDE.md) (Claude Code), [GEMINI.md](GEMINI.md) (Antigravity/Gemini). Codex reads this file directly.
