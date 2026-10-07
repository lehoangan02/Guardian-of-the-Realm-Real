# 0002 — AGENTS.md is the single source of truth for agent instructions
- Status: accepted
- Date: 2026-10-07
- Deciders: team

## Context
The team uses three agents: Claude Code (reads `CLAUDE.md`), Codex (reads `AGENTS.md`), Antigravity (reads `AGENTS.md` / `GEMINI.md`). Duplicated instructions drift apart; rules written only in `CLAUDE.md` are invisible to Codex.

## Decision
- `AGENTS.md` holds all shared rules and links into `docs/`.
- `CLAUDE.md` imports it with `@AGENTS.md` and adds only Claude-specific notes; `GEMINI.md` only points to it.
- Knowledge lives in `docs/` (plain Markdown), shared by humans and agents.
- Continuity across agents/people via the append-only `docs/process/handoff.md` and ADRs in `docs/decisions/`.

## Consequences
- One place to edit rules; every agent sees the same rules.
- Agent files must stay short; detail goes in `docs/`.
- Personal preferences stay out of the repo (`CLAUDE.local.md`, user-level config).
