# Hand interaction spec

> Hands only. No controllers, ever. This specification follows the crossbow arrow, hammer thunder, and open-hand meteor design agreed 2026-10-10. Implementation boundaries, package inventory, and delivery gates live in [hand-interaction-plan.md](hand-interaction-plan.md). Planned against Unity `6000.3.16f1` and Meta XR SDK `207.0.0`; verify exact SDK APIs before coding.

## Design rules

1. **Big targets and forgiving aim.** Hand tracking jitters. Use generous hit regions and visible snap previews.
2. **Clear action states.** Show available, armed, preview, committed, and cancelled states through visual and audio feedback.
3. **No accidental casts.** Ability selection gates ability gestures. An active grab or menu suppresses ability recognition. Add short dwell and hysteresis, then tune on Quest 3.
4. **Seated comfort.** Frequent actions stay within about 60 cm. No long raised-arm hold. Never require hitting the physical table.
5. **Recover from tracking loss.** Cancel active interaction; never fire it. Preserve prior hero position when a drag fails.
6. **One-hand path.** All gameplay remains playable with one hand. Crossbow's default uses two hands, with a one-hand draw control as fallback.
7. **Either hand.** No hard-coded dominant hand. Both hands can use hero, UI, thunder, meteor, and the one-hand arrow control.

## Actions

| Action | Begin and aim | Commit | Cancel and feedback |
|---|---|---|---|
| **Board placement** | Grab board handle; optional two-hand transform | Poke confirmation | Outline and surface preview; release without confirmation or loss leaves prior placement. Board implementation belongs to MR team. |
| **Build or barracks** | Poke build spot, tower, or barracks; hand ray plus pinch for distant targets | Poke action option | Hover highlight and available actions; invalid or unavailable action reports reason. Gameplay owns costs and effects. |
| **Hero chess move** | Hand grab hero from bench or road; road snap preview follows hand | Release over valid road point | Invalid release or hand loss returns hero visual to prior location. Gameplay owns road validity and hero behavior. |
| **Crossbow arrow** | Select crossbow; one hand grips virtual bow and other draws string. Draw distance sets normalized power. Aim marker shows predicted landing point. One-hand draw control available. | Release draw hand once | Releasing grip, menu, or hand loss cancels. Grip/draw sounds and marker communicate power. |
| **Hammer thunder** | Select thunder; make fist over board. Marker shows strike zone. | Downward strike through virtual trigger plane above table | Wrong direction, no valid target, menu, or hand loss cancels. Exactly one enemy may be selected by gameplay in zone. |
| **Open-hand meteor** | Select meteor; open hand above board. Marker follows projected point below hand. | Stable open-hand dwell while over valid board area; meteor falls from hand | Close hand or leave area before dwell to cancel. Gameplay owns impact and lava. |
| **App menu** | Non-dominant palm-up index pinch, with accessible alternate hand | Poke or pinch menu action | Menu suspends gameplay gestures; close returns to prior state. Avoid accidental activation from ordinary palm-up pose. |

## Shared behavior

- A hand can own only one active action. Crossbow's two-hand mode owns both hands.
- Previews do not spend resources or alter game state. The game accepts or rejects each committed request exactly once.
- Auto-pause on app focus loss or headset removal. Opening app menu pauses. Tracking loss cancels actions; test whether additional auto-pause is needed rather than pausing every time both hands briefly leave view.
- Resume with a clear countdown. Hide ability previews while paused.
- Poke and grab use Meta Interaction SDK components. Custom ability recognizers use pose and timed movement states behind `HandGestureService`.
- User feedback carries action state, available target, cooldown unavailable state, and rejection reason. Use audio/visual cues because bare hands provide no haptics.

## Tuning log

Record measured values on Quest 3; numbers below are design starting points, not verified thresholds.

| Parameter | Starting point | Measured value / date / tester |
|---|---|---|
| Ability arming dwell | About 100–150 ms | Not tested |
| Meteor open-hand dwell | About 300 ms | Not tested |
| Crossbow draw dead zone, cap, and power curve | Set after prototype | Not tested |
| Thunder stroke distance and timing | Set after prototype; no physical table contact | Not tested |
| Target size and spacing | Build options at least 4 cm as visual design target | Not tested |

Meta references: [hand pose detection](https://developers.meta.com/vr/documentation/unity/unity-isdk-hand-pose-detection/), [tracking limits](https://developers.meta.com/vr/design/hands-limitations-mitigations/), [hand UI guidance](https://developers.meta.com/vr/design/hands-ui-best-practices/).
