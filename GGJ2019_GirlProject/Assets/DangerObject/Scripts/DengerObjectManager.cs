using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DengerObjectManager
{
    public static void KillGirl(GirlProvider girlProvider)
    {
        girlProvider.Death();
        StageManager.Instance.IncDeleteGirlCount();
    }
}
