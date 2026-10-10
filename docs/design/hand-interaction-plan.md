# Hand interaction implementation plan

> Status: implementation plan, updated 2026-10-10. Contract run 1 is complete; gesture implementations have not started. This plan follows the three abilities specified by Tran Duc An: crossbow arrow, hammer thunder, and open-hand meteor. It supersedes the earlier rain-cloud, pinch-throw meteor, and separate punch gestures. See [interactions.md](interactions.md) for the player-facing gesture specification.

## Scope and ownership

The hand interaction module owns hand tracking integration, gesture recognition, interaction state, previews, hand feedback, and semantic requests. It does **not** own map geometry, paths, enemy selection, damage, gold, cooldown rules, tower rules, barracks rules, or hero combat. Small ports isolate those future systems; local fakes can drive the module until concrete systems exist. No shared scene edits are part of this plan.

All coordinates crossing the boundary use `BoardRoot` local space, metres before visual board scaling. Every request carries an interaction ID, hand identity, and timestamp so gameplay can reject duplicates. Preview data is temporary; only an accepted commit changes gameplay. A cancellation never changes gameplay.

## Package inventory and installation plan

Checked against local `Packages/manifest.json` and `Packages/packages-lock.json` on 2026-10-10, Unity `6000.3.16f1`, Meta XR SDK `207.0.0`. Package setup is currently uncommitted and may still be resolving; verify in Unity before coding. Current run status lives in [../process/hand-interaction-progress.md](../process/hand-interaction-progress.md).

| Package | Current state | Decision |
|---|---|---|
| `com.meta.xr.sdk.all` `207.0.0` | Installed; brings Core, Interaction SDK Essentials and OVR integration, MRUK, Simulator, plus other Meta packages | Keep for prototype. Do not install duplicate Meta interaction packages. Review a smaller explicit Meta package set only after the prototype works. |
| `com.unity.xr.management` | Manifest requests `4.5.4`; current lockfile resolves `4.7.0` | Keep. Android OpenXR provider is enabled. |
| `com.unity.xr.openxr` | Manifest requests `1.16.1`; current lockfile resolves `1.18.0` | Present in local setup changes. A dependency can resolve a newer version. Let Unity finish resolution; verify actual loaded version, Android provider, and compile status. If Unity reports a conflict, use Package Manager; never hand-edit the lockfile. |
| `com.unity.xr.hands` `1.7.3` | Installed transitively | Keep transitively while hand code uses Meta Interaction SDK. Add direct dependency only if team chooses Unity XR Hands APIs. |
| `com.unity.render-pipelines.universal` `17.3.0` | Installed | Keep. |
| `com.unity.inputsystem` `1.19.0` | Installed | Keep for Editor-only debug input; shipped controls stay hands-only. |
| `com.unity.test-framework` `1.6.0` | Installed | Keep for interaction-state tests. |
| `com.unity.xr.meta-openxr` `2.6.1` | In manifest and lockfile as an uncommitted setup change | Keep for the current OpenXR setup. Reassess only through Unity Package Manager after the interaction prototype works. |

No XR Interaction Toolkit, Oculus XR Plugin, third-party gesture recognizer, or extra physics package is needed. Building Blocks and Interaction SDK samples are features of installed Meta packages, not separate required packages. Package installation and project settings are setup work, outside this design-only session.

