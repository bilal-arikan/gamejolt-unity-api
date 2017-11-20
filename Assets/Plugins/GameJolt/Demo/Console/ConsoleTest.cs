using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using GameJolt.API.Objects;

public class ConsoleTest : MonoBehaviour
{
	#region Inspector Fields
	// Console
	public RectTransform consoleTransform;
	public GameObject linePrefab;

	// Users
	public InputField userNameField;
	public InputField userTokenField;
	public InputField userIdsField;

	// Scores
	public InputField scoreValueField;
	public InputField scoreTextField;
	public Toggle guestScoreToggle;
	public InputField guestNameField;
	public InputField tableField;
	public InputField limitField;
	public Toggle userScoresToggle;

	// Trophies
	public InputField trophyIDField;
	public InputField trophyIDsField;
	public Toggle unlockedTrophiesOnlyToggle;

	// DataStore
	public InputField keyField;
	public InputField valueField;
	public InputField modeField;
	public InputField patternField;
	public Toggle globalToggle;
	#endregion Inspector Fields

	#region Click Actions
	public void SignIn()
	{
		Debug.Log("Sign In. Click to see source.");

		var user = new GameJolt.API.Objects.User(userNameField.text, userTokenField.text);
		user.SignIn(
			signInSuccess => {
				AddConsoleLine(string.Format("Sign In {0}.", signInSuccess ? "Successful" : "Failed"));
			},
			userFetchSuccess => {
				AddConsoleLine(string.Format("User's Information Fetch {0}.", userFetchSuccess ? "Successful" : "Failed"));
			});
	}

	public void SignOut()
	{
		Debug.Log("Sign Out. Click to see source.");

		var isSignedIn = GameJolt.API.Manager.Instance.HasUser;
		if (isSignedIn)
		{
			GameJolt.API.Manager.Instance.CurrentUser.SignOut();
		}

		AddConsoleLine(string.Format("Sign Out {0}.", isSignedIn ? "Successful" : "Failed"));
	}

	public void GetUsersById()
	{
		Debug.Log("Get Users By Id. Click to see source.");

		var ids = ParseIds(userIdsField.text);
		GameJolt.API.Users.Get(ids, users => {
			if(users != null) {
				foreach(var user in users) {
					AddConsoleLine(string.Format("> {0} - {1}", user.Name, user.ID));
				}
				AddConsoleLine(string.Format("Found {0} user(s).", users.Length));
			}
		});
	}

	public void SessionOpen()
	{
		Debug.Log("Session Open. Click to see source.");

		GameJolt.API.Sessions.Open(success => {
			AddConsoleLine(string.Format("Session Open {0}.", success ? "Successful" : "Failed"));
		});
	}

	public void SessionPingActive()
	{
		Debug.Log("Session Ping Active. Click to see source.");

		GameJolt.API.Sessions.Ping(GameJolt.API.SessionStatus.Active, success => {
			AddConsoleLine(string.Format("Session Ping Active {0}.", success ? "Successful" : "Failed"));
		});
	}

	public void SessionPingIdle()
	{
		Debug.Log("Session Ping Idle. Click to see source.");

		GameJolt.API.Sessions.Ping(GameJolt.API.SessionStatus.Idle, success => {
			AddConsoleLine(string.Format("Session Ping Idle {0}.", success ? "Successful" : "Failed"));
		});
	}

	public void SessionClose()
	{
		Debug.Log("Session Close. Click to see source.");

		GameJolt.API.Sessions.Close(success => {
			AddConsoleLine(string.Format("Session Close {0}.", success ? "Successful" : "Failed"));
		});
	}

	public void GetTables()
	{
		Debug.Log("Get Tables. Click to see source.");

		GameJolt.API.Scores.GetTables(tables => {
			if (tables != null)
			{
				foreach (var table in tables.Reverse<GameJolt.API.Objects.Table>())
				{
					AddConsoleLine(string.Format("> {0} - {1}", table.Name, table.ID));
				}
				AddConsoleLine(string.Format("Found {0} table(s).", tables.Length));
			}
		});
	}

