using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGimmickSetter : MonoBehaviour
{
    [SerializeField]
    public GameObject gimmick1Prefab;

    [SerializeField]
    public GameObject gimmick2Prefab;

    [SerializeField]
    public GameObject gimmick3Prefab;

    [SerializeField]
    public GameObject gimmick4Prefab;

    // 位置座標
    private Vector3 clickPosition;
    // スクリーン座標をワールド座標に変換した位置座標
    private Vector3 screenToWorldPointPosition;

    public void Update()
    {
        // マウス入力で左クリックをした瞬間
        if (Input.GetMouseButtonDown(0))
        {
            SetDummyGimmick();
        }
    }

    private void SetDummyGimmick()
    {
        int currentGimmickNo = StageManager.Instance.currentGimmickNo;
        if (currentGimmickNo == 0)
        {
            // 出さない
            return;
        }

        /*
        // ここでの注意点は座標の引数にVector2を渡すのではなく、Vector3を渡すことである。
        // Vector3でマウスがクリックした位置座標を取得する
        clickPosition = Input.mousePosition;
        // Z軸修正
        clickPosition.z = 10f;
        */

        GameObject prefab = gimmick1Prefab;

        switch (currentGimmickNo)
        {
            case 1:
                prefab = gimmick1Prefab;
                break;
            case 2:
                prefab = gimmick2Prefab;
                break;
            case 3:
                prefab = gimmick3Prefab;
                break;
            case 4:
                prefab = gimmick4Prefab;
                break;
        }

        // オブジェクト生成 : オブジェクト(GameObject), 位置(Vector3), 角度(Quaternion)
        // ScreenToWorldPoint(位置(Vector3))：スクリーン座標をワールド座標に変換する
        GimmickObjectController gimmick = Instantiate(prefab, GetGridClickPosition(), prefab.transform.rotation).GetComponent<GimmickObjectController>();
        StartCoroutine(SetGimmick(gimmick));
    }

    private IEnumerator SetGimmick(GimmickObjectController gimmick)
    {
        do
        {
            gimmick.transform.position = GetGridClickPosition();
            yield return null;
        }
        while (!Input.GetMouseButtonUp(0));
        gimmick.Set();
        yield break;
    }

    private Vector3 GetGridClickPosition()
    {
        return GridManager.GetGridPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f));
    }
}
