using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTest : MonoBehaviour
{

 	void Death()
	{
		StageManager.Instance.IncDeleteGirlCount();
	}

    private void OnTriggerEnter(Collider collider)
    {
        var gameObject = collider.gameObject;

        // FIXME 女の子のレイヤー番号が10。どこかに定数で持ちたい
        if (gameObject.layer == LayerMask.NameToLayer("Girl"))
        {
            var provider = gameObject.GetComponent<Provider>();
            provider.Death();
            Death();
        }
        else if (gameObject.layer == LayerMask.NameToLayer("BadBoy"))
        {
            var provider = gameObject.GetComponent<Provider>();
            provider.Death();
        }
    }
}
