# Agent tooling — Claude Code, Codex, Antigravity

> Decision record: [../decisions/0002-agents-md-source-of-truth.md](../decisions/0002-agents-md-source-of-truth.md)

## How instructions reach each agent
| Agent | Reads | Who uses it |
|---|---|---|
| **Claude Code** | `CLAUDE.md` → imports `AGENTS.md` via `@AGENTS.md` | Le Hoang An, Nguyen Duc Thinh |
| **Codex** | `AGENTS.md` natively | Tran Duc An |
| **Antigravity** | `AGENTS.md` (and `GEMINI.md` → points to AGENTS.md) | Backup for anyone |

So: **edit `AGENTS.md` for rules, `docs/` for knowledge.** Never put shared rules only in `CLAUDE.md` — Codex won't see them.

Personal, non-shared preferences: `CLAUDE.local.md` (git-ignored) or your user-level `~/.claude/CLAUDE.md`, `~/.codex/AGENTS.md`.

## Install Meta agent skills (each person, once per machine)
Meta publishes agent skills (open Agent Skills format) for Quest/Horizon development at [meta-quest/agentic-tools](https://github.com/meta-quest/agentic-tools).

Easiest — auto-detect everything:
```bash
npx metavr@latest init
```
Or per agent:
```text
# Claude Code (inside a session)
/plugin marketplace add meta-quest/agentic-tools
/plugin install meta-vr@meta-vr

# Codex (terminal)
codex plugin marketplace add meta-quest/agentic-tools
codex plugin add meta-vr

# Antigravity / Gemini (terminal)
gemini extensions install https://github.com/meta-quest/agentic-tools
```

### What `metavr init` actually does (verified 2026-10-07, metavr 1.8.0, macOS)
Run non-interactively: `npx -y metavr@latest init -y --no-auth --claude-code --codex --gemini`
- **Claude Code:** installs the `meta-vr` plugin (all `hz-*` skills + a `metavr` MCP server).
- **Codex:** adds `[mcp_servers.metavr]` to `~/.codex/config.toml` (backup at `config.toml.bak`) + skills.
- **Gemini CLI:** adds `metavr` to `~/.gemini/settings.json` and copies skills to `~/.gemini/skills/`.
- **Antigravity** has no `init` flag, but it reads the shared `~/.gemini/skills/`, so `--gemini` gives it the skills too. Its MCP servers must be added by hand to `~/.gemini/antigravity-ide/mcp_config.json` (IDE) and `~/.gemini/config/mcp_config.json`:
  ```json
  "metavr": { "command": "npx", "args": ["-y", "metavr@latest", "mcp", "server"], "disabled": false }
  ```
- Gemini CLI shows MCP servers as *Disabled* until you trust the project folder (it asks on first interactive run).
- ⚠️ **Known issue (Claude Code 2.1.292 + metavr 1.8.0):** `claude mcp list` shows the metavr MCP as connected but `tools fetch failed — Invalid result for tools/list` (`ttlMs` / `cacheScope`). The **skills still work**: they drive the `metavr` CLI through Bash. Re-check after updating either tool.

### Skills that matter for us
| Skill | Use it when |
|---|---|
| `hz-new-project-creation` | Setting up / validating the Unity project |
| `hz-unity-meta-core-sdk` | OVRManager / camera rig, passthrough, hand tracking, spatial anchors |
| Interaction SDK / Scene (MRUK) skills | Hand grab, poke, pose detection, table detection |
| XR Simulator skill | Testing on Mac / without headset |
| `hz-perfetto-debug`, `hz-simpleperf-debug` | Frame drops on device |
| `hz-store-submit` | Preparing the build for submission/release channel |
| "Verify Quest answers" skill | Double-checking an agent's claim against official docs |
Tell your agent explicitly: *"Use the hz-unity-meta-core-sdk skill to …"* — it's more reliable than hoping it auto-triggers.

## Working across agents (handoff protocol)
Because the three of us use different agents, continuity lives in the repo, not in chat history:
1. **Start of session:** agent reads `AGENTS.md` + latest entries of [../process/handoff.md](../process/handoff.md).
2. **During:** decisions → ADR; design changes → GDD; tuning → [../design/interactions.md](../design/interactions.md) tuning log.
3. **End of session:** append a handoff entry (template in the file). Ask your agent: *"Write the handoff entry for this session."*

## Useful prompts
- "Read AGENTS.md and the last 3 handoff entries, then tell me what's in progress."
- "Implement X following docs/tech/architecture.md; use Unity MCP to create the prefab; read the console when done."
- "Review this branch against AGENTS.md hard rules."

## Model/usage tips
- Claude users: two accounts → split work by system (e.g. one on MR/board/gestures, one on level/towers/units) to avoid conflicts.
- Codex user: great for self-contained tasks (wave system, economy, tests, editor tools).
- If an agent runs out of quota mid-task, the handoff entry lets anyone continue in Antigravity.
