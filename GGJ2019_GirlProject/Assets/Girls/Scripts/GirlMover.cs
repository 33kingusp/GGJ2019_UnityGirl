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

    private MoveDirectionState currentMoveDirectionState = MoveDirectionState.RIGHT;

    public MoveDirectionState CurrentMoveDirectionState { get => currentMoveDirectionState; }
    

    private Rigidbody myRigidbody;

    [SerializeField]
    private float groundRayDistance = 0.4f;

    [SerializeField]
    private float graoundRayOrigin;

    [SerializeField]
    private float groundRayBoxSize_Y = 0.05f;

    private int slopeLayer = 9;

    private int groundLayer = 8;

    private const int girlLayer = 10;

    private SpriteRenderer spriteRenderer;

    private bool isRotate = false;

    [SerializeField]
    private int sideMoveMaskLayer = girlLayer;

    private MoveState currentMoveState = MoveState.AUTO;

    public MoveState CurrentMoveState { get => currentMoveState; }

    public enum MoveState
    {
        AUTO,
        FORCE,
        FEAR
    }

    public enum MoveDirectionState
    {
        RIGHT = 1,
        LEFT = -1
    }

    // Start is called before the first frame update
    void Awake()
    {

        myRigidbody = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ChangeMoveDirectionState(MoveDirectionState.RIGHT);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        var groundObject = JudgeIsGround();
        // 接地していない場合
        if (!groundObject)
        {
            FallMove();
            return;
        }

        // 壁に衝突したかどうかを取得し、衝突時動作をする
        if (JudgeReverseSide(sideMoveMaskLayer))
        {
            if (!isRotate)
            {
                ReverseSideMove();
                return;
            }

        }

        // 回転中は移動しない
        if (isRotate) { return; }
        switch (CurrentMoveState)
        {
            case MoveState.AUTO:
                SideMove();
                break;
            case MoveState.FORCE:
                SideMove();
                break;
            case MoveState.FEAR:
                SideMove();
                break;
        }

    }

    private void SideMove()
    {
        var movePos = transform.position;

        movePos += GetMoveSideDirection() * sideMoveSpeed * Time.deltaTime;

        myRigidbody.MovePosition(movePos);
    }

    private bool JudgeReverseSide()
    {

        return JudgeReverseSide(girlLayer);
    }

    private bool JudgeReverseSide(int maskLayer)
    {
        Vector3 rayOrigin = transform.position;

        rayOrigin.y += transform.lossyScale.y / 2;

        // 現在の進んでいる方向にrayを設定する
        var ray = new Ray(rayOrigin, GetMoveSideDirection());

        // 通常の床のみ衝突判定を取る
        int layerMask = 1 << groundLayer | 1 << maskLayer;

        float rayDistance = (transform.lossyScale.x / 2) + 0.02f;

        RaycastHit hit;

        return Physics.Raycast(ray.origin, ray.direction, out hit, rayDistance, layerMask);
    }

    public void ReverseSideMove()
    {
        switch (CurrentMoveDirectionState)
        {
            case MoveDirectionState.RIGHT:
                ChangeMoveDirectionState(MoveDirectionState.LEFT);
                break;

            case MoveDirectionState.LEFT:
                ChangeMoveDirectionState(MoveDirectionState.RIGHT);
                break;
        }
    }

    private void ChangeMoveDirectionState(MoveDirectionState moveState)
    {
        isRotate = true;
        currentMoveDirectionState = moveState;
        spriteRenderer.flipX = !spriteRenderer.flipX;
        isRotate = false;
    }

    private void FallMove()
    {
        var movePos = transform.position;

        movePos.y -= fallSpeed * Time.fixedDeltaTime;

        myRigidbody.MovePosition(movePos);
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
        float rayDistance = groundRayDistance;

        var rayOrigin = transform.position;

        rayOrigin.y += groundRayDistance;

        // 通常の床のみ衝突判定を取る
        int layerMask = 1 << groundLayer | 1 << slopeLayer;

        RaycastHit hit;

        Vector3 boxSize = transform.lossyScale *0.4f;

        boxSize.y = groundRayBoxSize_Y;

        var isHit = Physics.BoxCast(rayOrigin, boxSize, -transform.up, out hit, transform.rotation, rayDistance, layerMask);

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

        var isHit = Physics.BoxCast(rayOrigin, (transform.lossyScale) * 0.5f, -transform.up, out hit, transform.rotation, rayDistance, layerMask);

        if (isHit)
        {
            //Debug.Log(hit.collider.gameObject);
            return hit.collider.gameObject;
        }

        return null;
    }

    private Vector3 GetMoveSideDirection()
    {
        return transform.right * (int)CurrentMoveDirectionState;
    }

    public void ChangeMoveState(MoveState moveState,float time)
    {
        StartCoroutine(MoveStateCoroutine(moveState, time));
    }

    private IEnumerator MoveStateCoroutine(MoveState moveState, float time)
    {
        currentMoveState = moveState;
        yield return new WaitForSeconds(time);
        currentMoveState = MoveState.AUTO;
    }
}
