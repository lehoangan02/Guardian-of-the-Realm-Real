# Session handoff log

Append-only. **Newest entry at the top.** Every human or agent work session ends with an entry so the next person — on any agent (Claude Code, Codex, Antigravity) — can continue.

## Template
```markdown
### YYYY-MM-DD — <person> (<agent or "manual">) — <branch>
**Did:** what changed (files/systems)
**Verified:** editor / simulator / device / not verified (why)
**Open issues:** bugs, doubts, TODOs
**Next:** the concrete next step
```

---

### 2026-10-09 — Le Hoang An (Claude Code) — docs/desktop-input-workflow
**Did:** Answered how to develop without the headset (lab-only). Decision: build levels as a diorama under `BoardRoot` (no separate 3D-then-port step); add `IGameInput` with `HandGestureService` (device) and `DesktopDebugInput` (Editor only). Corrected `docs/setup/unity-mr-quest3.md`: Meta XR Simulator on macOS is limited (no hands/passthrough/rooms), hands are preset poses even on Windows. Added "Developing without a headset" section and an Input row in `docs/tech/architecture.md`.
**Verified:** Simulator limits from the meta-vr `hz-xr-simulator-install-and-configure` skill (v2.0.0); not checked in a running Unity. Docs only.
**Open issues:** `IGameInput` / `DesktopDebugInput` not implemented yet. Need a lab-visit checklist for device-only tests.
**Next:** Scaffold `IGameInput`, `DesktopDebugInput` and a `Sandbox_<You>` scene with `BoardRoot`.

### 2026-10-07 — Le Hoang An (Claude Code) — docs/setup
**Did:** Unity project created via Unity Hub (Universal 3D template, Unity 6000.3.16f1, pushed as "Initial check-in"). Removed template leftovers (`Assets/TutorialInfo/`, `Assets/Readme.asset`). Replaced `.gitattributes` with the standard Unity template (+ `glb`/`gltf`/`flac` → LFS). Re-added `.gitignore` lines for OS files, personal agent settings and APK/AAB builds. Ran `git lfs install` in the repo and set the global UnityYAMLMerge driver on Le Hoang An's Mac. Installed Meta agent skills + metavr MCP for Claude Code, Codex, Gemini CLI and Antigravity on that Mac. Committed the docs.
**Verified:** Ignore rules checked with `git check-ignore`; LFS `pre-push` hook present; merge driver path exists. Unity not reopened after deleting template assets.
**Open issues:** Teammates must run `git lfs install` in their clone + set their own merge driver + run `metavr init`. Claude Code can't list metavr MCP tools (see docs/setup/agent-tooling.md).
**Next:** Unity setup steps 2–4: switch to Android, OpenXR, Meta XR SDK, Building Blocks → "hello MR" on Quest 3.

### 2026-10-07 — Le Hoang An (Claude Code) — main
**Did:** Created the documentation system: `AGENTS.md` (single source of truth) + `CLAUDE.md`/`GEMINI.md` pointers, `docs/` tree (competition brief, GDD, interaction spec, architecture, conventions, git workflow, setup guides for Unity MR/MCP/agent tooling, art & audio guides, credits, roadmap, ADRs), `.gitattributes` (LFS + UnityYAMLMerge), `.gitignore` additions.
**Verified:** Docs links checked. No Unity project yet.
**Open issues:** Pin Unity & Meta SDK versions; confirm Devpost judging criteria & submission format; scene owners and audio owner are proposals.
**Next:** Follow [../setup/unity-mr-quest3.md](../setup/unity-mr-quest3.md) to create the Unity project and reach the "hello MR" milestone.
