using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManagerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ReadAndWriteTest();
    }

    private void ReadAndWriteTest()
    {
        RecordManager.Instance.SaveRecord(0, 3, 3, 20f);
        RecordManager.Instance.SaveRecord(1, 2, 1, 44f);
        var rec0 = RecordManager.Instance.GetRecord(0);
        Debug.Log("ステージID: 0");
        Debug.Log("初期幼女: " + rec0.InitialYoujoCount);
        Debug.Log("生存幼女: " + rec0.SurvivedYoujoCount);
        Debug.Log("クリアタイム（秒）: " + rec0.ClearedTimeSeconds);

        Debug.Log(RecordManager.Instance.GetRecord(2)); //null
    }
}
