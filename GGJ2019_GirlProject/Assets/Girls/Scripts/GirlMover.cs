using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class GirlMover : MonoBehaviour
{

    private bool isGround;

    [SerializeField]
    private float sideMoveSpeed = 5f;

    [SerializeField]
    private float fallSpeed = 5f;

    private MoveState currentMoveState = MoveState.RIGHT;

    private Rigidbody myRigidbody;

    private enum MoveState
    {
        RIGHT = 1,
        LEFT = -1
    }

    // Start is called before the first frame update
    void Awake()
    {
        currentMoveState = MoveState.RIGHT;
        //myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!JudgeIsGround())
        {
            FallMove();
            return;
        }

        ReverseSideMove();

        SideMove();
        
    }

    private void SideMove()
    {
        var movePos = transform.position;

        movePos.x += sideMoveSpeed * Time.deltaTime* (int)currentMoveState;

        transform.position = movePos;
    }

    private void ReverseSideMove()
    {
        // 現在の進んでいる方向にrayを設定する
        var ray = new Ray(transform.position, GetMoveSideDirection());

        float rayDistance = (transform.lossyScale.x/2)+0.1f;

        if (Physics.Raycast(ray, rayDistance))
        {
            switch (currentMoveState)
            {
                case MoveState.RIGHT:
                    currentMoveState = MoveState.LEFT;
                    break;

                case MoveState.LEFT:
                    currentMoveState = MoveState.RIGHT;
                    break;
            }
        }
    }

    private void ChangeMoveState(MoveState moveState)
    {
        
    }

    private void FallMove()
    {
        var movePos = transform.position;

        movePos.y -= fallSpeed * Time.deltaTime;

        transform.position = movePos;
    }

    private bool JudgeIsGround()
    {
        // 自分の下の方にrayを飛ばす
        var ray = new Ray(transform.position,-transform.up);

        // rayの距離を設定
        float rayDistance = (transform.lossyScale.y / 2);

        //RaycastHit hit;

        var rayOrigin = transform.position;

        rayOrigin.y += 0.1f;

        var isHit = Physics.BoxCast(rayOrigin, (transform.lossyScale)/2, -transform.up, transform.rotation);

        //Debug.Log(isHit);

        if (isHit)
        {
            return true;
        }

        return false;
    }

    private Vector3 GetMoveSideDirection()
    {
        return transform.right * (int)currentMoveState;
    }
}
