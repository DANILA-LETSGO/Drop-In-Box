using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
	public Spawn spawn;
	public Spawn spawn_2;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.name == "Ball")
		{
			Destroy(col.gameObject);
			UI.Instance.panelGameOver.ShowPanel();
			spawn.HideSpawn();
			if (spawn_2 != null)
			{
				spawn_2.HideSpawn();
			}

		}
	}
}
