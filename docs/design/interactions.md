# Hand interaction spec

> Hands only. No controllers, ever. Built on **Meta Interaction SDK** (hand grab, poke, pose/shape recognition). Before implementing, load the relevant Meta agent skill (`hz-unity-meta-core-sdk`, Interaction SDK skill) — APIs change between SDK versions.

## Design rules
1. **Big targets, forgiving detection.** Tracking jitters; enemies are tiny. Use generous colliders and assist (snap to the nearest valid target).
2. **Every gesture has a clear start, hold and release state** with distinct audio + visual feedback for each.
3. **No accidental triggers.** Gestures only activate while the hand is *over the board volume* (except wrist menu). Add short hysteresis (enter/exit thresholds, ~100–150 ms dwell).
4. **Ergonomic:** all interactions within ~60 cm of a seated player; no sustained raised arms (> 5 s) required.
5. **Recover gracefully** when tracking is lost: cancel the current gesture, never fire it.
6. **One-handed playable**; two hands only for board transform (optional alternative: handles).

## Gestures
| # | Action | Detection | Start / hold / release feedback | Notes |
|---|---|---|---|---|
| G1 | **Place board** | Two-hand pinch on board edges → move/rotate (yaw only)/scale; one-hand grab on a handle as fallback | Board outline glow; snap-to-surface click; "thunk" on confirm | Keep board level (lock pitch/roll). Snap to MRUK `TABLE` plane height when near |
| G2 | **Poke button / build spot** | Interaction SDK Poke | Hover highlight → press "click" → radial menu pop | Radial menu items ≥ 4 cm |
| G3 | **Grab hero** | HandGrab (pinch or palm) on hero collider (enlarged) | Hero lifts + dangles, "hey!" voice bark; road highlights valid drops | Invalid drop → hero walks back to the road |
| G4 | **Rain cloud** | Hand shape: open, palm facing down (palm normal · down > 0.8), height 10–35 cm above board | Shadow decal under palm → after 0.3 s cloud forms, rain loop | Cloud follows palm (smoothed). Cooldown when released or after max duration |
| G5 | **Thunder** | While G4 active, hand transitions to fist | Thunder crack + flash + camera-safe bolt to nearest enemy under the cloud | Consumes cloud charge |
| G6 | **Meteor** | Pinch above a height threshold (≥ 40 cm over board) → pull down/throw; release point + velocity determine landing | Rumble while held, fireball trail, impact boom | Show landing reticle while held |
| G7 | **Punch / flick** | Fist (or index finger) velocity > threshold intersecting an enemy collider | Whoosh + hit "bonk", enemy knockback | Short cooldown; bosses resist |
| G8 | **Wrist menu** | Palm facing the user's face (palm-up, near head view) for 0.5 s | Menu blooms from wrist; poke items | Opening the menu pauses the game |

## System behaviors
- **Auto-pause** when: app loses focus, headset removed, both hands lost > 1 s during a wave, wrist menu open.
- **Resume** with a 3-2-1 countdown.
- **Handedness:** all gestures work with either hand. Optional left-handed setting for wrist menu side.
- **Hand visuals:** use the SDK's hand mesh with a subtle magical tint; highlight fingertips when a gesture is "armed".

## Tuning log
Record thresholds here as they are tuned on device (value, date, who).
| Param | Value | Date | Who |
|---|---|---|---|
| Palm-down dot threshold | 0.8 | — | — |
| Punch velocity | ? m/s | — | — |
