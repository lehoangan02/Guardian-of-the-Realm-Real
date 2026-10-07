# Game Design Document — Guardian of the Realm

> Living document. Status: **v0.1 draft (2026-10-07)**. Change it freely, but log big changes in [../process/handoff.md](../process/handoff.md).

## 1. Pitch
*You are the giant guardian of a tiny kingdom on your table.* Waves of goblins and skeletons march along a road toward the castle. Build towers, pick up and drop heroes where they're needed, and use your own hands as the forces of nature: become a storm cloud, call down thunder, hurl meteors, or just punch a troll off the map.

**Pillars**
1. **Your hands are the power.** Every verb is physical and satisfying; no menus to dig through, no controllers.
2. **A living diorama.** The board feels like a real miniature on your real table — readable from a seated position, beautiful up close.
3. **Easy in, easy out.** Playable in 10 minutes, understood in 1.

**References:** Kingdom Rush (structure, heroes, tower types, readability), Townsmen VR / Demeo (table-top MR feel), Moss (hands interacting with a miniature).

## 2. Platform & constraints
- Meta Quest 3, mixed reality (passthrough), **hand tracking only**, seated.
- Board ~ **80 × 50 cm** default at table scale, adjustable 0.5×–1.5×. Floor mode: same board scaled ~3×.
- 72 fps target. Low-poly CC0 assets (Kenney, KayKit). See [../competition/brief.md](../competition/brief.md).

## 3. Session flow
1. **Cold start** → passthrough on immediately; if a saved board anchor exists, the board appears there. Otherwise →
2. **Board placement**: suggest the detected table (MRUK); player can grab/move/rotate/scale the board with two-hand pinch; confirm with a 👍 poke button → anchor saved.
3. **Level select**: two floating "storybook" banners above the board (Level 1, Level 2). Poke to start.
4. **Build phase** (short) → **wave** → **build** → … → **boss wave** → **victory/defeat**.
5. Wrist menu (palm-up) available any time: pause, restart, re-place board, volume, quit.

## 4. Core loop
Enemies spawn at the board edge and walk a fixed path to the castle gate. Each enemy that reaches the gate removes lives (20 lives per level). Killing enemies earns **gold** → build/upgrade towers. Abilities run on **cooldowns** (not gold) so the player always has something physical to do.

## 5. Player verbs (summary — full spec in [interactions.md](interactions.md))
| Verb | Gesture | Purpose |
|---|---|---|
| Build / upgrade tower | Poke a build spot → radial menu → poke option | Economy & strategy |
| Move hero | Grab hero (pinch/palm grab), carry, release on road | Tactical positioning |
| Rain cloud | Open hand **palm-down** hovering over board, hold | Slow + damage over time in an area |
| Thunder | While cloud is active, **clench fist** | Single big strike, stun |
| Meteor | **Pinch high above board, pull down / throw** | AoE burst, long cooldown |
| Punch / flick | Fast fist or finger flick into enemies | Emergency "save", short cooldown, small knockback |

## 6. Content (competition scope)
### Towers (3 types, 2 upgrade levels each = 6 states)
| Tower | Role | Kenney source |
|---|---|---|
| Archer tower | Fast single target, hits air | Tower Defense Kit / Castle Kit |
| Mage tower | Slow, high magic damage (ignores armor) | Tower Defense Kit |
| Barracks | Spawns 3 soldiers who block the road | Castle Kit / Fantasy Town Kit + KayKit |
| *(stretch)* Cannon | Splash damage | Tower Defense Kit |

### Heroes (KayKit Adventurers — pick 2)
| Hero | KayKit model | Passive | Special (auto, cooldown) |
|---|---|---|---|
| Knight | Knight | Blocks up to 3 enemies | Shield bash (stun) |
| Mage *or* Rogue | Mage / Rogue | Ranged attacks | Fireball / dash |
Heroes respawn after 15 s at the castle if killed. Max 2 heroes on the board.

### Enemies (4 + boss)
| Enemy | Trait |
|---|---|
| Goblin / small skeleton | Fast, weak |
| Skeleton warrior | Armored (weak to mage) |
| Orc brute | Slow, tanky |
| Bat / flying | Ignores blockers, only archers/abilities hit |
| **Boss** (scaled-up orc/skeleton) | Final wave, punch-resistant, must be zapped |
> Enemy models: KayKit Skeletons pack (same author, CC0) is a natural match; check before week 3.

### Levels
| Level | Setting | Length | Teaches |
|---|---|---|---|
| 1 — Forest Road | Kenney Mini Forest + Fantasy Town | 6 waves, ~6–8 min | Placement, building, heroes, rain/thunder (tutorial) |
| 2 — Castle Siege | Castle Kit, two paths merging | 8 waves + boss, ~8–10 min | Meteor, flying enemies, path choice |

## 7. First five minutes (prize target)
- 0:00 passthrough + friendly narrator line ("Guardian, the realm needs you!") while the board "unfolds" on the table.
- Wave 1 is only 3 goblins; a glowing hand ghost shows *poke the build spot*.
- Wave 2 introduces grabbing the Knight.
- Wave 3 the sky darkens → ghost hand shows palm-down cloud, then fist → thunder.
- No text walls: ghost hands + voice/SFX + 1-line captions.

## 8. Feedback & juice (no haptics → audio/visual carry everything)
- Every hand action has: a sound, a particle, and a visible hand highlight.
- Enemies react: hit flash, knockback, comedic death "poof".
- Cloud: real-time shadow under the hand; rain particles; ambience changes.
- See [../assets/audio-guide.md](../assets/audio-guide.md).

## 9. Scope cuts (in priority order if we fall behind)
1. Cannon tower 2. Level 2's second path 3. Second hero 4. Upgrade level 2 5. Floor mode 6. Meteor.
**Never cut:** board placement, hands-only flow, rain + thunder, tutorial, audio, pause/resume.

## 10. Open questions
- Board scale default — test with real tables in week 1.
- Punch: physics knockback vs. scripted? (Prototype both quickly.)
- Score/stars per level for replayability?
