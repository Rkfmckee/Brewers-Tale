using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
	#region Properties

	[SerializeField]
	private Tab defaultTab;

	private List<Tab> tabs;

	#endregion

	#region Events

	private void Start()
	{
		foreach (var tab in tabs)
		{
			var button = tab.GetComponent<Button>();
			button.onClick.AddListener(() => ShowPage(tab));
		}

		ShowPage(defaultTab);
	}

	#endregion

	#region Methods

	public void Subscribe(Tab tab)
	{
		if (tabs == null) tabs = new List<Tab>();
		tabs.Add(tab);
	}

	private void ShowPage(Tab tab)
	{
		HideAllPages();
		tab.Page.SetActive(true);
		tab.IsActive = true;
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
