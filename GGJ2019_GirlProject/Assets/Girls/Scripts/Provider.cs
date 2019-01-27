using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Provider : MonoBehaviour
{
    private Mover mover;

    [SerializeField]
    private float deathTimer = 1f;

    private Collider myCollider;

    [SerializeField]
    private AudioClip deathAudioClip;

    private AudioSource audioSource;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        mover = GetComponent<Mover>();
        myCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Death()
    {
        mover.enabled = false;

        myCollider.enabled = false;

        audioSource.PlayOneShot(deathAudioClip);
        // TODO: アニメーション関連の処理を追加

        Destroy(gameObject, deathTimer);
    }

    public void ReverseMoveDirection()
    {
        mover.ReverseSideMove();
    }

    public Mover.MoveDirectionState GetGirlMoveDirectionState()
    {
        return mover.CurrentMoveDirectionState;
    }

    public void SetMoveDirectioState(Mover.MoveDirectionState state)
    {
        if(mover.CurrentMoveDirectionState != state)
            mover.ReverseSideMove();
    }

    public Mover.MoveState GetGirlMoveState()
    {
        return mover.CurrentMoveState;
    }



    public void Fear(float time)
    {

    }
}
