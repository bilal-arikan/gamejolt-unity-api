using GameJolt.API;
using UnityEngine;
using UnityEngine.UI;

public class RestrictedButton : MonoBehaviour {
	public bool RequiresSignedInUser = true;
	private Button button;

	private void Start() {
		button = GetComponent<Button>();
	}

	private void Update() {
		button.interactable = Manager.Instance.HasSignedInUser == RequiresSignedInUser;
	}
}
