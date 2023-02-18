using TMPro;

public class EnergyManager : Singleton<EnergyManager>
{
	#region Fields

	private int currentEnergy;

	private TextMeshProUGUI currentEnergyText;

	#endregion

	#region Properties

	public int CurrentEnergy { get => currentEnergy; }

	public int MaxEnergy { get; set; }

	#endregion

	#region Events

	protected override void Awake()
	{
		base.Awake();

		currentEnergyText = WorldCanvasManager.BookCanvasRight.transform.Find("EndTurn").Find("Energy").GetComponent<TextMeshProUGUI>();
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

			NotificationManager.AddNotification(err, NotificationType.Error);
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