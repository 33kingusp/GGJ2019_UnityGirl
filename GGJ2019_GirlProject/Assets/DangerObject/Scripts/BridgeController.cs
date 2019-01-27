using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    [SerializeField] private int breakLimit = 1;

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exit" + collision);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Girl") || collision.gameObject.layer == LayerMask.NameToLayer("BadBoy"))
        {
            breakLimit--;
            if (breakLimit <= 0) Break();
        }
    }

    private void Break()
    {
        Destroy(gameObject);
    }
}