using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTest : MonoBehaviour
{
	[SerializeField]
	StageManager stageManager;

 	void Death()
	{
		stageManager.IncDeleteGirlCount();
	}

	private void OnTriggerEnter(Collider collider)
	{
		var gameObject = collider.gameObject;

		// FIXME 女の子のレイヤー番号が10。どこかに定数で持ちたい
		if (gameObject.layer == 10)
		{
			var girlProvider = gameObject.GetComponent<GirlProvider>();
			girlProvider.Death();
			Death();
		}
	}

}
