using UnityEngine.UI;
using System;
using UnityEngine;

namespace GameJolt.UI.Controllers {
	public class SignInWindow : BaseWindow {
		public InputField UsernameField;
		public InputField TokenField;
		public Text ErrorMessage;
		public Toggle RememberMeToggle;
		public Toggle ShowTokenToggle;

		private Action<bool> signedInCallback;
		private Action<bool> userFetchedCallback;

		public override void Show(Action<bool> callback) {
			Show(callback, null);
		}

		public void Show(Action<bool> signedInCallback, Action<bool> userFetchedCallback) {
			ErrorMessage.enabled = false;
			animator.SetTrigger("SignIn");
			this.signedInCallback = signedInCallback;
			this.userFetchedCallback = userFetchedCallback;
			string username, token;
			RememberMeToggle.isOn = API.GameJoltAPI.Instance.GetStoredUserCredentials(out username, out token);
			UsernameField.text = username;
			TokenField.text = token;
			ShowTokenToggle.isOn = false;
		}

		public override void Dismiss(bool success) {
			animator.SetTrigger("Dismiss");
			if(signedInCallback != null) {
				signedInCallback(success);
				signedInCallback = null;
			}
		}

		public void Submit() {
			ErrorMessage.enabled = false;

			if(UsernameField.text.Trim() == string.Empty || TokenField.text.Trim() == string.Empty) {
				ErrorMessage.text = "Empty username and/or token.";
				ErrorMessage.enabled = true;
			} else {
				animator.SetTrigger("Lock");
				animator.SetTrigger("ShowLoadingIndicator");

				var user = new API.Objects.User(UsernameField.text.Trim(), TokenField.text.Trim());
				user.SignIn(signInSuccess => {
					if(signInSuccess) {
						Dismiss(true);
					} else {
						// Technically this could be because of another user being already signed in.
						ErrorMessage.text = "Wrong username and/or token.";
						ErrorMessage.enabled = true;
					}

					animator.SetTrigger("HideLoadingIndicator");
					animator.SetTrigger("Unlock");
				}, userFetchedSuccess => {
					if(userFetchedCallback != null) {
						// This will potentially be called after a user dismissed the window..
						userFetchedCallback(userFetchedSuccess);
						userFetchedCallback = null;
					}
				}, RememberMeToggle.isOn);
			}
		}

		public void ShowToken(bool show) {
			TokenField.contentType = show ? InputField.ContentType.Standard : InputField.ContentType.Password;
			TokenField.ActivateInputField();
		}

		public void CreateAccount() {
			Application.OpenURL("https://gamejolt.com/join");
		}
	}
}