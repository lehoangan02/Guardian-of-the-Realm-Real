# Art assets

All packs below are **CC0** (public domain), so commercial use is fine and no credit is required. We credit them anyway in [credits.md](credits.md).

| Pack | Use | Link |
|---|---|---|
| KayKit — Adventurers | Heroes (Knight, Mage, Rogue, Barbarian…), rigged + animations | https://kaylousberg.itch.io/kaykit-adventurers |
| Kenney — Tower Defense Kit | Path tiles, towers, projectiles, enemies (placeholder) | https://kenney.nl/assets/tower-defense-kit |
| Kenney — Castle Kit | Castle gate (the thing we defend), walls, Level 2 | https://kenney.nl/assets/castle-kit |
| Kenney — Fantasy Town Kit | Houses, market, props for Level 1 | https://kenney.nl/assets/fantasy-town-kit |
| Kenney — Mini Forest | Trees, rocks, foliage | https://kenney.nl/assets/mini-forest |
| *(candidate)* KayKit — Skeletons / other KayKit packs | Enemies matching hero style | https://kaylousberg.itch.io |

## Import rules
- Download the **FBX** (or GLB with a glTF importer) version. Import only the models we use into `Assets/ThirdParty/<Author>/<Pack>/`.
- Never edit in place: make prefab variants in `Assets/_Project/Prefabs/`.
- **Scale:** Kenney tiles are ~1 unit; our board is ~0.8 m wide. Build levels at "world" scale under a `BoardRoot`, then scale `BoardRoot` (e.g. 0.05). All gameplay logic must be scale-independent (see [../tech/architecture.md](../tech/architecture.md)).
- **Materials:** Kenney & KayKit use one shared color-palette texture → convert to **URP/Lit** (or Simple Lit for perf) and **share one material** across the pack for batching.
- **Style consistency:** KayKit and Kenney are both low-poly, flat-colored; tune palette/saturation so heroes pop against the board (heroes slightly brighter, enemies with a distinct accent color).
- **Animations (KayKit):** Humanoid rig; use Idle, Walk, Attack, Hit, Death, and a "Dangling/Held" pose for when the player lifts a hero.

## MR-specific look
- The board needs a clear **base/plinth** (wooden table-top "game board" edge) so it reads as an object on the real table.
- Use a soft **contact shadow** under the board so it doesn't float.
- Keep saturated colors; passthrough is slightly washed out.
