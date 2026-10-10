# Audio guide — how to choose, where to find, how to implement

Audio is **more important than usual** in this game: with no controllers there are **no haptics**, so sound is the main way the player *feels* a grab, a crossbow release or a thunderstrike. Treat audio as a core feature, not polish.

## 1. Start with direction, not with browsing
Before downloading anything, agree (30 min, whole team) on:
- **Mood:** playful heroic fantasy — think Kingdom Rush / Clash Royale: light orchestral + folk (flutes, strings, brass stabs, war drums), cartoony-punchy SFX, not gritty-realistic.
- **Reference list:** 3–5 tracks/videos you all like (e.g. Kingdom Rush soundtrack, Clash Royale battle music, Moss, Townsmen VR). Put links below.
- **Rule of consistency:** pick **one main SFX family** (e.g. one big pack) and **one composer/pack for music**. Mixing ten sources sounds amateur; consistency beats individual quality.

References: _(add links)_

## 2. Sound event list (what we need)
Priority: **P0** = needed for the playable build, **P1** = for submission, **P2** = nice to have.

| Category | Events | Pri |
|---|---|---|
| Hands / UI | hover, poke click, menu open/close, confirm, error/invalid, wrist menu bloom | P0 |
| Board | board appear/"unfold", place/snap, anchor confirmed | P0 |
| Heroes | pick up (voice "Hey!"/"Whoa!"), put down/land, attack swings, special, death, respawn | P0 |
| Towers | build (construction), upgrade, archer shot, mage zap, barracks bell/footsteps | P0 |
| Enemies | footsteps loop (per type, quiet), hit, death "poof", reach gate (alarm), boss roar | P0 |
| Abilities | crossbow grip, string draw/release, arrow flight/impact, thunder crack, meteor whoosh/impact, lava loop, cooldown ready chime | **P0 (most important)** |
| Game flow | wave incoming horn, wave cleared, gold earned (coin), life lost, victory fanfare, defeat sting | P0 |
| Ambience | forest birds/wind (L1), castle wind/flags (L2), lava ambience during meteor area | P1 |
| Music | menu/placement theme, build-phase calm loop, wave/battle loop, boss loop, victory & defeat stingers | P0 (1 battle loop) → P1 (rest) |
| Voice | narrator lines for tutorial (can be text + SFX if no VO) | P2 |

## 3. Licensing — what we may use
| License | OK? | Notes |
|---|---|---|
| **CC0 / public domain** | ✅ Best | No credit required (credit anyway) |
| **Royalty-free with commercial license** (Sonniss, Asset Store, paid itch.io packs) | ✅ | Keep the license text/receipt |
| **CC-BY** | ✅ | **Must credit** author + link in-game/Devpost |
| CC-BY-SA | ⚠️ Avoid | Share-alike is messy for games |
| **Non-commercial (NC)** / No-derivatives (ND) | ❌ Never | We may win prize money / publish |
| "Free for personal use", unknown, YouTube rips, commercial game rips | ❌ Never | |
| AI-generated (ElevenLabs SFX, Suno/Udio music) | ⚠️ Only on a **paid plan granting commercial rights**, and only if Devpost rules allow AI content — check first | |
**Every file used → a row in [credits.md](credits.md) in the same commit.**

