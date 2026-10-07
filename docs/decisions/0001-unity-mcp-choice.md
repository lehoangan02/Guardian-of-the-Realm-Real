# 0001 — Use CoplayDev "MCP for Unity" as our Unity MCP server
- Status: accepted
- Date: 2026-10-07
- Deciders: team

## Context
We want AI agents (Claude Code ×2, Codex ×1, Antigravity as backup) to inspect and edit the Unity Editor. Options:
1. **Unity MCP (official)**, part of `com.unity.ai.assistant` 2.0 — requires Unity 6 and a Unity subscription / Unity AI access; documented clients: Claude Code, Cursor, Windsurf, Claude Desktop.
2. **CoplayDev `unity-mcp` ("MCP for Unity")** — MIT, free, Unity 2021.3–6.x, works with any MCP client (incl. Codex & Gemini/Antigravity), ~50 tools (scenes, GameObjects, scripts, assets, console, tests, builds), very active (v10.3.0, Oct 2026, ~14.7k stars).

We use Unity 6 LTS **Personal**, and one teammate uses Codex.

## Decision
Use **CoplayDev MCP for Unity**, pinned to a release tag in `Packages/manifest.json`. Client configs are per-user (not committed). Meta device access via the **Meta VR CLI MCP** (`metavr`).

## Consequences
- Same tooling for all three agents; no license cost.
- Third-party, not Unity-supported: if it breaks after a Unity/SDK update, pin to the last working tag.
- It's an editor-only package; it does not ship in the build.
- Revisit if the team gets a Unity subscription and the official server supports Codex.
