using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

	[SerializeField] public Panel panelGameOver;
	public PanelPause panelPause;

	public static UI Instance;

	private void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		panelGameOver.gameObject.SetActive(false);

		BonusManager.LoadData();
		if (BonusManager.isOpenBonus[0] == true)
		{
			Instantiate(Resources.Load<GameObject>("Flame Gauge"), transform);
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			panelPause.ShowPanelPause();
		}
	}
}
