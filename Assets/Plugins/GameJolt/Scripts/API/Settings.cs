using UnityEngine;

namespace GameJolt.API
{
	/// <summary>
	/// API Settings Config Object.
	/// </summary>
	[System.Serializable]
	public class Settings : ScriptableObject {
		[Header("Game")]
		[Tooltip("The game ID. It can be found on the Game Jolt website under Dashboard > YOUR-GAME > Game API > API Settings.")]
		public int gameID;
		[Tooltip("The game Private Key. It can be found on the Game Jolt website under Dashboard > YOUR-GAME > Game API > API Settings.")]
		public string privateKey;

		[Header("Settings")]
		[Tooltip("The time in seconds before an API call should timeout and return failure.")]
		public float timeout = 10f;
		[Tooltip("Automatically create and ping sessions once a user has been authentified.")]
		public bool autoPing = true;
		[Tooltip("Automatically show a message if a user has successfully signed in our out")]
		public bool autoSignInOutMessage;
		[Tooltip("If AutoSignInOutMessage is set to true, this message will be shown if a user has signed in.")]
		public string signInMessage = "Signed in as '{0}'";
		[Tooltip("If AutoSignInOutMessage is set to true, this message will be shown if a user has signed out.")]
		public string signOutMessage = "Signed out";
		[Tooltip("Cache High Score Tables and Trophies information for faster display.")]
		public bool useCaching = true;
		[Tooltip("The key used to encrypt the user credentials.")]
		public string encryptionKey = "";
		[Tooltip("Set LogLevel for all GameJolt API log messages. Messages below this level will be discarded.")]
		public LogHelper.LogLevel LogLevel = LogHelper.LogLevel.Warning;
		[Tooltip("List of trophies which are only shown if the user has achieved them.")]
		public int[] secretTrophies;

		[Header("Debug")]
		[Tooltip("AutoConnect in the Editor as if the game was hosted on GameJolt.")]
		public bool autoConnect;
		[Tooltip("The username to use for AutoConnect.")]
		public string user;
		[Tooltip("The token to use for AutoConnect.")]
		public string token;
	}
}