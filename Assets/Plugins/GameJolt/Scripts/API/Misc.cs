using UnityEngine;
using System;
using System.Collections.Generic;

namespace GameJolt.API
{
	/// <summary>
	/// Misc API methods.
	/// </summary>
	public static class Misc
	{
		private static readonly Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();
		private static readonly Dictionary<string, Action<Sprite>> openRequests = new Dictionary<string, Action<Sprite>>();

		/// <summary>
		/// Destroys all sprites in the spritecache.
		/// </summary>
		public static void ClearSpriteCache() {
			foreach(var sprite in spriteCache.Values) {
				UnityEngine.Object.Destroy(sprite);
			}
			spriteCache.Clear();
		}

		/// <summary>
		/// Downloads an image.
		/// </summary>
		/// <param name="url">The image URL.</param>
		/// <param name="callback">A callback function accepting a single parameter, a UnityEngine.Sprite.</param>
		public static void DownloadImage(string url, Action<Sprite> callback) {
			// check if the sprite is already cached
			Sprite cachedSprite;
			if(spriteCache.TryGetValue(url, out cachedSprite)) {
				if(cachedSprite) { // check if sprite is not destroyed
					if(callback != null) callback(cachedSprite);
					return;
				}
				// sprite was cached, but it is already destroyed
				spriteCache.Remove(url);
			}

			// check if there is already an open request
			Action<Sprite> cachedMulticast;
			if(callback != null && openRequests.TryGetValue(url, out cachedMulticast)) {
				// there is already a request for that image
				openRequests[url] = cachedMulticast + callback; // add callback to multicast
			} else {
				// no open request -> initiate one
				openRequests[url] = callback;
				Manager.Instance.StartCoroutine(Manager.Instance.GetRequest(url, Core.ResponseFormat.Texture, response => {
					Sprite sprite = null;
					if(response.success) {
						sprite = Sprite.Create(
							response.texture,
							new Rect(0, 0, response.texture.width, response.texture.height),
							new Vector2(.5f, .5f),
							response.texture.width);
						spriteCache[url] = sprite;
					}

					Action<Sprite> multicast;
					if(openRequests.TryGetValue(url, out multicast)) {
						openRequests.Remove(url);
						multicast(sprite);
					}
				}));
			}
		}

		/// <summary>
		/// Get the server time.
		/// </summary>
		/// <param name="callback">A callback function accepting a single parameter, a System.DateTime.</param>
		public static void GetTime(Action<DateTime> callback)
		{
			Core.Request.Get(Constants.API_TIME_GET, null, response => {
				if (callback != null)
				{
					double timestamp = response.json["timestamp"].AsDouble;
					DateTime serverTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified);
					serverTime = serverTime.AddSeconds(timestamp);
					callback(serverTime);
				}
			}, false);
		}
	}
}