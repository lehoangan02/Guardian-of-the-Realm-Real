# MCP setup (Unity + Meta device)

> Decision record: [../decisions/0001-unity-mcp-choice.md](../decisions/0001-unity-mcp-choice.md). Checked 2026-10-07.

We use two MCP servers:
| Server | What it gives agents | Scope |
|---|---|---|
| **MCP for Unity** (CoplayDev `unity-mcp`) | ~50 tools: create/modify scenes & GameObjects, edit scripts, manage assets/prefabs/materials, read console, run tests, build | Talks to **your** open Unity Editor |
| **Meta VR CLI MCP** (`metavr`) | 40+ tools: device list, install/launch APK, logcat, screenshots, perf traces | Talks to **your** connected Quest |

Both are configured **per user** (not committed), because they point at local binaries/paths that differ across Mac and Windows.

## Why not Unity's official Unity MCP?
Unity's own MCP (`com.unity.ai.assistant` 2.0) needs Unity 6 **and a Unity subscription / Unity AI access**, and documents only Claude Code, Cursor, Windsurf and Claude Desktop. We're on Unity Personal with one Codex user, so CoplayDev's MIT-licensed, client-agnostic server is the better fit. Revisit only if that changes.

## 1. Install MCP for Unity (once per project — done by whoever creates the project)
Prerequisites on **every** machine: **Python 3.10+** and **uv**
- macOS: `brew install uv` (or `curl -LsSf https://astral.sh/uv/install.sh | sh`)
- Windows: `winget install --id=astral-sh.uv -e` (or `powershell -c "irm https://astral.sh/uv/install.ps1 | iex"`)

In Unity: **Window → Package Manager → + → Add package from git URL…**
```
https://github.com/CoplayDev/unity-mcp.git?path=/MCPForUnity#main
```
> Pin to a release tag instead of `#main` (e.g. `#v10.3.0`) so all three of us run the same version. The package entry lands in `Packages/manifest.json` and is committed.

## 2. Connect your agent (each person)
Open **Window → MCP for Unity**. Use **Configure** next to your client (or *Configure All Detected Clients*). It writes the correct config for your OS. Then restart your agent.

Manual / verification per client:
- **Claude Code** — run `claude mcp list` (should show the Unity server) and `/mcp` inside a session. If you configure manually, use `claude mcp add --scope user ...` with the command shown in the MCP for Unity window.
- **Codex** — config lives in `~/.codex/config.toml` under `[mcp_servers.<name>]` (or `codex mcp add <name> -- <command...>`). Check with `codex mcp list`.
- **Antigravity** — Agent panel → MCP servers → *Manage / View raw config* → `mcp_config.json`; paste the JSON shown in the MCP for Unity window.

> Recent MCP for Unity versions serve **HTTP at `http://127.0.0.1:8080/mcp`** while the Unity Editor is open (confirmed in an existing Antigravity config on Le Hoang An's Mac). Clients that support HTTP can point straight at that URL, e.g. Claude Code: `claude mcp add --scope user --transport http unityMCP http://127.0.0.1:8080/mcp`.

Test prompt: *"Using Unity MCP, read the Unity console and list the GameObjects in the open scene."*

## 3. Meta VR CLI MCP (each person)
```bash
npx metavr@latest init          # detects Claude Code / Codex / Gemini, installs skills + MCP
# or explicitly:
metavr mcp install claude-code
```
For Codex/Antigravity, if `init` didn't configure it, run `metavr mcp --help` to see the install targets. Test: *"List connected Meta devices and show the last 50 log lines of our app."*

## Rules for agents using MCP
- The Unity Editor must be open (and not in a modal dialog/compiling) for Unity MCP calls to work. If not connected: say so, don't guess.
- Respect scene ownership ([../tech/git-workflow.md](../tech/git-workflow.md)). Prefer prefab/SO/script edits.
- After script changes, wait for recompilation and **read the console** before claiming success.
- Never run builds or device installs without the human's go-ahead (they take minutes and need the headset on).

## Troubleshooting
- Server not starting → `uv --version` in a fresh terminal; on Windows restart after installing uv (PATH).
- Client connects but no tools → restart the Unity Editor, then the agent.
- Two Unity Editors open → close one; the bridge serves one editor at a time per port.
- See the [MCP for Unity docs](https://coplaydev.github.io/unity-mcp/) and its Discord.

## Sources
- [CoplayDev/unity-mcp](https://github.com/CoplayDev/unity-mcp)
- [Unity MCP overview (official)](https://docs.unity3d.com/Packages/com.unity.ai.assistant@2.0/manual/unity-mcp-overview.html) · [get started](https://docs.unity3d.com/Packages/com.unity.ai.assistant@2.0/manual/unity-mcp-get-started.html)
- [meta-quest/agentic-tools](https://github.com/meta-quest/agentic-tools)
