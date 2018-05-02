using UnityEngine;
using System;
using System.Linq;
using GameJolt.API;

namespace GameJolt.UI.Controllers
{
	public class TrophiesWindow: BaseWindow
	{
		public RectTransform container;
		public GameObject trophyItem;

		Action<bool> callback;

		public override void Show(Action<bool> callback)
		{
			animator.SetTrigger("Trophies");
			animator.SetTrigger("ShowLoadingIndicator");
			this.callback = callback;

			Trophies.Get(trophies => {
				if (trophies != null)
				{
					trophies = trophies.Where(x => !x.IsSecret || x.Unlocked).ToArray();
					// Create children if there are not enough
					while(container.childCount < trophies.Length) {
						var tr = Instantiate(trophyItem).transform;
						tr.SetParent(container);
						tr.SetAsLastSibling();
					}

					// Update children's text.
					for (int i = 0; i < trophies.Length; ++i)
					{
						container.GetChild(i).GetComponent<TrophyItem>().Init(trophies[i]);
					}

					animator.SetTrigger("HideLoadingIndicator");
					animator.SetTrigger("Unlock");
				}
				else
				{
					// TODO: Show error notification
					animator.SetTrigger("HideLoadingIndicator");
					Dismiss(false);
				}
			});
		}
		
		public override void Dismiss(bool success)
		{
			animator.SetTrigger("Dismiss");
			if (callback != null)
			{
				callback(success);
				callback = null;
			}
		}
	}
}