Sources checked 2026-10-10: [Meta XR plugin management](https://developers.meta.com/vr/documentation/unity/unity-xr-plugin/), [Meta Interaction SDK packages](https://developers.meta.com/vr/documentation/unity/unity-isdk-packages-and-requirements/), [Meta SDK bundle](https://developers.meta.com/vr/documentation/unity/unity-sdks-overview/), [Meta OpenXR compatibility](https://developers.meta.com/vr/documentation/unity/unity-and-openxr-compatibility/).

## Object-oriented boundary

Use four layers. Dependencies point downward; gameplay never imports Meta SDK types.

1. **`MetaHandTrackingAdapter`** reads Meta hand pose, confidence, and Interaction SDK hand grab/poke events. This is the only production layer that knows Meta APIs. One adapter instance per hand.
2. **`HandInteractionCoordinator`** owns per-hand action state and precedence. It feeds the ability recognizers and direct manipulation handlers. At most one committed action may own a hand. A two-hand crossbow owns both hands until release or cancellation.
3. **Action objects** implement a shared `IHandInteraction` lifecycle: `CanBegin`, `Begin`, `UpdatePreview`, `Commit`, `Cancel`. Planned objects: `CrossbowInteraction`, `ThunderInteraction`, `MeteorInteraction`, `HeroPlacementInteraction`, and `BuildInteraction`. Each owns gesture rules and feedback state, never gameplay effects.
4. **Gameplay ports** are small interfaces initially backed by deterministic local fakes and later by concrete gameplay adapters. `IBoardSpace` converts world and board-local positions. `IInteractionAvailability` says whether an action can start. `IHeroPlacementPreview` returns valid road positions. `IArrowTrajectoryPreview` predicts landing. `IGameInput` accepts typed action requests and returns accepted/rejected with a reason. `IInteractionFeedback` presents interaction feedback and cancellation.

`GuardianRealm.Hands.Contracts` now provides `IGameInput` as the public boundary. It exposes semantic actions, not joints, SDK components, or enemy classes. `HandGestureService` will compose the adapter and coordinator. `DesktopDebugInput` will drive the same coordinator in Editor. Future gameplay code plugs in through adapters. Avoid a global event bus for high-frequency previews; use direct provider calls with no per-frame allocation.

### Version 1 request contracts

| Request | Data from interaction layer | Future gameplay decision |
|---|---|---|
| `ArrowFireRequested` | Board-local origin, aim direction, normalized draw power `0..1`, predicted landing point, interaction ID | Projectile trajectory, range mapping, hit, damage, cooldown |
| `ThunderStrikeRequested` | Board-local strike center, interaction ID | Which **one** enemy in zone is hit, damage, stun, cooldown |
| `MeteorDropRequested` | Board-local landing point, hand origin, interaction ID | Impact, area damage, lava duration/effect, cooldown |
| `HeroMoveRequested` | Hero ID, snapped road position, interaction ID | Validity, hero movement, combat and occupancy |
| `BuildActionRequested` | Build spot ID, selected action ID, interaction ID | Cost, construction, upgrade, barracks command, availability |

Gameplay reports rejection without side effects. Interaction layer then shows reason and restores preview. No hand code reads enemy lists or changes hero/tower state directly. Gameplay publishes availability and valid targets; interaction team does not invent path or map coordinates.

## Player interaction flows

### Crossbow arrow

The player selects crossbow using a hand-only ability control. A virtual grip appears within comfortable reach. One hand grips it; the other pinches and draws a virtual string. Grip and draw hands stay separated in the headset tracking view. Draw distance maps to normalized power with a dead zone and cap; actual range curve comes from gameplay. Aim updates a landing marker supplied by `IArrowTrajectoryPreview`, so marker and shot agree. Releasing draw hand commits once. Releasing grip, losing either hand, leaving safe tracking volume, or opening menu cancels. A one-hand accessibility path offers a pinch-and-drag draw control with the same preview and commit request; this remains hands-only.

### Hammer thunder

The player selects thunder, then forms a fist over the board. A zone marker follows projected fist position. A deliberate downward strike crosses a virtual plane **above** the physical table; physical table contact is never required. Gesture needs pose, direction, travel, and short timing window, not raw speed alone. After one recognized strike, the coordinator locks the hand until it returns to idle. Gameplay picks exactly one enemy in the zone. No valid enemy means no cast request and no cooldown use; gameplay owns final acceptance. Tracking loss cancels.

### Open-hand meteor

The player selects meteor, then opens a hand over the board. An impact marker appears below that hand. After a short stable open-hand dwell, a meteor visually falls from the hand to the marker. Moving outside the board or clo`sing the hand before dwell cancels. Selection gates the open-hand pose so ordinary open palms never cast. Gameplay decides area damage and lava behavior. The interaction layer only controls aim, activation, and visual feedback.

### Hero and building interaction

A hero can be grabbed from bench or road with hand grab. During drag, `IHeroPlacementPreview` provides road snap candidates and validity. Release sends `HeroMoveRequest`; invalid release or tracking loss restores the prior visual placement. No hero combat or road logic lives here.

Build spots, towers, barracks, upgrade controls, and barracks commands use hand poke for near targets. Offer hand ray plus pinch for targets beyond comfortable reach. The gameplay team supplies each object's available action IDs and labels; interaction code displays and submits those actions. Provide generous hit regions and separation. Never require a controller or precise contact with a tiny model.

## Arbitration and failure rules

Priority: system/menu and pause; active hero/build grab; active crossbow; selected thunder or meteor; idle hover. An action that owns a hand suppresses competing recognizers. Ability selection is hand-only. Both hands can act independently except during crossbow. Tracking loss cancels active actions and hides previews; never fires an ability. A rejected request produces clear feedback and returns to idle. Store no gameplay cooldown or score state in hand classes.

## Delivery sequence and acceptance

1. **Setup gate:** install OpenXR provider, configure Hands Only, select OpenXR hand skeleton, clear required Meta setup fixes, make a tiny personal sandbox. Do not edit a shared scene without human approval.
2. **Contract gate:** define and version request/preview interfaces, IDs, board-local coordinate convention, availability responses, and cancellation semantics. Use fake gameplay ports first. **Complete for version 1.**
3. **Direct actions:** hand poke for building/barracks and hand grab for hero bench-to-road. Verify valid, invalid, and tracking-loss release.
4. **Ability spike:** crossbow first because two-hand occlusion and exact landing preview are highest risk. Test thunder without table contact, then selected open-hand meteor. Tune on Quest 3, not simulator alone.
5. **Integration gate:** connect gameplay ports; verify preview matches committed result, exactly-once requests, pause/cancel behavior, both hands, one-hand arrow path, far target fallback.
6. **Performance gate:** profile Quest 3 at 72 fps. Avoid per-frame allocations and repeated hierarchy searches; pool reticles/VFX and subscribe/unsubscribe events. Test seated play in varied lighting with several users. Record measured thresholds in [interactions.md](interactions.md).

This plan creates no map, combat, economy, tower, or wave implementation. Those teams can replace fake ports without changing gesture recognizers.

Implementation order, exit checks, and the required post-build progress update are in [../process/hand-interaction-coding-plan.md](../process/hand-interaction-coding-plan.md).
