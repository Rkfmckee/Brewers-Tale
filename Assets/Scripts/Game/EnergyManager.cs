using TMPro;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
	#region Fields

	private static EnergyManager instance;
	private int currentEnergy;

	private TextMeshProUGUI currentEnergyText;

	#endregion

	#region Properties

	public static EnergyManager Instance
	{
		get
		{
			if (instance == null)
				instance = Instantiate(Resources.Load<EnergyManager>("Prefabs/Game/EnergyManager"),
				GameObject.Find("GameControllers").transform);

			return instance;
		}
	}

	public int CurrentEnergy { get => currentEnergy; }

	public int MaxEnergy { get; set; }

	#endregion

	#region Events

	private void Start()
	{
		currentEnergyText = OverlayCanvasManager.Canvas.transform.Find("Energy").Find("EnergyLevel").GetComponent<TextMeshProUGUI>();
		MaxEnergy = 3;
	}

	#endregion

	#region Methods

	#region Static methods

	public static bool HaveEnoughEnergy(int energy, string error, bool shouldUse = true)
	{
		if (!EnergyManager.Instance.HaveEnough(energy))
		{
			var err = "Not enough energy";
			if (!string.IsNullOrEmpty(error)) err += $" to {error}";

			NotificationManager.Add(err, NotificationType.Error);
			return false;
		}

		if (shouldUse) UseEnergy(energy);
		return true;
	}

	public static void UseEnergy(int energy)
	{
		EnergyManager.Instance.Use(energy);
	}

	public static void AddEnergy(int energy)
	{
		EnergyManager.Instance.Add(energy);
	}

	public static void ResetEnergy()
	{
		EnergyManager.Instance.Reset();
	}

	#endregion

	private bool HaveEnough(int energy)
	{
		return CurrentEnergy >= energy;
	}

	private void Use(int energy)
	{
		Set(currentEnergy - energy);
	}

	private void Add(int energy)
	{
		Set(currentEnergy + energy);
	}

	private void Reset()
	{
		Set(MaxEnergy);
	}

	private void Set(int energy)
	{
		currentEnergy = energy;
		currentEnergyText.SetText(energy.ToString());
	}

	#endregion
}