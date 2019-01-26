using System.Collections;
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

    [SerializeField]
    private AudioClip deathAudioClip;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        girlMover = GetComponent<GirlMover>();
        myCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Death()
    {
        girlMover.enabled = false;

        myCollider.enabled = false;

        audioSource.PlayOneShot(deathAudioClip);
        // TODO: アニメーション関連の処理を追加

        Destroy(gameObject, deathTimer);
    }

    public void ReverseMoveDirection()
    {
        girlMover.ReverseSideMove();
    }

    public GirlMover.MoveDirectionState GetGirlMoveDirectionState()
    {
        return girlMover.CurrentMoveDirectionState;
    } 

    public GirlMover.MoveState GetGirlMoveState()
    {
        return girlMover.CurrentMoveState;
    }

    public void Fear(float time)
    {

    }
    
}
