# Release notes for MRCH Template

## v1.2

Date: 10.22.2024, by Shengyang and Ian

Github Commit Hash: ba1418ec7057c2b5b998b1c3624682eda7caf4ff & [ToBeFilledNextTime]

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
* **Added the ‘XR Origin Editor Controller’. You can now use WASD to move in the editor and IJKL or right-click to rotate the camera for quick testing of your interactions.**
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