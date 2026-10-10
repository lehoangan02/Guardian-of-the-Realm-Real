# Hand interaction coding plan

> Planned 2026-10-10 for Unity `6000.3.16f1` and Meta XR SDK `207.0.0`. This is an implementation sequence, not a claim that any interaction is built. Player behavior is specified in [../design/interactions.md](../design/interactions.md); boundaries and package inventory are in [../design/hand-interaction-plan.md](../design/hand-interaction-plan.md). Read [hand-interaction-progress.md](hand-interaction-progress.md) before each run.

## Scope

Own hand input, gesture state, previews, hand feedback, and typed requests to gameplay. Do not implement or alter map paths, wave logic, enemy selection, damage, economy, hero combat, tower rules, or barracks rules. Do not edit shared scenes without human approval. Build interactions in scripts, prefabs, and a personal sandbox after Unity generates asset `.meta` files.

## Dependency direction

`Meta XR hand data` → `MetaHandTrackingAdapter` → `HandInteractionCoordinator` + action objects → `IGameInput` semantic requests → gameplay adapters. Gameplay supplies availability, board-space conversion, placement validity, and arrow landing prediction through small interfaces. The contracts layer must have no Meta SDK dependency. The Meta adapter must never call concrete enemy, map, tower, or hero classes.

Use the separate, Meta-free `GuardianRealm.Hands.Contracts` assembly as the stable boundary. Meta implementation assemblies reference it; future gameplay code implements or adapts its ports. Keep integration changes localized in adapters when contracts evolve.

## Run sequence

| Run | Work owned by interaction agent | Exit check and handoff |
|---|---|---|
| **0. Setup audit** | Read Git status, latest handoff, progress record, package manifest and lock, Unity console, active XR provider, and Meta Project Setup Tool. Confirm editor is idle. Use official Meta docs or installed `hz-*` skills for SDK `207.0.0` before using APIs. | Record exact resolved package versions, console state, required setup fixes, and whether Quest 3 is available. Stop package-dependent coding if Unity cannot compile. Do not hand-edit lockfile or project GUIDs. |
| **1. Contract foundation** | Define and freeze version 1 of `IGameInput`, request/response types, `BoardRoot` local coordinates, IDs, preview providers, and cancellation semantics. Keep it independent from Meta and gameplay assemblies. | Contracts compile without Meta references. Focused EditMode tests verify invariants, IDs, and acceptance results. **Complete.** |
| **2. Coordinator core** | Implement one owner per hand and explicit `Idle`, `Armed`, `Preview`, `Commit`, `Cancel` transitions. Add action priority, tracking-loss cancellation, pause/menu suspension, exactly-once commit guard. Feed deterministic fake hand frames. | EditMode tests cover competing gestures, both hands, duplicate release, lost tracking, rejection, and resume. No map or scene dependency. |
| **3. Meta adapter** | Connect Meta Interaction SDK hand state, confidence, grab, poke, and pose signals to coordinator. Cache component references. Keep SDK types inside adapter. | Unity compiles with no errors. Personal sandbox shows both hands and debug state. Record actual SDK APIs and package version used. Quest test required before declaring gesture detection reliable. |
| **4. Direct interactions** | Add hand poke for build/tower/barracks controls and hand grab for hero bench-to-road drag. Use fake placement/availability ports until concrete adapters exist. Add hand ray plus pinch for distant targets. | Valid preview, commit, invalid return, tracking-loss cancel, and either-hand operation work in sandbox. No hero, road, tower, or barracks gameplay state changes inside hand code. |
| **5. Crossbow** | Add virtual grip, second-hand draw, normalized power, gameplay-supplied landing marker, release commit, cancellation, and one-hand draw path. | Preview equals gameplay provider result; one request per release. On-device check for hand overlap, comfort, range, and both hand choices. |
| **6. Thunder** | Add selected fist pose, projected zone, deliberate downward stroke above table, single commit guard. | No physical table contact. No cast for idle fist, grab, empty target, or tracking loss. Gameplay chooses exactly one enemy. |
| **7. Meteor** | Add selected open-hand pose, impact marker, stable dwell, hand-origin fall feedback, cancel on close/exit/loss. | Open palm outside selected mode never casts. Gameplay handles area damage and lava. |
| **8. Integration** | Replace fake ports with thin adapters to the concrete gameplay systems available then. Keep dependency direction. Add feedback for accepted/rejected requests and cooldown availability. | Integration smoke test: hero move, build/barracks command, and all three abilities through full loop. No duplicate commits, no stale preview after pause or scene change. |
| **9. Device and performance** | Profile seated Quest 3 play under normal and weaker lighting. Tune thresholds and UI. Pool preview/VFX objects; remove per-frame allocations and repeated hierarchy searches. | Human validates gestures and passthrough on Quest 3. Record observed frame rate against 72 fps target and 60 fps floor, test conditions, failures, and next fix. |

Runs can be split into smaller sessions. Do not mark a run complete from code presence alone; satisfy its exit check. Keep untested device behavior marked **unverified**.

## Tests and build rhythm

- After each script change: wait for Unity compilation, read console, fix errors before proceeding.
- Run focused EditMode tests for pure coordinator and contract behavior. Run PlayMode smoke tests for adapter, previews, and scene lifecycle when those parts exist.
- Use a personal sandbox for interaction work. Ask the human before changing Bootstrap or level scenes.
- A build or device install follows the authorization rule in [../setup/mcp.md](../setup/mcp.md). When authorized, capture build result and device result separately. A successful APK build does not prove hand tracking or 72 fps.
- After **every build attempt**, including failed builds, update [hand-interaction-progress.md](hand-interaction-progress.md) before the next coding step. At session end, append [handoff.md](handoff.md). The next agent starts from those records, not from a guess or chat history.

## Definition of done

All three abilities, hero placement, and building/barracks controls work hands-only. One-hand arrow path works. Gameplay owns effects and rules through agreed interfaces. Unity console has no errors. Focused tests pass. A human validates gestures and seated comfort on Quest 3. Frame-time evidence supports stable 72 fps and no drops below 60 fps in tested scenarios. Docs, tuning log, progress record, and handoff reflect actual result.
