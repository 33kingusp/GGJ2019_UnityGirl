using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerObjectController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        /*
        Debug.Log("Collision");
        GirlProvider girl = other.gameObject.GetComponent<GirlProvider>();

        if (girl != null)
            KillGirl(girl);
            */
    }

    protected virtual void KillGirl(GirlProvider girlProvider)
    {
        Debug.Log("KillGirl : " + girlProvider);
        girlProvider.Death();
    }
}
