using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class GirlMover : MonoBehaviour
{

    /// <summary>
    /// 現在坂の上かどうか
    /// </summary>
    private bool isSlope = false;

    [SerializeField]
    private float sideMoveSpeed = 5f;

    [SerializeField]
    private float fallSpeed = 5f;

    private MoveState currentMoveState = MoveState.RIGHT;

    private Rigidbody myRigidbody;

    [SerializeField]
    private float boxCastRayMargine = 0.4f;

    private int slopeLayer = 9;

    private int groundLayer = 8;

    private int girlLayer = 10;

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
        // 坂の動きをする
        if (MoveSlope())
        {
            
        }
        else
        {
            var groundObject = JudgeIsGround();
            // 接地していない場合
            if (!groundObject)
            {
                Rotate(Quaternion.identity);
                FallMove();
                return;
            }
        }

        // 壁に衝突したかどうかを取得し、衝突時動作をする
        ReverseSideMove();

        SideMove();

    }

    private void SideMove()
    {
        var movePos = transform.position;

        movePos += GetMoveSideDirection() * sideMoveSpeed * Time.deltaTime;

        transform.position = movePos;
    }

    private void ReverseSideMove()
    {
        // 現在の進んでいる方向にrayを設定する
        var ray = new Ray(transform.position, GetMoveSideDirection());

        // 通常の床のみ衝突判定を取る
        int layerMask = 1 << groundLayer | 1 << girlLayer;

        float rayDistance = (transform.lossyScale.x / 2) + 0.01f;

        if (Physics.Raycast(ray, rayDistance, layerMask))
        {
            switch (currentMoveState)
            {
                case MoveState.RIGHT:
                    ChangeMoveState(MoveState.LEFT);
                    break;

                case MoveState.LEFT:
                    ChangeMoveState(MoveState.RIGHT);
                    break;
            }
        }
    }

    private void ChangeMoveState(MoveState moveState)
    {
        currentMoveState = moveState;
    }

    private void FallMove()
    {
        var movePos = transform.position;

        movePos.y -= fallSpeed * Time.deltaTime;

        transform.position = movePos;
    }

    private void Rotate(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    private bool MoveSlope()
    {
        // 坂を取得する
        var slopeObject = JudgeIsSlope();
        // 坂だった場合
        if (slopeObject)
        {
            isSlope = true;
            Rotate(slopeObject.transform.rotation);

            //Vector3 movepos = transform.position;
            //movepos.y = slopeObject.transform.position.y + (transform.lossyScale.y / 2f) + (slopeObject.transform.lossyScale.y / 2f);

            //transform.position = movepos;
        }
        else
        {
            isSlope = false;
        }
        return isSlope;
    }

    private GameObject JudgeIsGround()
    {
        // 自分の下の方にrayを飛ばす
        var ray = new Ray(transform.position, -transform.up);

        // rayの距離を設定
        float rayDistance = (transform.lossyScale.y * 0.5f) - boxCastRayMargine;

        var rayOrigin = transform.position;

        rayOrigin.y += 0.3f;

        // 通常の床のみ衝突判定を取る
        int layerMask = 1 << groundLayer;

        RaycastHit hit;

        var isHit = Physics.BoxCast(rayOrigin, (transform.lossyScale) / 2f, -transform.up, out hit, transform.rotation, rayDistance, layerMask);

        if (isHit)
        {
            return hit.collider.gameObject;
        }

        return null;
    }

    private GameObject JudgeIsSlope()
    {
        // 自分の下の方にrayを飛ばす
        var ray = new Ray(transform.position, -transform.up);

        // rayの距離を設定
        float rayDistance = (transform.lossyScale.y * 0.5f);

        RaycastHit hit;

        var rayOrigin = transform.position;

        // 坂のみ衝突判定を取る
        int layerMask = 1 << slopeLayer;

        rayOrigin.y += 0.1f;

        var isHit = Physics.BoxCast(rayOrigin, (transform.lossyScale) *0.5f, -transform.up, out hit, transform.rotation, rayDistance, layerMask);

        if (isHit)
        {
            Debug.Log(hit.collider.gameObject);
            return hit.collider.gameObject;
        }

        return null;
    }

    private Vector3 GetMoveSideDirection()
    {
        return transform.right * (int)currentMoveState;
    }
}
