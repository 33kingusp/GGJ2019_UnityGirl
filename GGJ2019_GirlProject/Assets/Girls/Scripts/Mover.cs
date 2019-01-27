using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    /// <summary>
    /// 現在坂の上かどうか
    /// </summary>
    private bool isSlope = false;

    [SerializeField]
    private float sideMoveSpeed = 5f;

    [SerializeField] private float fallSpeed = 5f;
    [SerializeField] private float groundFallSpped = 0.3f;

    private MoveDirectionState currentMoveDirectionState = MoveDirectionState.RIGHT;

    public MoveDirectionState CurrentMoveDirectionState { get => currentMoveDirectionState; }

    private Rigidbody myRigidbody;

    [SerializeField]
    private float groundRayDistance = 0.4f;

    [SerializeField]
    private float graoundRayOrigin;

    [SerializeField]
    private float groundRayBoxSize_Y = 0.05f;

    private SpriteRenderer spriteRenderer;

    private bool isRotate = false;

    private MoveState currentMoveState = MoveState.AUTO;

    public MoveState CurrentMoveState { get => currentMoveState; }

    [SerializeField]
    private AudioClip walkAudioClip;

    private AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        ChangeMoveDirectionState(MoveDirectionState.RIGHT);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3();

        GetGroundHorizontal();

        var groundObject = JudgeIsGround();
        // 接地していない場合
        //if (!groundObject)
        //{
        Debug.Log(IsGround());
        movement = FallMove(movement);
        //  return;
        //}
        // 壁に衝突したかどうかを取得し、衝突時動作をする
        if (IsGround() && JudgeReverseSide(gameObject.layer))
        {
            if (!isRotate)
            {
                ReverseSideMove();
                //return;
            }
        }
        // 回転中は移動しない
//        if (isRotate) { return; }
        if (IsGround() && !isRotate)
            switch (CurrentMoveState)
            {
                case MoveState.AUTO:
                    movement = SideMove(movement);
                    break;
                case MoveState.FORCE:
                    movement = SideMove(movement);
                    break;
                case MoveState.FEAR:
                    movement = SideMove(movement);
                    break;
            }
        // myRigidbody.MovePosition(transform.position + movement * Time.fixedDeltaTime);
        transform.position = transform.position + movement * Time.fixedDeltaTime;
    }

    private Vector3 SideMove(Vector3 movement)
    {
        //audioSource.PlayOneShot(walkAudioClip);

        //var movePos = transform.position;

        //movePos += GetMoveSideDirection() * sideMoveSpeed * Time.deltaTime;

        //myRigidbody.MovePosition(movePos);

        movement += GetMoveSideDirection() * sideMoveSpeed;
        return movement;
    }

    private bool JudgeReverseSide()
    {

        return JudgeReverseSide(gameObject.layer);
    }

    private bool JudgeReverseSide(int maskLayer)
    {
        Vector3 rayOrigin = transform.position;

        rayOrigin.y += transform.lossyScale.y / 2;

        // 現在の進んでいる方向にrayを設定する
        var ray = new Ray(rayOrigin, GetMoveSideDirection());

        // 通常の床のみ衝突判定を取る
        int layerMask = 1 << LayerMask.NameToLayer("Ground") | 1 << maskLayer;

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

    private Vector3 FallMove(Vector3 movement)
    {
        // var movePos = transform.position;
        // movePos.y -= fallSpeed * Time.fixedDeltaTime;
        // myRigidbody.MovePosition(movePos);
        if (IsGround())
        {
            movement.y = -groundFallSpped;
        }
        else
        {
            movement.y = -fallSpeed;
        }
        return movement;
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

    private bool IsGround()
    {
        return (JudgeIsGround() != null &&  JudgeIsGround().layer == LayerMask.NameToLayer("Ground"));
    }

    private GameObject JudgeIsGround()
    {
        // 自分の下の方にrayを飛ばす
     

        var ray = new Ray(transform.position + transform.up * 0.5f , -transform.up);

        // rayの距離を設定
        float rayDistance = groundRayDistance + 0.5f;

        var rayOrigin = transform.position;

        rayOrigin.y += groundRayDistance;

        Debug.DrawRay(transform.position, -transform.up * groundRayDistance, Color.red, 2f);

        // 通常の床のみ衝突判定を取る
        int layerMask = 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Slope");

        RaycastHit hit;

        Vector3 boxSize = transform.lossyScale * 0.4f;

        boxSize.y = groundRayBoxSize_Y;

        //var isHit = Physics.BoxCast(rayOrigin, boxSize, -transform.up, out hit, transform.rotation, rayDistance, layerMask);
        var isHit = Physics.SphereCast(rayOrigin, 0.8f, -transform.up, out hit, rayDistance * 0.5f, layerMask);

        if (isHit && hit.distance <= 0.2f)
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
        int layerMask = 1 << LayerMask.NameToLayer("Slope");

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

    public void ChangeMoveState(MoveState moveState, float time)
    {
        StartCoroutine(MoveStateCoroutine(moveState, time));
    }

    private IEnumerator MoveStateCoroutine(MoveState moveState, float time)
    {
        currentMoveState = moveState;
        yield return new WaitForSeconds(time);
        currentMoveState = MoveState.AUTO;
    }
    
    private Vector3 GetGroundHorizontal()
    {
        Vector3 rayOrigin = transform.position;

        rayOrigin.y += 0.25f;

        Debug.Log(rayOrigin);

        //自分の足元にRayを二本飛ばす

        var rayL = new Ray(rayOrigin + Vector3.left * 0.25f, Vector3.down);
        var rayR = new Ray(rayOrigin + Vector3.right * 0.25f, Vector3.down);


        // 通常の床のみ衝突判定を取る
        int layerMask = 1 << LayerMask.NameToLayer("Ground") | 1 << gameObject.layer;

        float rayDistance = (transform.lossyScale.x / 2) + 0.02f;

        RaycastHit hitL, hitR;
        Physics.Raycast(rayOrigin + Vector3.left, Vector3.down, 5f, layerMask,QueryTriggerInteraction.);

        Physics.Raycast(rayOrigin + Vector3.right, Vector3.down, layerMask, out hitL);

        return Vector3.zero;
    }
}
