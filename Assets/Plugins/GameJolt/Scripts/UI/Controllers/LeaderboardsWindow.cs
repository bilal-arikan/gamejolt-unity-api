using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using GameJolt.API;
using GameJolt.API.Objects;

namespace GameJolt.UI.Controllers
{
	public class LeaderboardsWindow: BaseWindow
	{
		public RectTransform tabsContainer;
		public GameObject tableButton;

		public ScrollRect scoresScrollRect;
		public GameObject scoreItem;
		
		Action<bool> callback;

		int[] tableIDs;
		int currentTab;
		
		public override void Show(Action<bool> callback) {
			Show(callback, null, null);
		}

		public void Show(Action<bool> callback, int? activeTable, int[] visibleTables) {
			animator.SetTrigger("Leaderboards");
			animator.SetTrigger("ShowLoadingIndicator");
			this.callback = callback;

			Scores.GetTables(tables => {
				// preprocess tables to match the visible tables provided by the user
				if(tables != null && visibleTables != null && visibleTables.Length > 0) {
					tables = tables.Where(x => visibleTables.Contains(x.ID)).ToArray();
				}
				if(tables != null && tables.Length > 0) {
					// Create the right number of children.
					Populate(tabsContainer, tableButton, tables.Length);
					int activeId = GetActiveTableId(tables, activeTable);

					// Update children's text. 
					tableIDs = new int[tables.Length];
					for(int i = 0; i < tables.Length; ++i) {
						tabsContainer.GetChild(i).GetComponent<TableButton>().Init(tables[i], i, this, tables[i].ID == activeId);

						// Keep IDs information and current tab for use when switching tabs.
						tableIDs[i] = tables[i].ID;
						if(tables[i].ID == activeId) {
							currentTab = i;
						}
					}

					SetScores(activeId);
				} else {
					// TODO: Show error notification
					animator.SetTrigger("HideLoadingIndicator");
					Dismiss(false);
				}
			});
		}

		private int GetActiveTableId(Table[] tables, int? activeTable) {
			if(activeTable.HasValue && tables.Any(x => x.ID == activeTable.Value)) // try to use the provided table id
				return activeTable.Value;
			// try to find the primary table
			var primary = tables.FirstOrDefault(x => x.Primary);
			if(primary != null) return primary.ID;
			// the first table is used as a fallback
			return tables[0].ID;
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

		public void ShowTab(int index)
		{
			// There is no need to set the new tab button active, it has been done internally when the button has been clicked.
			tabsContainer.GetChild(currentTab).GetComponent<TableButton>().SetActive(false);
			currentTab = index;

			animator.SetTrigger("Lock");
			animator.SetTrigger("ShowLoadingIndicator");

			// Request new scores.
			SetScores(tableIDs[currentTab]);
		}

		void SetScores(int tableID)
		{
			Scores.Get(scores => {
				if (scores != null)
				{
					scoresScrollRect.verticalNormalizedPosition = 0;

					// Create the right number of children.
					Populate(scoresScrollRect.content, scoreItem, scores.Length);
					
					// Update children's text.
					for (int i = 0; i < scores.Length; ++i)
					{
						scoresScrollRect.content.GetChild(i).GetComponent<ScoreItem>().Init(scores[i]);
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
			}, tableID, 50);
		}
	}
}