using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickObjectController : MonoBehaviour
{
    [SerializeField] Collider gimmickCollider = null;

    private void Awake()
    {
        SetDummy();
    }

    public void SetDummy()
    {
        gimmickCollider.enabled = false;
    }

    public void Set()
    {
        gimmickCollider.enabled = true;
    }
}
