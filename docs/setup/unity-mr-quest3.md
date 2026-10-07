# Setting up the Unity MR project for Meta Quest 3

> Written 2026-10-07 against Unity 6 LTS and Meta XR SDK v207-era docs. Menu names drift between SDK versions — if something doesn't match, ask your agent to use the `hz-unity-meta-core-sdk` / `hz-new-project-creation` skills, then fix this doc.

**Who does what:** Step 0 is for everyone. Steps 1–4 are done **once by one person**, pushed, then everyone else clones and does step 5.

> **Status (2026-10-07):** Step 1 is done (Le Hoang An created the project with the Universal 3D template, Unity 6000.3.16f1, template leftovers removed). Steps 2–4 are next.

## 0. Every teammate: install tools
- [ ] **Unity Hub** + the team's pinned **Unity 6 LTS** version: **`6000.3.16f1`** (Unity 6.3 LTS, pinned 2026-10-07 in `ProjectSettings/ProjectVersion.txt`). Install exactly this version.
  - Modules: **Android Build Support** with **OpenJDK** and **Android SDK & NDK Tools**.
  - Windows: also Visual Studio / Rider. Mac: Rider or VS Code with the C# Dev Kit + Unity extension.
- [ ] **Git + Git LFS** → follow [../tech/git-workflow.md](../tech/git-workflow.md).
- [ ] **Meta Horizon (Meta Quest) mobile app** — pair your Quest 3, enable **Developer Mode** (requires the developer org you're in as Start members).
- [ ] **Meta Quest Developer Hub (MQDH)** (Mac & Windows) — device manager, install APKs, logs, casting/recording (useful for the demo video).
- [ ] **Windows only:** **Meta Horizon Link** (Quest Link) app for Play-in-Editor on the headset.
- [ ] **Meta XR Simulator** (installed via the Meta XR SDK in Unity) — test without a headset; works on Mac.
- [ ] **Meta VR CLI**: `npx metavr@latest init` (also sets up agent skills — see [agent-tooling.md](agent-tooling.md)).
- [ ] On Quest 3: Settings → Hands and body tracking → **Hand tracking ON**, auto-switch from controllers ON. Run **Space Setup** in the room with your table so the table is captured as a furniture object.

## 1. Create the project (one person)
1. Unity Hub → New project → **Universal 3D (URP)** template → name `Guardian-of-the-Realm`.
   - **Not** Unity's "Mixed Reality (MR)" template. That one is built on Unity's cross-platform XR Interaction Toolkit and AR Foundation. We use Meta's Interaction SDK, MRUK and Building Blocks instead (better hand gestures, table detection, Meta agent skills target them). Running both interaction stacks causes duplicate rigs and input conflicts. The "MR Multiplayer Tabletop" template has the same problem and adds networking we don't need.
2. The project lives at the repo root. Delete the template leftovers (`Assets/TutorialInfo/`, `Assets/Readme.asset`).
3. Edit → Project Settings → Editor: **Asset Serialization = Force Text**, **Version Control = Visible Meta Files**.

## 2. Platform & XR plug-in
1. File → Build Profiles (Build Settings) → **Android** → Switch Platform. Texture compression **ASTC**.
2. Project Settings → **XR Plug-in Management** → install → Android tab: check **OpenXR** (Unity OpenXR is the recommended plug-in for Unity 6 + Meta SDK v74+; the old *Oculus XR plug-in* is deprecated — don't use it).
3. Under OpenXR (Android): enable the **Meta Quest** feature group / *Meta Quest Support*; interaction profiles: **Hand Interaction Profile** (+ Meta hand tracking aim). We still may list Oculus Touch profile for editor convenience but the game must never require it.

## 3. Meta XR SDK
1. Package Manager → *Unity Registry / My Assets* → add **Meta XR All-in-One SDK** (or individually: **Meta XR Core SDK, Meta XR Interaction SDK, Meta MR Utility Kit (MRUK), Meta XR Audio SDK, Meta XR Simulator**). Keep all Meta packages on the **same version**.
2. Open **Meta → Tools → Project Setup Tool** → *Fix All* for Android (and Windows/Mac editor). Re-run until green. It sets: Linear color, ARM64, IL2CPP, Vulkan, min API level, multiview, etc.
3. Player Settings checks: Scripting Backend **IL2CPP**, Target Architectures **ARM64 only**, Graphics API **Vulkan**, Color Space **Linear**, Minimum API level per setup tool.
4. URP asset: disable HDR, MSAA 4x, disable post-processing (or keep minimal), Render Scale 1.0; disable SSAO/depth/opaque texture unless needed.

## 4. Starter scene with Building Blocks
`Meta → Tools → Building Blocks`, drag into a new scene `Assets/_Project/Scenes/Bootstrap.unity`:
- [ ] **Camera Rig**
- [ ] **Passthrough** (MR — camera background must be transparent; the block handles this)
- [ ] **Hand Tracking** + **Virtual Hands / Synthetic hands** (Interaction SDK) — set *Hand tracking support* to **Hands Only** in OVRManager / Quest features (no controllers)
- [ ] **MR Utility Kit** (Scene / room data) — needs **Scene support** + **Spatial anchors support** permissions; MRUK gives labeled planes like `TABLE`, `FLOOR`
- [ ] **Hand Grab** + **Poke** interaction samples (for testing heroes / buttons)
- [ ] **Spatial Anchor** (Core) — to persist the board placement
Add a test cube (10 cm) on a grabbable, Build And Run → you should see your room, your hands, and be able to grab the cube. **This is the Week-1 "hello MR" milestone.**

Commit, push, done. Write the exact Unity + Meta SDK versions into this file and [../decisions/](../decisions/).

## 5. Everyone else
1. Clone (with LFS), open with the **same** Unity version from Unity Hub.
2. Wait for import; run Project Setup Tool once; Build And Run the Bootstrap scene to your Quest.

## Iteration loops
| OS | Fast loop | Device loop |
|---|---|---|
| Windows | **Quest Link** + Play mode in editor (hands work over Link) | Build And Run (USB-C) |
| macOS | **Meta XR Simulator** in Play mode (synthetic hands, simulated rooms incl. tables) | Build And Run (USB-C) or MQDH install |
Tips: use **Build And Run** with incremental builds; keep a small `Sandbox_<You>` scene; `metavr` CLI / MQDH for logcat.

## Board placement implementation notes
- MRUK: on scene loaded, query anchors with label `TABLE`; pick the largest surface in front of the user → propose placement (board centered, facing the user).
- No table / bad scan → fallback: user places the board on any detected plane, or floor mode.
- Board transform: Interaction SDK **Grab Free Transformer** (or two-hand transformer) constrained to yaw + uniform scale; keep the board level.
- Confirm → create **spatial anchor** at board origin, store UUID in `PlayerPrefs`; on next launch load + localize anchor, fallback to placement if it fails.

## Performance checklist (Quest 3, 72 fps)
- Fixed foveated rendering on, dynamic resolution allowed. ASW off for MR.
- Batching: GPU instancing / SRP batcher; Kenney/KayKit use a texture atlas → share materials.
- Shadows: one cheap blob/projected shadow for units, or a single low-res realtime shadow on the board only.
- Profile on device with OVR Metrics Tool / `hz-perfetto-debug` skill.

## Sources
- [Meta — Unity project setup](https://developers.meta.com/horizon/documentation/unity/unity-project-setup/)
- [Meta — Unity OpenXR](https://developers.meta.com/horizon/documentation/unity/unity-openxr)
- [Unity Manual — Develop for Meta Quest](https://docs.unity3d.com/6000.0/Documentation/Manual/xr-meta-quest-develop.html)
- [Meta — Building Blocks](https://developers.meta.com/horizon/blog/accelerate-development-mixed-reality-building-blocks-unity-meta-Quest-developers/)
