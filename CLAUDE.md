# CLAUDE.md

All project instructions live in AGENTS.md (shared with Codex and Antigravity). Do not duplicate them here.

@AGENTS.md

## Claude Code–specific notes
- **Meta agent skills:** install the `meta-vr` plugin once per machine (see [docs/setup/agent-tooling.md](docs/setup/agent-tooling.md)). Invoke the relevant `hz-*` skill before writing Meta XR / Interaction SDK / MRUK code.
- **MCP:** Unity MCP and the Meta VR CLI MCP are configured per user (`claude mcp add ...`), see [docs/setup/mcp.md](docs/setup/mcp.md). Run `/mcp` to check they are connected. If Unity MCP is not connected, ask the human to open the Unity Editor instead of guessing scene state.
- **Personal preferences** go in `CLAUDE.local.md` (git-ignored), not here.
- If you learn something every agent should know, put it in AGENTS.md or `docs/`, not in this file.
