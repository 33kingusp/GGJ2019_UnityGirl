using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 幼女の外部公開クラス
/// </summary>
public class GirlProvider : MonoBehaviour
{

    private GirlMover girlMover;

    // Start is called before the first frame update
    void Awake()
    {
        girlMover = GetComponent<GirlMover>();
    }

    public void Death()
    {
		Destroy(gameObject);
    }

    public void ReverseMoveDirection()
    {
        girlMover.ReverseSideMove();
    }

    public GirlMover.MoveState GetGirlMoveState()
    {
        return girlMover.CurrentMoveState;
    } 
    
}
