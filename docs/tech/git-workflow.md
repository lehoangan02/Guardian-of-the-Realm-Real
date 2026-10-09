# Git workflow

## One-time setup (every teammate, Mac & Windows)
```bash
git lfs install                 # once per machine
git clone <repo-url>
cd Guardian-of-the-Realm
git lfs install                 # inside the repo too: installs the pre-push hook that uploads LFS files
git lfs pull
```
**UnityYAMLMerge** (smart merges for scenes/prefabs). Add to your *global* git config, using your Unity install path:

macOS:
```bash
git config --global merge.unityyamlmerge.name "Unity SmartMerge"
git config --global merge.unityyamlmerge.driver '"/Applications/Unity/Hub/Editor/6000.3.16f1/Unity.app/Contents/Helpers/UnityYAMLMerge" merge -p %O %B %A %A'
git config --global merge.unityyamlmerge.recursive binary
```
Windows (Git Bash):
```bash
git config --global merge.unityyamlmerge.name "Unity SmartMerge"
git config --global merge.unityyamlmerge.driver '"C:/Program Files/Unity/Hub/Editor/6000.3.16f1/Editor/Data/Tools/UnityYAMLMerge.exe" merge -p %O %B %A %A'
git config --global merge.unityyamlmerge.recursive binary
```
> Unity 6 on macOS moved the tool from `Contents/Tools/` to `Contents/Helpers/`. If the path doesn't exist, run `find /Applications/Unity/Hub/Editor -name UnityYAMLMerge -type f`. Windows users should check the `.exe` path exists the same way.

`.gitattributes` is the standard [Unity template](https://github.com/gitattributes/gitattributes/blob/master/Unity.gitattributes) (plus `glb`/`gltf`/`flac`). It forces LF line endings for Unity YAML/JSON files; leave Git for Windows' default `core.autocrlf` alone.

## What goes in LFS
Everything binary (models, textures, audio, video, fonts, zips, `.unitypackage`) — see `.gitattributes`. If you add a new binary type, add it there **before** committing the files.

## Branches & PRs
- `main` is always buildable. No direct commits.
- Branch per task: `feat/board-placement`, `fix/thunder-double-fire`, `docs/audio-guide`, `art/level1-dressing`.
- Small PRs (< ~1 day of work). Another teammate reviews; for agent-written PRs, the human who ran the agent is responsible and must have tested it.
- PR description: what, why, how tested (editor / simulator / device), screenshots/video for visible changes.
- Rebase or merge `main` into your branch often; resolve scene conflicts with the scene owner.

## Scene ownership (avoid unmergeable conflicts)
| Scene | Owner |
|---|---|
| `Bootstrap` | Le Hoang An |
| `Level_01_ForestRoad` | Nguyen Duc Thinh |
| `Level_02_CastleSiege` | Tran Duc An |
Only the owner edits a scene; others change prefabs or ask the owner. Reassign here when needed. Agents must respect this table.

## Commit messages
`<area>: <imperative summary>` — e.g. `abilities: add thunder strike on fist under cloud`. Agent-assisted commits keep the agent's co-author trailer.

## Big files & imports
Import asset packs **only what we use** (pick individual models/sounds), into `Assets/Art/...`, and log them in [../assets/credits.md](../assets/credits.md).
