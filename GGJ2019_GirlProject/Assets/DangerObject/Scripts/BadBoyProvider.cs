using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadBoyProvider : Provider
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Girl"))
            DengerObjectManager.KillGirl(collision.gameObject.GetComponent<GirlProvider>());
    }
}
