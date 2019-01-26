using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerObjectController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GirlProvider girl =  other.GetComponent<GirlProvider>();

        if (other != null)
        {
            
        }
    }

    protected virtual void KillGirl(GirlProvider girlProvider)
    {

    }
}
