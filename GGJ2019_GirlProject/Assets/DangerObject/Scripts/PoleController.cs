using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleController : DangerObjectController
{
    [SerializeField] private PoleController nextPole;
    [SerializeField] private CrowController crow;

    public void MoveCrow()
    {
        crow.Move(nextPole.transform.position + Vector3.up * 4, 5f);
    }
}