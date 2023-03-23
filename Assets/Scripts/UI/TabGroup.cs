using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
	#region Fields

	[SerializeField]
	private Tab defaultTab;
	[SerializeField]
	private Tab creatureTab;

	private Tab currentTab;
	private Tab beforeCreatureTab;
	private List<Tab> tabs;

	#endregion

	#region Events

	private void Awake()
	{
		References.TabGroup = this;
	}

	private void Start()
	{
		foreach (var tab in tabs)
		{
			var button = tab.GetComponent<Button>();
			button.onClick.AddListener(() => ShowPage(tab));
		}

		ShowPage(defaultTab);
		ShowCreatureTab(false);
	}

	#endregion

	#region Methods

	public void Subscribe(Tab tab)
	{
		if (tabs == null) tabs = new List<Tab>();
		tabs.Add(tab);
	}

	public void ShowCreatureTab(bool show)
	{
		creatureTab.gameObject.SetActive(show);
		var tabToShow = show ? creatureTab : beforeCreatureTab;
		beforeCreatureTab = show ? currentTab : null;

		ShowPage(tabToShow);
	}

	private void ShowPage(Tab tab)
	{
		if (tab == null) return;

		HideAllPages();

		tab.Page.SetActive(true);
		tab.IsActive = true;
		currentTab = tab;
	}

	private void HideAllPages()
	{
		foreach (var tab in tabs)
		{
			tab.Page.SetActive(false);
			tab.IsActive = false;
		}
	}

	#endregion
}
