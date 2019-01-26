﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 幼女の外部公開クラス
/// </summary>
public class GirlProvider : MonoBehaviour
{

    private GirlMover girlMover;

    [SerializeField]
    private float deathTimer = 1f;

    private Collider myCollider;

    // Start is called before the first frame update
    void Awake()
    {
        girlMover = GetComponent<GirlMover>();
        myCollider = GetComponent<Collider>();
    }

    public void Death()
    {
        girlMover.enabled = false;

        myCollider.enabled = false;

        // TODO: アニメーション関連の処理を追加

        Destroy(gameObject, deathTimer);
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
