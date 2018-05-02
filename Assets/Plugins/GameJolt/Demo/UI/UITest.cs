using GameJolt.API;
using GameJolt.UI;
using UnityEngine;
using UnityEngine.UI;

public class UITest : MonoBehaviour {
	public Button showTrophiesButton;

	int notificationQueued = 0;

	public void SignInButtonClicked()
	{
		GameJoltUI.Instance.ShowSignIn((bool signInSuccess) => {
			if (signInSuccess)
			{
				showTrophiesButton.interactable = true;
				Debug.Log("Logged In");
			}
			else
			{
				Debug.Log("Dismissed or Failed");
			}
		}, (bool userFetchSuccess) => {
			Debug.Log(string.Format("User's Information Fetch {0}.", userFetchSuccess ? "Successful" : "Failed"));
		});
	}

	public void SignOutButtonClicked()
	{
		if (GameJoltAPI.Instance.HasUser)
		{
			showTrophiesButton.interactable = false;
			GameJoltAPI.Instance.CurrentUser.SignOut();
		}
	}

	public void DownloadAvatar() {
		GameJoltAPI.Instance.CurrentUser.DownloadAvatar(success => Debug.LogFormat("Downloading avatar {0}", success ? "succeeded" : "failed"));
	}

	public void QueueNotification()
	{
		GameJoltUI.Instance.QueueNotification(
			string.Format("Notification <b>#{0}</b>", ++notificationQueued));
	}

	public void ShowLeaderboards() {
		GameJoltUI.Instance.ShowLeaderboards();
		// if you only want to show certain tables, you can provide them as additional arguments:
		// GameJolt.UI.Manager.Instance.ShowLeaderboards(null, null, 123, 456, 789, ...);
	}

	public void Pause()
	{
		Time.timeScale = 0f;
	}

	public void Resume()
	{
		Time.timeScale = 1f;
	}
}
