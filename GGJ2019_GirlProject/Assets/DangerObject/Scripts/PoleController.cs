using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleController : DangerObjectController
{
    [SerializeField] private PoleController nextPole;
    [SerializeField] private CrowController crow;

    public void MoveCrow()
    {
        crow.MoveToPole(nextPole);
    }
}