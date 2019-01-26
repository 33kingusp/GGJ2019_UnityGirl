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
            SetGimmick();
        }
    }

    void SetGimmick()
    {
        int currentGimmickNo = StageManager.Instance.currentGimmickNo;
        if (currentGimmickNo == 0)
        {
            // 出さない
            return;
        }


        // ここでの注意点は座標の引数にVector2を渡すのではなく、Vector3を渡すことである。
        // Vector3でマウスがクリックした位置座標を取得する
        clickPosition = Input.mousePosition;
        // Z軸修正
        clickPosition.z = 10f;

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
        Vector3 position = GridManager.GetGridPosition(Camera.main.ScreenToWorldPoint(clickPosition));
        Instantiate(prefab, position, prefab.transform.rotation);
		Debug.Log(position);
    }
}