	public void AddScore()
	{
		if (guestScoreToggle.isOn)
		{
			Debug.Log("Add Score (for Guest). Click to see source.");

			var scoreValue = scoreValueField.text != string.Empty ? int.Parse(scoreValueField.text) : 0;
			var tableID = tableField.text != string.Empty ? int.Parse(tableField.text) : 0;

			GameJolt.API.Scores.Add(scoreValue, scoreTextField.text, guestNameField.text, tableID, "", success => {
				AddConsoleLine(string.Format("Score Add (for Guest) {0}.", success ? "Successful" : "Failed"));
			});
		}
		else
		{
			Debug.Log("Add Score (for Guest). Click to see source.");

			var scoreValue = scoreValueField.text != string.Empty ? int.Parse(scoreValueField.text) : 0;
			var tableID = tableField.text != string.Empty ? int.Parse(tableField.text) : 0;

			GameJolt.API.Scores.Add(scoreValue, scoreTextField.text, tableID, "", success => {
				AddConsoleLine(string.Format("Score Add {0}.", success ? "Successful" : "Failed"));
			});
		}
	}

	public void GetScores()
	{
		Debug.Log("Get Scores. Click to see source.");

		var tableID = tableField.text != string.Empty ? int.Parse(tableField.text) : 0;
		var limit = limitField.text != string.Empty ? int.Parse(limitField.text) : 10;
		GameJolt.API.Scores.Get(scores => {
			if (scores != null)
			{
				foreach (var score in scores.Reverse<GameJolt.API.Objects.Score>())
				{
					AddConsoleLine(string.Format("> {0} - {1}", score.PlayerName, score.Value));
				}
				AddConsoleLine(string.Format("Found {0} scores(s).", scores.Length));
			}
		}, tableID, limit, userScoresToggle.isOn);
	}

	public void GetRank()
	{
		Debug.Log("Get Rank. Click to see source.");

		var scoreValue = scoreValueField.text != string.Empty ? int.Parse(scoreValueField.text) : 0;
		var tableID = tableField.text != string.Empty ? int.Parse(tableField.text) : 0;

		GameJolt.API.Scores.GetRank(scoreValue, tableID, rank => {
			AddConsoleLine(string.Format("Rank {0}", rank));
		});
	}

	public void UnlockTrophy()
	{
		Debug.Log("Unlock Trophy. Click to see source.");

		var trophyID = trophyIDField.text != string.Empty ? int.Parse(trophyIDField.text) : 0;
		GameJolt.API.Trophies.Unlock(trophyID, success => {
			AddConsoleLine(string.Format("Unlock Trophy {0}.", success ? "Successful" : "Failed"));
		});
	}

	public void GetTrophies()
	{
		if (trophyIDsField.text.IndexOf(',') == -1)
		{
			Debug.Log("Get Single Trophy. Click to see source.");

			var trophyID = trophyIDsField.text != string.Empty ? int.Parse(trophyIDsField.text) : 0;
			GameJolt.API.Trophies.Get(trophyID, PrintTrophy);
		}
		else
		{
			Debug.Log("Get Multiple Trophies. Click to see source.");
			
			var trophyIDs = ParseIds(trophyIDsField.text);

			GameJolt.API.Trophies.Get(trophyIDs, trophies => {
				if (trophies != null)
				{
					foreach (var trophy in trophies.Reverse()) {
						PrintTrophy(trophy);
					}
					AddConsoleLine(string.Format("Found {0} trophies.", trophies.Length));
				}
			});
		}
	}

	public void GetAllTrophies()
	{
		Debug.Log("Get All Trophies. Click to see source.");

		GameJolt.API.Trophies.Get(trophies => {
			if (trophies != null)
			{
				foreach (var trophy in trophies.Reverse()) {
					PrintTrophy(trophy);
				}
				AddConsoleLine(string.Format("Found {0} trophies.", trophies.Length));
			}
		});
	}

	public void GetTrophiesByStatus()
	{
		Debug.Log("Get Trophies by Status (Unlocked or not). Click to see source.");

		GameJolt.API.Trophies.Get(unlockedTrophiesOnlyToggle.isOn, trophies => {
			if (trophies != null)
			{
				foreach (var trophy in trophies.Reverse<GameJolt.API.Objects.Trophy>()) {
					PrintTrophy(trophy);
				}
				AddConsoleLine(string.Format("Found {0} trophies.", trophies.Length));
			}
		});
	}