## 4. Where to find it
### Sound effects
| Source | License | Why |
|---|---|---|
| [Kenney audio packs](https://kenney.nl/assets/category:Audio) (Impact Sounds, RPG Audio, Interface Sounds, UI Audio, Casino Audio for coins) | CC0 | Matches our Kenney visual kit, instantly usable — **start here** |
| [Sonniss GameAudioGDC bundles](https://sonniss.com/gameaudiogdc) | Royalty-free, commercial, no credit | Tens of GB of pro SFX (thunder, rain, whooshes, impacts) — best for abilities |
| [Freesound](https://freesound.org) | Mixed — filter by **CC0** (or CC-BY) | Huge; great for specific things (thunder, crowd, creature voices) |
| [Pixabay Sound Effects](https://pixabay.com/sound-effects/) | Pixabay license (commercial OK) | Quick, decent quality |
| [OpenGameArt](https://opengameart.org) | Mixed — check each | Game-oriented packs |
| [ZapSplat](https://www.zapsplat.com) | Free w/ attribution, or paid | Large catalog |
| itch.io SFX packs (search "fantasy RPG sound effects", "magic spells SFX") | Read each license | Some excellent cheap packs |
| Unity Asset Store (free/paid SFX) | Asset Store EULA (commercial OK) | Already Unity-ready |
| [jsfxr](https://sfxr.me) / ChipTone | Your own output | Fast placeholders while prototyping |

### Music
| Source | License | Notes |
|---|---|---|
| itch.io music packs (search "fantasy orchestral loop", "medieval music pack") | Read each license | Look for packs with **seamless loops + stingers** |
| [Alexander Nakarada (serpentsoundstudios)](https://www.serpentsoundstudios.com) | CC-BY | Lots of fantasy/medieval tracks |
| [Kevin MacLeod — incompetech](https://incompetech.com) | CC-BY | Very recognizable; fine for a jam-style entry |
| [Pixabay Music](https://pixabay.com/music/) | Pixabay license | Filter "fantasy", "medieval", "epic" |
| [Eric Matyas — soundimage.org](https://soundimage.org) | Free w/ attribution | Fantasy loops |
| Unity Asset Store music packs | EULA | Many fantasy packs with loops/stingers, often on sale |
| A composer friend / student | Agree in writing | Best result if available |

## 5. How to choose (listening checklist)
For each candidate sound, play it **in context** (in the headset, with other sounds), then ask:
1. **Readability:** can you identify it in < 0.3 s with other sounds playing? Short attack = good for actions.
2. **Size:** does it match how big the thing is? Tiny goblins → small, high-pitched; thunder → big, but not ear-splitting (it repeats!).
3. **Family:** does it sound like it belongs with the others (same reverb, same "cartoon-ness")?
4. **Repetition:** would you tolerate it 200 times in 10 minutes? If not, get 3–5 variants or make it quieter.
5. **Frequency space:** UI clicks high, impacts mid-low, music not fighting SFX (music leaves mids open during waves).
For music: does the loop point click? Does battle music stay exciting but not tiring over 8 minutes? Is there a clear **calm (build) vs. intense (wave)** version — ideally the same theme in two intensities so we can crossfade.

## 6. Processing (free tools)
- **Audacity** (free) or **Reaper** ($60, unlimited trial): trim silence at start (latency kills feel!), fade-outs, normalize.
- **Loudness:** normalize SFX peaks around -3 dBFS and keep categories consistent; music around -16 LUFS integrated, then balance in the Unity mixer.
- Export **WAV 48 kHz 16-bit** into the repo (LFS); Unity compresses on import.
- Name files per [../tech/conventions.md](../tech/conventions.md): `sfx_ability_thunder_01.wav`, `mus_battle_loop.wav`.

## 7. Implementation in Unity
- **Import settings:** short SFX → *Decompress On Load*, Vorbis/ADPCM; long ambience → *Compressed In Memory*; music → *Streaming*, Vorbis ~q70. Force To Mono for 3D SFX.
- **`SoundEvent` ScriptableObjects** (see architecture): list of clip variants, volume, random pitch range (±5–10%), mixer group, cooldown/max-simultaneous voices (important: 30 goblins dying at once).
- **AudioMixer groups:** Master → Music, SFX (Abilities, Units, Towers), UI, Ambience. **Duck music** a few dB on big ability hits. Snapshots for Pause (lowpass) and Boss.
- **Spatial audio:** use the **Meta XR Audio SDK** spatializer for sounds on the board (enemies, towers, impacts) so they come *from the table*. UI and music stay 2D. Use short min distance, since the whole board is within 1 m.
- **Hand feedback without haptics:** every grab/release/gesture-armed state gets a distinct short sound attached to the hand position. This is our replacement for controller rumble.
- **Music state machine:** Placement theme → Build (calm) ↔ Wave (intense) crossfade → Boss → Victory/Defeat stinger.

## 8. Workflow
1. Week 2: placeholder pass (Kenney + jsfxr) for every P0 event — no silent actions.
2. Week 4: audio pass 1 — real SFX for abilities/heroes, 1 battle music loop.
3. Week 5: audio pass 2 — all P1, mixing session **in the headset**, ambience.
4. Week 6: final mix on the demo-video build.
