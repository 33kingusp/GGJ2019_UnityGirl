﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private PoleController startPole;
    private IEnumerator moveEnumerator = null;
    private bool isMoveing;

    private void Start()
    {
        startPole.StopCrow(this);
    }

    private void Update()
    {
        FindGirl();
    }

    private void FindGirl()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.SphereCast(transform.position, 1.0f, Vector3.down, out hit))
        {
            GirlProvider girl = hit.collider.GetComponent<GirlProvider>();
            if (girl)
            {
                // != girl.GetGirlMoveState();
                girl.ReverseMoveDirection();
            }
        }
    }

    public void MoveToPole(PoleController pole, float delay)
    {
        if (isMoveing)
            StopCoroutine(moveEnumerator);

        moveEnumerator = MovePositon(pole, delay);
        StartCoroutine(moveEnumerator);
    }

    private IEnumerator MovePositon(PoleController pole, float delay)
    {
        isMoveing = true;
        animator.SetBool("isFrying", true);
        Vector3 oldPosition = transform.position;
        Vector3 position = pole.transform.position + Vector3.up * 4;

        if (position.x - oldPosition.x <= 0) transform.eulerAngles = Vector3.zero;
        else transform.eulerAngles = Vector3.up * 180;

        float t = 0;

        do
        {
            transform.position = Vector3.Lerp(oldPosition, position, t / delay);
            t += Time.deltaTime;
            yield return null;
        }
        while (t <= delay) ;
        transform.position = position;
        isMoveing = false;
        animator.SetBool("isFrying", false);
        pole.StopCrow(this);
        yield break;
    }
}