	private void PrintTrophy(Trophy trophy) {
		if(trophy != null) {
			AddConsoleLine(string.Format("> {0} - {1} - {2} - {3}Unlocked - {4}Secret", trophy.Title, trophy.ID,
				trophy.Difficulty, trophy.Unlocked ? "" : "Not ", trophy.IsSecret ? "" : "Not "));
		}
	}

	public void GetDataStoreKey()
	{
		Debug.Log("Get DataStore Key. Click to see source.");

		GameJolt.API.DataStore.Get(keyField.text, globalToggle.isOn, value => {
			if (value != null)
			{
				AddConsoleLine(string.Format("> {0}", value));
			}
		});
	}

	public void GetDataStoreKeys()
	{
		Debug.Log("Get DataStore Keys. Click to see source.");

		GameJolt.API.DataStore.GetKeys(globalToggle.isOn, patternField.text, keys => {
			if (keys != null)
			{
				foreach (var key in keys)
				{
					AddConsoleLine(string.Format("> {0}", key));
				}
				AddConsoleLine(string.Format("Found {0} keys.", keys.Length));
			}
			else
			{
				AddConsoleLine("No keys found.");
			}
		});
	}

	public void RemoveDataStoreKey()
	{
		Debug.Log("Remove DataStore Key. Click to see source.");

		GameJolt.API.DataStore.Delete(keyField.text, globalToggle.isOn, success => {
			AddConsoleLine(string.Format("Remove DataStore Key {0}.", success ? "Successful" : "Failed"));
		});
	}

	public void SetDataStoreKey()
	{
		Debug.Log("Set DataStore Key. Click to see source.");

		GameJolt.API.DataStore.Set(keyField.text, valueField.text, globalToggle.isOn, success => {
			AddConsoleLine(string.Format("Set DataStore Key {0}.", success ? "Successful" : "Failed"));
		});
	}

	public void UpdateDataStoreKey()
	{
		GameJolt.API.DataStoreOperation mode;
		try
		{
			mode = (GameJolt.API.DataStoreOperation) System.Enum.Parse(typeof(GameJolt.API.DataStoreOperation), modeField.text);
		}
		catch
		{
			Debug.LogWarning("Wrong Mode. Should be Add, Subtract, Multiply, Divide, Append or Prepend.");
			return;
		}

		Debug.Log("Update DataStore Key. Click to see source.");

		GameJolt.API.DataStore.Update(keyField.text, valueField.text, mode, globalToggle.isOn, value => {
			if (value != null)
			{
				AddConsoleLine(string.Format("> {0}", value));
			}
		});
	}

	public void GetTime()
	{
		Debug.Log("Get Time. Click to see source.");

		GameJolt.API.Misc.GetTime(time => {
			AddConsoleLine(string.Format("Server Time: {0}", time));
		});
	}
	#endregion Click Actions

	#region Internal
	void Start()
	{
		// Do not try this at home! Seriously, you shouldn't.
		var settings = Resources.Load(GameJolt.API.Constants.SETTINGS_ASSET_NAME) as GameJolt.API.Settings;
		if (settings != null)
		{
			userNameField.text = settings.user;
			userTokenField.text = settings.token;
		}
		userIdsField.onValidateInput += ValidateIdList;
		trophyIDsField.onValidateInput += ValidateIdList;
	}

	void AddConsoleLine(string text)
	{
		var tr = Instantiate<GameObject>(linePrefab).transform;
		tr.GetComponent<Text>().text = text;
		tr.SetParent(consoleTransform);
		tr.SetAsFirstSibling();
	}

	private char ValidateIdList(string text, int index, char addedChar) {
		if(addedChar >= '0' && addedChar <= '9' || addedChar == ',') return addedChar;
		return '\0';
	}

	private int[] ParseIds(string text) {
		return text.Split(',').Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x)).ToArray();
	}
	#endregion Internal
}
