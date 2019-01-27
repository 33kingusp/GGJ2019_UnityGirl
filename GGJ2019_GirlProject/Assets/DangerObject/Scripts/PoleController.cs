using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleController : DangerObjectController
{
    [SerializeField] private PoleController nextPole;
    [SerializeField] private CrowController crow;
    private bool isStopedCrow;

    private void Update()
    {
        if (isStopedCrow && Input.GetMouseButtonDown(0))
            MoveCrow();
    }

    public void MoveCrow()
    {
        isStopedCrow = false;
        crow.MoveToPole(nextPole, 3f);
    }

    public void StopCrow(CrowController crow)
    {
        this.crow = crow;
        isStopedCrow = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if(isStopedCrow && other.gameObject.layer == LayerMask.NameToLayer("Girl"))
        {
            DangerObjectManager.FearMover(other.gameObject, gameObject);
        }
    } 
}