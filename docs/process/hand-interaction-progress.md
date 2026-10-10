# Hand interaction progress

> Current-state record. Update this file after every interaction coding run and **every build attempt**, successful or failed. Keep [handoff.md](handoff.md) append-only for history. Do not mark checks complete without evidence. Coding sequence: [hand-interaction-coding-plan.md](hand-interaction-coding-plan.md).

## Current snapshot — 2026-10-10

| Item | State | Evidence / next check |
|---|---|---|
| Branch | `docs/hand-interaction-review` | Working tree contains unrelated Unity setup changes; inspect before editing. No commit or push requested. |
| Unity / Meta | Unity `6000.3.16f1`; Meta XR All-in-One `207.0.0` | Verified from project files and the current Unity editor log. Live Unity MCP was unavailable for this audit. |
| Resolved XR packages | Meta OpenXR `2.6.1`; OpenXR `1.18.0`; XR Management `4.7.0`; XR Hands `1.7.3` | Verified in `Packages/packages-lock.json`. The manifest requests some lower direct versions; the lock records Unity's resolved graph. Do not hand-edit it. |
| OpenXR configuration | **Ready for coding** | Live Unity MCP confirms Android uses `UnityEngine.XR.OpenXR.OpenXRLoader`. Hand Interaction Profile, Meta Hand Tracking Aim, XR Hands tracking, Meta Quest Support, Meta XR, foveation, and subsampled layout are enabled. |
| Hands-only configuration | **Ready for coding** | Meta SDK 207 project config is `HandsOnly`. Hand frequency remains `LOW`; change only if Quest testing shows fast-motion tracking loss. The Oculus Touch profiles exist only because Meta's Required setup check needs them for full `OVRInput` compatibility; product interaction remains hands-only. |
| Mixed-reality configuration | **Ready for coding; device unverified** | Passthrough and Scene API are `Required`; Spatial Anchor support is enabled. Unity splash is disabled for a consistent MR startup. Validate permissions and behavior on Quest 3 later. |
| Meta Project Setup Tool | **Required = 0** | Live SDK 207 status has three Recommended items: Data Use Checkup, application ID/package name, and newest Meta XR Simulator. Two Optional items remain: Android manifest synchronization and Application SpaceWarp. SpaceWarp stays off per the MR performance plan. |
| Project interaction code | **Contracts complete; gestures not started** | `GuardianRealm.Hands.Contracts` contains typed IDs, requests, decisions, query/results, and narrow ports. No gesture recognizer, Meta adapter, prefab, map logic, or shared scene change yet. |
| Scenes / build content | Bootstrap not started | Both scenes contain only basic camera/light setup. Build Settings enables only `Assets/Scenes/SampleScene.unity`. Do not edit a shared scene without human approval. |
| Coding plan | Implemented through run 1 | Next work is run 2, the deterministic coordinator core. |
| Gameplay contracts | **Version 1 complete and self-owned** | Meta-free `IGameInput`, typed requests and IDs, board-local coordinates, preview ports, availability, build catalog, feedback, cancellation, and acceptance decisions compile independently. Future gameplay plugs in through adapters. |
| Latest diagnostics | **Live Unity console: 0 errors** | Unity imported and compiled the new contract and test assemblies successfully. |
| Quest 3 availability | Not available to this run | Neither `metavr` nor `adb` was available on PATH; no connected-device check or headset validation was possible. |
| Latest tests / build / Quest test | **7/7 focused EditMode tests passed; build not run; Quest unverified** | Unity job `d032235a45df4cebb3800ed4a48837fb` passed all contract tests. Quest 3 validation remains mandatory after gesture implementation. |

## Coding gates

| Run | State | Evidence / blocker |
|---|---|---|
| 0 Setup audit | **Complete** | Live Unity MCP verified Editor readiness, Android OpenXR loader, Hands Only, required MR capabilities, Meta Required = 0, and console errors = 0. Quest hardware remains unverified and is recorded above. |
| 1 Contract foundation | **Complete** | Meta-free assembly compiles; 7/7 focused EditMode tests pass. |
| 2 Coordinator core | Pending | Contracts ready |
| 3 Meta adapter | Pending | Depends on setup and coordinator |
| 4 Direct interactions | Pending | Depends on adapter and preview ports |
| 5 Crossbow | Pending | Landing prediction port needed |
| 6 Thunder | Pending | Strike zone and availability port needed |
| 7 Meteor | Pending | Board projection and availability port needed |
| 8 Integration | Pending | Concrete gameplay adapters will be added when systems exist |
| 9 Device and performance | Pending | Human Quest 3 validation needed |

## Latest build attempt

**Result:** No interaction build attempted yet.

After every build, replace this section with:

- Date/time and agent; branch, commit or dirty state; Unity and resolved Meta/OpenXR versions.
- Build target and method; artifact path or build job ID; success/failure and duration.
- First actionable compiler/build error, if any; exact file and line when available.
- Focused test results; Unity console errors/warnings after build.
- Quest 3 install/play result separately; headset, lighting, gesture result, and measured frame rate if tested.
- Files changed since prior build; current blocker; exact next run and first command or tool action.

If a build is still running, record **in progress** and its job ID. Do not report success until it finishes. Keep older build summaries in [handoff.md](handoff.md) or a dated section below when comparison is useful.

## Next run

Start run 2: coordinator core.

1. Recheck Git status and this record because the setup worktree is shared and dirty.
2. Implement one owner per hand with explicit `Idle`, `Armed`, `Preview`, `Commit`, and `Cancel` transitions.
3. Add priority, tracking-loss cancellation, pause suspension, rejection recovery, and an exactly-once commit guard.
4. Drive it with deterministic fake hand frames and test competing gestures, both hands, duplicate release, loss, rejection, and resume.

Do not modify shared scenes, map systems, or gameplay logic. Keep Quest behavior marked unverified until a human device test.
