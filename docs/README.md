# Documentation map

Everything the team (humans **and** AI agents) needs to know lives in this folder as plain Markdown. Agents enter through [../AGENTS.md](../AGENTS.md); humans can start here.

## Principles
- **One fact, one place.** Link instead of copying. If two docs disagree, fix one and link it from the other.
- **Agent files are thin.** `AGENTS.md` holds rules + links; `CLAUDE.md` / `GEMINI.md` only point to it. Real knowledge goes in `docs/`.
- **Docs change with code.** A PR that makes a doc stale updates it in the same PR.
- **Decisions are recorded.** Anything that's hard to reverse or that someone might later ask "why?" about → an ADR in [decisions/](decisions/).
- **Sessions hand off.** Whoever (person or agent) ends a work session appends to [process/handoff.md](process/handoff.md).

## Map
| Folder | What's in it | Owner of truth for |
|---|---|---|
| [competition/](competition/) | [brief.md](competition/brief.md) | Rules, dates, submission checklist |
| [design/](design/) | [gdd.md](design/gdd.md), [interactions.md](design/interactions.md) | What the game is and how it feels |
| [tech/](tech/) | [architecture.md](tech/architecture.md), [conventions.md](tech/conventions.md), [git-workflow.md](tech/git-workflow.md) | How the code is built |
| [setup/](setup/) | [unity-mr-quest3.md](setup/unity-mr-quest3.md), [mcp.md](setup/mcp.md), [agent-tooling.md](setup/agent-tooling.md) | Getting a machine ready |
| [assets/](assets/) | [art-assets.md](assets/art-assets.md), [audio-guide.md](assets/audio-guide.md), [credits.md](assets/credits.md) | Third-party content & licenses |
| [process/](process/) | [roadmap.md](process/roadmap.md), [handoff.md](process/handoff.md) | Plan & status |
| [decisions/](decisions/) | ADRs `NNNN-title.md` | Why we chose X |

## Writing docs that work for agents
- Use explicit headings, tables and checklists; agents scan structure.
- Prefer exact names (`HeroDragController`, `Assets/_Project/Prefabs/Heroes/`) over descriptions.
- Mark unverified info: `> ⚠️ Unverified: ...` and the date checked. SDK details drift — always note the SDK/Unity version.
- Keep each file focused; split when it passes ~400 lines.

## ADR template
```markdown
# NNNN — Title
- Status: proposed | accepted | superseded by NNNN
- Date: YYYY-MM-DD
- Deciders: names

## Context
## Decision
## Consequences
```
