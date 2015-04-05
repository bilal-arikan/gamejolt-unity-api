using System;
using System.Collections.Generic;

namespace GJAPI
{
	public static class Users
	{
		#region Authenticate
		public static void Authenticate(string name, string token, Action<bool> callback = null)
		{
			Manager.Instance.CurrentUser = new Objects.User(name, token);
			Manager.Instance.CurrentUser.Authenticate(callback);
		}
		#endregion Authenticate

		#region Get
		public static void Get(string name, Action<Objects.User> callback)
		{
			var user = new Objects.User(name, string.Empty);
			Get(user, callback);
		}

		public static void Get(int id, Action<Objects.User> callback)
		{
			var user = new Objects.User(id);
			Get(user, callback);
		}

		public static void Get(Objects.User user, Action<Objects.User> callback)
		{
			var parameters = new Dictionary<string, string>();
			if(user.Name != null && user.Name != string.Empty)
			{
				parameters.Add ("username", user.Name);
			}
			else if (user.ID != 0)
			{
				parameters.Add ("user_id", user.ID.ToString());
			}

			Core.Request.Get(Constants.API_USERS_FETCH, parameters, (Core.Response response) => {
				if(response.success)
				{
					user.BulkUpdate(response.json["users"][0].AsObject);
				}
				else
				{
					user = null;
				}
				
				callback(user);
			}, false);
		}

		public static void Get(int[] ids, Action<Objects.User[]> callback)
		{
			var parameters = new Dictionary<string, string>();
			parameters.Add(Constants.API_USERS_FETCH, string.Join(",", Array.ConvertAll(ids, id => id.ToString())));

			Core.Request.Get("users/", parameters, (Core.Response response) => {
				Objects.User[] users;
				if(response.success)
				{
					int count = response.json["users"].AsArray.Count;
					users = new Objects.User[count];

					for (int i = 0; i < count; ++i)
					{
						users[i] = new Objects.User(response.json["users"][i].AsObject);
					}
				}
				else
				{
					users = null;
				}
				
				callback(users);
			}, false);
		}
		#endregion Get
	}
}