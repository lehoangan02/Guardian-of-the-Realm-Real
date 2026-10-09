# Roadmap — Oct 7 → Nov 17 2026 (6 weeks)

Hard deadline: **Nov 18 12:00 PM PST** (Nov 19 03:00 Vietnam). Internal deadline: **Nov 17**. Each week ends with a **device build on Quest 3** that someone plays end-to-end.


## W1 · Oct 7–13 — Foundations
- [ ] Re-read Devpost rules & judging criteria → update [../competition/brief.md](../competition/brief.md)
- [ ] Everyone: tools installed, Meta skills + MCP working ([../setup/](../setup/))
- [ ] Unity project created, pinned versions recorded, "hello MR" (passthrough + hands + grab cube) on device
- [ ] Board placement prototype: MRUK table detection + manual move/rotate/scale + spatial anchor
- [ ] Audio direction meeting + reference list
- **Exit:** the board appears on a real table and survives an app restart.

## W2 · Oct 14–20 — Core TD loop (graybox)
- [ ] Path + enemy movement, wave spawner (SO-driven), lives & gold
- [ ] Build spot → poke → radial menu → archer tower that shoots
- [ ] Game state machine, pause/resume, wrist menu skeleton
- [ ] Placeholder SFX for every action
- **Exit:** a full graybox wave can be won/lost with hands only.

## W3 · Oct 21–27 — Heroes & gestures
- [ ] Hero grab/drop on road, auto-combat, respawn
- [ ] Mage + Barracks towers, upgrades
- [ ] `HandGestureService`: palm-down cloud + rain, fist → thunder
- [ ] Level 1 layout with real Kenney assets (rough)
- **Exit:** Level 1 playable start-to-finish with rain + thunder; first playtest with someone outside the team.

## W4 · Oct 28–Nov 3 — Content & first five minutes
- [ ] Meteor + punch (prototype; cut if they don't feel good)
- [ ] Tutorial / onboarding in Level 1's first waves
- [ ] Enemy roster (4 + boss), KayKit heroes animated
- [ ] Audio pass 1 (abilities, heroes, battle music loop)
- **Exit:** "feature complete" for Level 1. Feature freeze for new mechanics.

## W5 · Nov 4–10 — Level 2, perf, polish
- [ ] Level 2 (Castle Siege) + boss
- [ ] Performance: stable 72 fps on Quest 3 (profile!)
- [ ] Balance pass, cold-start time, anchor-failure fallback, tracking-loss handling
- [ ] Audio pass 2 + mix in headset; VFX juice
- **Exit:** release-candidate build; 3+ external playtests.

## W6 · Nov 11–17 — Ship
- [ ] Bug fixing only (no features)
- [ ] Capture demo video on device (<3 min, with game audio), key art, screenshots
- [ ] Devpost page text, credits, build upload
- [ ] **Submit by Nov 17**
