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
| **Abilities** | `AbilityController`, `RainCloudAbility`, `ThunderAbility`, `MeteorAbility`, `PunchAbility` | Gesture → ability, cooldowns |
| **Input** | `IGameInput`, `HandGestureService` (device), `DesktopDebugInput` (Editor only, never shipped) | Gameplay depends only on `IGameInput` events (poke, grab/drop, ability gestures), so the full loop is playable with mouse/keyboard without a headset |
| **Hands** | `HandGestureService` (wraps Interaction SDK pose/shape detection) | One place translating hand data into gesture events; abilities never read raw joints |
| **Economy** | `GoldWallet`, `LivesCounter` | |
| **Audio** | `AudioService`, `SoundEvent` (SO with clip variants, volume, pitch range, mixer group) | All sounds go through `SoundEvent`s; music state machine |
| **UI** | `WristMenu`, `RadialBuildMenu`, `WorldTooltip` | Poke-based UI |

## Principles
- **Data in ScriptableObjects** (towers, enemies, waves, sounds) so designers and agents can tune without code changes.
- **Pooling** for enemies, projectiles, VFX, audio sources — no `Instantiate`/`Destroy` during waves.
- **Board-local space:** all gameplay positions are relative to `BoardRoot`; scale-independent math (use board scale factor for speeds/ranges).
- **Events over references:** systems communicate with C# events / a light event bus; avoid singletons except `Services` locator in Bootstrap.
- **Gesture layer isolation:** only `HandGestureService` touches Meta hand APIs → swap/tune detection in one place, test abilities in the editor with a fake gesture source.

## Tests
- EditMode tests for pure logic (wave scheduling, economy, targeting).
- PlayMode smoke test: load Bootstrap + Level_01 in the editor, run 1 wave with fake gestures.
