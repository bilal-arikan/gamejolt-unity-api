# Change Log
All notable changes to this project will be documented in this file.
This project adheres to [Semantic Versioning](http://semver.org/).

## [Unreleased][Unreleased]

## [2.5.7][v2_5_7] (2021-08-07)

### Fixed
- `TryUnlock` would still show two notifications if requested faster than the server could reply
- Get and Post requests were not wrapped in a `using` statement

## [2.5.6][v2_5_6] (2020-03-06)

### Fixed
- `NullReferenceException` when accessing the `GameJoltUI` from within the `AutoSignIn` callback (see issue [#45](https://github.com/InfectedBytes/gj-unity-api/issues/45))

## [2.5.5][v2_5_5] (2019-12-20)

### Add
- `SettingsProvider` for Unity 2018.3 or newer (see issue [#42](https://github.com/InfectedBytes/gj-unity-api/issues/42))
- `Trophies.TryUnlock` method, which will first check if the trophy is already unlocked (see issue [#43](https://github.com/InfectedBytes/gj-unity-api/issues/43))

### Fixed
- Build error on newer Unity versions. Fixed `GameJoltEditor.asmdef` file
- Fixed small error in `tutorial.md` (see issue [#40](https://github.com/InfectedBytes/gj-unity-api/issues/40))

## [2.5.4][v2_5_4] (2019-08-09)

### Add
- Assembly Definition Files (asmdef)
  - Now the GameJolt API resides in it's own assembly.
  - This improves the build time
  - if you're already using Assembly Definition files, you need to add a reference to the `GameJoltRuntime`

## [2.5.3][v2_5_3] (2018-09-22)

### Add
- Guest support for `Scores.Get*`
- `AutoLoginEvent` which is called for the AutoLogin mechanism, for e.g. when the `Remember me` option was set.

## [2.5.2][v2_5_2] (2018-08-05)

### Fixed
- `DataStore.SetSegmented` error for large data which needs to be url encoded 
(see issue [#31](https://github.com/InfectedBytes/gj-unity-api/issues/31))
- Added some `#if UNITY_..._OR_NEWER` macros to accomondate Unity's API changes (see issue [#34](https://github.com/InfectedBytes/gj-unity-api/issues/34))

## [2.5.1][v2_5_1] (2018-07-21)

### Fixed
- GameJolt changed `/get-time` API call to `/time`

## [2.5.0][v2_5_0] (2018-07-15)

### Add
- More User Infos (see issue [#24](https://github.com/InfectedBytes/gj-unity-api/issues/24))
  - SignedUp, SignedTimestamp, LastLoggedIn, LastLoggedinTimestamp, DeveloperName, DeveloperSite, DeveloperDescription
- `Trophies.RemoveTrophy` API call (see issue [#25](https://github.com/InfectedBytes/gj-unity-api/issues/25))
- Implemented Scores get `better_than`/`worse_than` options (see issue [#22](https://github.com/InfectedBytes/gj-unity-api/issues/22))
- Implemented Friends API (see issue [#23](https://github.com/InfectedBytes/gj-unity-api/issues/23))
- `Sessions.Check` (see issue [#4](https://github.com/InfectedBytes/gj-unity-api/issues/4))
  - Currently this method uses a little workaround, because GameJolt misuses the `success` argument for this check.
  - Once GameJolt has fixed it, we have to adapt the check method.
- `DataStore.SetSegmented` (see issue [#16](https://github.com/InfectedBytes/gj-unity-api/issues/16))
  - Workaround for GameJolt's 1MB per request limitation.
  - This method will therefore upload the data in several smaller parts.

### Changed
- Now using GameJolt API 1_2
- Bumped minimum Version of Unity to 5.5 (see issue [#15](https://github.com/InfectedBytes/gj-unity-api/issues/15))
- Replaced `WWW` with `UnityWebRequest` (see issue [#14](https://github.com/InfectedBytes/gj-unity-api/issues/14))

### Removed
- Removed WebPlayer, because Unity has dropped it in Version 5.4

## [2.4.0][v2_4_0] (2018-05-04)
#### :warning: "BigRefactor"-Update, contains breaking changes

### Add
- Added LogHelper class and LogLevel setting. All log messages with a level below the provided one are discarded. (see issue [#2](https://github.com/InfectedBytes/gj-unity-api/issues/2))

### Changed
- Renamed Manager classes to `GameJoltAPI` and `GameJoltUI` (see issue [#21](https://github.com/InfectedBytes/gj-unity-api/issues/21))
  - :warning: This is a breaking change! If you're migrating from an older version, please have a look at the migration page.
- *"Mega Refactoring"* - refactored nearly all files (see issue [#20](https://github.com/InfectedBytes/gj-unity-api/issues/20))
  - applied consistent style (code formatting)
  - corrected namespaces
  - fixed naming scheme (now consistently using C# CamelCase)
  - used explicit visibility modifiers
  - adjusted visibility
  - :warning: This is possibly a breaking change, because it changes the naming scheme

### Fixed
- issue [#2](https://github.com/InfectedBytes/gj-unity-api/issues/2): Debug option for the API
- issue [#19](https://github.com/InfectedBytes/gj-unity-api/issues/19): Got rid of all `Resources.Load` calls. Instead those assets are now directly linked via the Manager prefab.
- issue [#20](https://github.com/InfectedBytes/gj-unity-api/issues/20): huge refactoring
- issue [#21](https://github.com/InfectedBytes/gj-unity-api/issues/21): renamed `Manager` classes

## [2.3.1][v2_3_1] (2018-03-03)

### Add
- New `GameJolt.API.Manager.HasUser` property, which returns true if there is a current user (which is not necessarily signed in).
- New `GameJolt.API.Manager.HasSignedInUser` property, which returns true if there is an authenticated user.
- Added `Show Token` toggle to the sign in window.
- New `GameJolt.API.Manager.ShowLeaderboards(Action<bool> callback, int? activeTable, params int[] visibleTables)` method, which shows only the provided tables and also opens the table given by the activeTable parameter.
- Added `Create Account` button to sign in window, which opens the gamejolt sign up page in the webbrowser
- Added `GameJolt.API.Settings.secretTrophies` setting, which defines which trophies are secret.
- Added `GameJolt.API.Manager.IsSecretTrophy` method, which returns whether the provided trophy id is secret or not.
- Added `GameJolt.API.Objects.Trophy.IsSecret` property (GameJolt does not yet return whether a trophy is secret or not, therefore this property is filled from the settings.)
- Added support for automatic sign in and out messages. If `GameJolt.API.Settings.autoSignInOutMessage` is set to true, the message defined by `GameJolt.API.Settings.signInMessage` and `GameJolt.API.Settings.signOutMessage` will be shown if the user signs in/out. (fixes [#17](https://github.com/InfectedBytes/gj-unity-api/issues/17))

## [2.3.0][v2_3_0] (2017-11-08)

### Add
- New `GameJolt.API.DataStore.GetKeys(bool global, string pattern, Action<string[]> callback)` method to fetch all datastore keys matching the provided pattern.
- New "Remember me" functionality for SignIn (user credentials stored in PlayerPrefs with XTEA encryption), see issue loicteixeira/gj-unity-api#18
- Demo scenes

### Changed
- Changed SignIn signature to `void SignIn(Action<bool> signedInCallback = null, Action<bool> userFetchedCallback = null, bool rememberMe = false)`

### Fixed
- Users.Get(int[] ids, ...) throws an exception loicteixeira/gj-unity-api#79

## [2.2.0][v2_2_0] (2017-08-13)

### Add
- New `GameJolt.API.Scores.GetRank(int value, int table = 0, Action<int> callback = null)` method to fetch the rank of a given score value. loicteixeira/gj-unity-api#29
- New `GameJolt.API.Misc.GetTime(Action<DateTime> callback)` method to get the server time. loicteixeira/gj-unity-api#24
- `User.SignIn` and `GameJolt.UI.Manager.Instance.ShowSignIn` now accept a second callback `Action<bool> userFetchedCallback = null` which is called once all the attributes of the user have been populated. The first callback is called like before, as soon as the user has been successfully signed-in. loicteixeira/gj-unity-api#48
  - *Thanks to @movrajr for reporting the issue and discussing solutions.*

### Changed
- Use HTTPS for API calls. loicteixeira/gj-unity-api#76
- Use API version `1.2`. loicteixeira/gj-unity-api#23
  - *Thanks to @jianmingyong for his insight.*

## [2.1.3][v2_1_3] (2017-07-19)

### Fixed
- Unity 2017.1 compatibility. loicteixeira/gj-unity-api#73
  - *Thanks to @DanielJMus for his contribution and @mgeorgedeveloper for his help.*

## [2.1.2][v2_1_2] (2017-04-09)

### Fixed
- Unity 5.5 compatibility. loicteixeira/gj-unity-api#70
  - *Thanks to Moire (hkid800) for reporting the issue*
- Leaderboard window sometimes not showing scores. loicteixeira/gj-unity-api#47, loicteixeira/gj-unity-api#65
  - Thanks to Derpybunneh, Nanapus and sebasrez for reporting the issue and helping diagnosing it*

## [2.1.1][v2_1_1] (2016-06-21)

### Fixed
- Could not unlock trophies. loicteixeira/gj-unity-api#64
  - *Thanks to RomejanicDev for reporting the issue.*

## [2.1.0][v2_1_0] (2016-06-19)

### Warning
- The minimum version to use the API is now **Unity 5.0.1**.
- License is now MIT (less restrictive). loicteixeira/gj-unity-api#62.

### Fixed
- ShowLeaderboards callback not being called. loicteixeira/gj-unity-api#51
  - *Thanks to @michidk for his contribution and @WizzardMaker for his help.*
- Call signature could be invalid when using URL with forward slash. loicteixeira/gj-unity-api#59
  - *Thanks to DerpVulpes for his contribution.*

### Other
- Thanks to @andyman for helping other users on Twitter.


## [2.0.2][v2_0_2] (2015-07-18)

### Fixed
- AutoConnect for WebGL builds was broken. loicteixeira/gj-unity-api#46.
  - *Thanks to David Florek for reporting the issue.*
- The UI was not on the UI layer. loicteixeira/gj-unity-api#49.
  - *Thanks to Piotrek for reporting the issue.*

### Deprecated
- Unity 5.0.0 has a bug regarding md5 calculations for WebGL builds. It has been fixed in Unity 5.0.1. Therefore **the API will require Unity 5.0.1 from the next update.**


## [2.0.1][v2_0_1] (2015-07-11)

### Fixed
- Allow UI do display when Time.timeScale is set to 0.
- Automatically create EventSystem if there is none.
  - *Thanks to Gosh Darn Games for reporting the issue.*
- Prevent user to be unauthenticated if he signed in with his name with a different case as what is stored in GameJolt database.
  - *Thanks to @sebasrez for reporting the issue and providing useful information.*

## [2.0.0][v2_0_0] (2015-07-02)

Initial release

[Unreleased]: https://github.com/InfectedBytes/gj-unity-api/compare/v2.5.7...HEAD
[v2_5_7]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.5.7
[v2_5_6]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.5.6
[v2_5_5]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.5.5
[v2_5_4]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.5.4
[v2_5_3]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.5.3
[v2_5_2]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.5.2
[v2_5_1]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.5.1
[v2_5_0]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.5.0
[v2_4_0]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.4.0
[v2_3_1]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.3.1
[v2_3_0]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.3.0
[v2_2_0]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.2.0
[v2_1_3]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.1.3
[v2_1_2]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.1.2
[v2_1_1]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.1.1
[v2_1_0]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.1.0
[v2_0_2]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.0.2
[v2_0_1]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.0.1
[v2_0_0]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.0.0
