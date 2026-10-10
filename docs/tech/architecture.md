# Architecture

> Proposed v0.1. Update as the code grows; this file should always describe what exists, with "planned" marked.

## Scenes
| Scene | Purpose |
|---|---|
| `Bootstrap` | Loads services, passthrough, camera rig, hand tracking; restores board anchor |
| `Level_01_ForestRoad` | Additive level content (board, path, spawns) |
| `Level_02_CastleSiege` | Additive level content |
| `Sandbox_<name>` | Personal test scenes, not shipped |

`Bootstrap` stays loaded; levels are loaded **additively** as children of the board root so the whole level moves with the anchored board.

## Core systems (namespaces under `GuardianRealm.*`)
| System | Key classes (planned) | Responsibility |
|---|---|---|
| **MR / Board** | `BoardPlacementController`, `BoardAnchorStore`, `SurfaceFinder` | Find table via MRUK, let player transform board, save/load spatial anchor |
| **Game flow** | `GameStateMachine` (Boot → Placement → LevelSelect → Build → Wave → Result), `PauseService` | High-level state, pause/resume, focus loss |
| **Level** | `LevelDefinition` (SO), `WaveDefinition` (SO), `WaveSpawner`, `PathNetwork` (spline waypoints) | Data-driven waves and paths |
| **Units** | `Enemy`, `EnemyDefinition` (SO), `Health`, `UnitMover`, `Blocker` | Enemy movement, combat, blocking |
| **Towers** | `BuildSpot`, `Tower`, `TowerDefinition` (SO), `Projectile` (pooled) | Building/upgrading, targeting |
| **Heroes** | `Hero`, `HeroDefinition` (SO), `HeroDragController` | Grab/drop on road, auto-combat, respawn |
| **Abilities** | `AbilityController`, `ArrowAbility`, `ThunderAbility`, `MeteorAbility` | Gesture → ability, cooldowns |
| **Input** | `GuardianRealm.Hands.Contracts`, `IGameInput`, typed requests, preview ports; later `HandGestureService` and `DesktopDebugInput` | The implemented Meta-free contracts expose semantic requests and board-local previews. Device and Editor adapters will feed the same coordinator. |
| **Hands** | `HandGestureService` (wraps Interaction SDK pose/shape detection) | Composes Meta hand adapter, interaction coordinator, and action objects; gameplay never reads raw joints |
| **Economy** | `GoldWallet`, `LivesCounter` | |
| **Audio** | `AudioService`, `SoundEvent` (SO with clip variants, volume, pitch range, mixer group) | All sounds go through `SoundEvent`s; music state machine |
| **UI** | `WristMenu`, `RadialBuildMenu`, `WorldTooltip` | Poke-based UI |

## Principles
- **Data in ScriptableObjects** (towers, enemies, waves, sounds) so designers and agents can tune without code changes.
- **Pooling** for enemies, projectiles, VFX, audio sources — no `Instantiate`/`Destroy` during waves.
- **Board-local space:** all gameplay positions are relative to `BoardRoot`; scale-independent math (use board scale factor for speeds/ranges).
- **Events over references:** systems communicate with C# events / a light event bus; avoid singletons except `Services` locator in Bootstrap.
- **Gesture layer isolation:** only the adapter inside `HandGestureService` touches Meta hand APIs. Gameplay receives typed action requests and supplies board-local preview/validity through interfaces. See [../design/hand-interaction-plan.md](../design/hand-interaction-plan.md) and [../decisions/0003-hand-interaction-boundary.md](../decisions/0003-hand-interaction-boundary.md).

## Tests
- EditMode tests for pure logic (wave scheduling, economy, targeting).
- PlayMode smoke test: load Bootstrap + Level_01 in the editor, run 1 wave with fake gestures.
