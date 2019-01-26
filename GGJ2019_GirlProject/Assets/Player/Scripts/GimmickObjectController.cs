using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickObjectController : MonoBehaviour
{
    [SerializeField] Vector2 gimmickSize = new Vector2(1, 1);
    [SerializeField] Collider gimmickCollider = null;
    private bool isDummy = false;

    private void Awake()
    {
        SetDummy();
    }

    public void SetDummy()
    {
        isDummy = true;
        gimmickCollider.enabled = false;
    }

    public bool Set()
    {
        for(int y = 0; y < gimmickSize.y; y++)
            for (int x = 0; x < gimmickSize.x; x++)
                if (GridManager.IsEmptyGrid(transform.position + Vector3.right * x + Vector3.down * y)) return false;

        isDummy = false;
        gimmickCollider.enabled = true;
        return true;
    }
    
}
