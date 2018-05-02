using GameJolt.UI.Objects;
using UnityEngine;
using UnityEngine.UI;

namespace GameJolt.UI.Controllers
{
	public class NotificationItem : MonoBehaviour
	{
		public Image image;
		public Text text;

		public void Init(Notification notification)
		{
			text.text = notification.Text;

			if (notification.Image != null)
			{
				image.sprite = notification.Image;
				image.enabled = true;
			}
			else
			{
				image.enabled = false;
			}
		}
	}
}