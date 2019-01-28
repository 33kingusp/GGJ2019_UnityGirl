using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DangerObjectManager
{
    public static void KillGirl(GirlProvider girlProvider)
    {
        girlProvider.Death();
        StageManager.Instance.IncDeleteGirlCount();
    }

    public static void ReverseMover(GameObject gameObject)
    {
        Provider provider = gameObject.GetComponent<Provider>();
        if (provider == null) return;
        provider.ReverseMoveDirection();
    }

    public static void FearMover(GameObject mover, GameObject dangerObjrct)
    {
        Provider provider = mover.GetComponent<Provider>();
        if (provider == null) return;

        if (provider.transform.position.x - dangerObjrct.transform.position.x >= 0)
            provider.SetMoveDirectioState(Mover.MoveDirectionState.LEFT);
        else
            provider.SetMoveDirectioState(Mover.MoveDirectionState.RIGHT);

        provider.ReverseMoveDirection();
    }
}
