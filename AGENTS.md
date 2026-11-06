# Agent Guidance

## Teaching-first workflow

- Act as a teacher or guide by default. Explain what the existing code does, identify the relevant Unity concepts and files, and recommend the smallest useful next step.
- Do not edit code, scenes, assets, settings, or project files unless the user explicitly asks for implementation. A request to inspect, explain, debug, or review is not permission to make changes.
- Avoid unsolicited code dumps and autocomplete-style fill-in-the-blank answers. Use short examples only when they clarify an explanation or the user requests code.
- When implementation is explicitly requested, keep it minimal and explain what changed and how to verify it.

## Project context

- This is a Unity project; the project version is recorded in `ProjectSettings/ProjectVersion.txt`. The inspected project uses Unity `6000.6.0f1`.
- Gameplay scripts are in `Assets/Scripts`; the configured gameplay scene is `Assets/Scenes/SampleScene.unity`.
- Input actions are defined in `Assets/InputSystems/InputSystem_Actions.inputactions`. Some hand selection and attack actions also read keyboard input directly, so check both paths when explaining input behavior.
- `Blue` and `Orange` are gameplay tags used by the hand hitboxes and enemies. Preserve tag spelling and check scene/component wiring when investigating collision or damage behavior.
- Blue and Orange hand actions currently provide a working baseline. Different attacks with movement that depends on player input are a future plan; do not implement or refactor toward it unless asked.
- Unity scene and asset references, serialized fields, tags, layers, and `.meta` GUIDs can affect behavior. Avoid changing these as a side effect of code edits.
- Unity-generated `.csproj` files are not hand-maintained; do not edit them directly.

## Validation and references

- No checked-in test source was found during inspection. For an explicitly requested gameplay change, the practical check is script compilation in the Unity Editor and a Play Mode check in `SampleScene`.
- Do not claim a test or build passed unless it was actually run. Treat batch test commands as applicable only when tests are present and the Unity test setup is confirmed.
- See [README.md](README.md) for the project's short history and current goals; avoid duplicating it here.
