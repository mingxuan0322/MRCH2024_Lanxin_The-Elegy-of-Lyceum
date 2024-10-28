# Release notes for MRCH Template

## v1.35

Date: 10.28.2024, by Shengyang

GitHub Commit Hash: [ToBeFilledNextTime]

**Changes:**

- Add Gizmos*  and a `useGizmos` option to `Interaction Trigger`, `Touch Manager`, `Move And Rotate`, and `XR Origin Editor Control`. This enhancement will let you visualize the distance range of *Distance Triggers*, *LookAt Triggers*, and *Touch Range*. Additionally, you’ll see a line connecting the moving object to its target and a ray projecting from the camera's front, helping to estimate the LookAt angle.

**Gizmos are used to give visual debugging or setup aids in the Scene view.*

## v1.3

Date: 10.26.2024, by Shengyang

GitHub Commit Hash: 368edf5997489b7806db809ec252f55e919e3a9a

**Changes:**

- Introduced `EventBroadcaster.cs` to broadcast `Initialized` and `Reset` events for the ImmersalSDK component, along with `FirstLocalized` and `SuccessfulLocalized` events for the Localizer component. Refer to **QuickStart.md** for instructions on how to implement these updates in your scene, or you can copy from the updated template scene.
- Added `XR Origin Editor Controller` to the template scenes. For details on how to apply it to your scenes, see **QuickStart.md** or copy the template scenes directly.
- Enhanced `MoveAndRotate` with options for movement and rotation following the first successful localization.

**Fixed:**

- Resolved an issue where `MoveAndRotate` was initialized on `FirstLocalized` instead of during the awake phase.

## v1.2

Date: 10.22.2024, by Shengyang and Ian

GitHub Commit Hash: ba1418ec7057c2b5b998b1c3624682eda7caf4ff & 14516e0676a665463889ab223615b401d7c3b8a9

Changes: 

* Add an `AudioController.cs` for audio FadeIn and FadeOut.
* Asked GPT to fix the grammer issues of the release note of last time.

## v1.1

Date: 10.19.2024, by Shengyang

Github Commit Hash: c2b83b2797506efc76aa6f15e02e1dd88a24c20c

Changes:

* Updated the structure of abstract scripts and wrappers. They are now organized under separate namespaces.
* Changed the input system from a combination of the Input Manager and Input System to using only the Input System. Fixed the issue with screen touch handling. You can now test touch interactions in the editor runtime in both Game and Simulator modes.
* Improved the `MapModel` script. It is now excluded from the built version to reduce the package size and improve efficiency.
* Added a ‘Start New Line When Overflow’ option to the Simple TMP Typewriter to enhance the typing process, especially for English text. (It’s not so simple anymore, haha.)
* **Added the `XR Origin Editor Controller`. You can now use WASD to move in the editor and IJKL or right-click to rotate the camera for quick testing of your interactions.**
* Exposed more variables for inheritance.
* Fixed several issues.

Known Issues:

* Some initializations should be delayed until localization is completed. This primarily affects certain functions in `MoveAndRotate` when returning to the initial position.
* Some on-screen text does not display correctly.
* You may not be able to locate the Map Model GameObject in the scene. Manually re-extracting textures can resolve this issue.

## v1.0

Date: 10.17.2024, by Shengyang

Github Commit Hash: 4b93f45441cbe655227272d7aca8c54fde75807b

Changes: 

* Finished the rough documentation and asked AI to improve it. So, you may see the style is not unified. I guess it’s fine... Anyway I will probably keep improve it later.