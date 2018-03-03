# Change Log
All notable changes to this project will be documented in this file.
This project adheres to [Semantic Versioning](http://semver.org/).

## [Unreleased][Unreleased]

### Add
- New `GameJolt.API.Manager.HasUser` property, which returns true if there is a current user (which is not necessarily signed in).
- New `GameJolt.API.Manager.HasSignedInUser` property, which returns true if there is an authenticated user.
- Added `Show Token` toggle to the sign in window.
- New `GameJolt.API.Manager.ShowLeaderboards(Action<bool> callback, int? activeTable, params int[] visibleTables)` method, which shows only the provided tables and also opens the table given by the activeTable parameter.
- Added `Create Account` button to sign in window, which opens the gamejolt sign up page in the webbrowser
- Added `GameJolt.API.Settings.secretTrophies` setting, which defines which trophies are secret.
- Added `GameJolt.API.Manager.IsSecretTrophy` method, which returns whether the provided trophy id is secret or not.
- Added `GameJolt.API.Objects.Trophy.IsSecret` property (GameJolt does not yet return whether a trophy is secret or not, therefore this property is filled from the settings.)
- Added support for automatic sign in and out messages. If `GameJolt.API.Settings.autoSignInOutMessage` is set to true, the message defined by `GameJolt.API.Settings.signInMessage` and `GameJolt.API.Settings.signOutMessage` will be shown if the user signs in/out.

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

[Unreleased]: https://github.com/InfectedBytes/gj-unity-api/compare/v2.3.0...HEAD
[v2_3_0]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.3.0
[v2_2_0]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.2.0
[v2_1_3]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.1.3
[v2_1_2]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.1.2
[v2_1_1]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.1.1
[v2_1_0]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.1.0
[v2_0_2]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.0.2
[v2_0_1]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.0.1
[v2_0_0]: https://github.com/InfectedBytes/gj-unity-api/tree/v2.0.0
