# Unity & C# conventions

## Folder layout
```
Assets/
  _Project/
    Art/            materials, textures, VFX made by us
    Audio/          Music/, SFX/, Mixers/  (imported clips we chose, renamed)
    Data/           ScriptableObject instances (Towers/, Enemies/, Waves/, Sounds/)
    Prefabs/        Board/, Towers/, Heroes/, Enemies/, VFX/, UI/
    Scenes/         Bootstrap.unity, Levels/, Sandbox/
    Scripts/
      Runtime/      GuardianRealm.Runtime.asmdef  (namespaces GuardianRealm.*)
      Editor/       GuardianRealm.Editor.asmdef
      Tests/        EditMode/, PlayMode/
  Art/    (sibling of _Project, i.e. Assets/Art/)
    <pack-folder>/      original pack name, e.g. kenney_castle-kit, KayKit_Adventurers_2.0_FREE
```
- Never modify files under `Assets/Art/` directly — make a prefab variant / material copy in `_Project`.
- `Assets/Oculus/`, `Assets/MetaXR/`, `Assets/XR/` etc. created by SDKs stay where the SDK puts them.

## Naming
| Thing | Convention | Example |
|---|---|---|
| Scripts / classes | PascalCase, one class per file | `WaveSpawner.cs` |
| Private fields | `_camelCase`; serialized: `[SerializeField] private float _speed;` | |
| Prefabs | `PF_<Category>_<Name>` | `PF_Tower_Archer` |
| ScriptableObjects | `<Type>_<Name>` | `Tower_Archer`, `Wave_L1_03`, `SFX_Thunder_Strike` |
| Materials | `M_<Name>` | `M_Board_Grass` |
| Scenes | `Level_NN_Name`, `Sandbox_<Person>` | |
| Audio files | `sfx_<category>_<name>_NN`, `mus_<name>` | `sfx_ability_thunder_01.wav` |

## C#
- Namespaces `GuardianRealm.<System>`. `.editorconfig`-style defaults: 4 spaces, braces on new lines (Unity style).
- No `FindObjectOfType`, `GameObject.Find`, `GetComponent` in `Update`. Cache in `Awake`.
- No LINQ / allocations in per-frame code. Use pools.
- `[SerializeField] private` over public fields. Validate references in `OnValidate` where useful.
- Comments explain *why*, not *what*.

## Project settings (must stay consistent)
- Asset Serialization: **Force Text**; Version Control: **Visible Meta Files**.
- Color space Linear, URP, Android ARM64, IL2CPP, Vulkan. (See [../setup/unity-mr-quest3.md](../setup/unity-mr-quest3.md).)
- All teammates use **the exact same Unity version** (pinned in `ProjectSettings/ProjectVersion.txt`). Upgrading = ADR + whole team.

## Prefab-first
Scenes contain mostly prefab instances. Make changes in prefabs so two people rarely touch the same scene file.
