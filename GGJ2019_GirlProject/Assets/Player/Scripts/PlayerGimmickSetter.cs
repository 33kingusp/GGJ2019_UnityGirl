using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerGimmickSetter : MonoBehaviour
{
	StageData stageData;

    // 位置座標
    private Vector3 clickPosition;
    // スクリーン座標をワールド座標に変換した位置座標
    private Vector3 screenToWorldPointPosition;

	void Start()
	{
		stageData = StageManager.Instance.stageData;
	}

	public void Update()
    {
        // マウス入力で左クリックをした瞬間
        if (Input.GetMouseButtonDown(0) && !IsUGUIHit(Input.mousePosition))
        {
            SetDummyGimmick();
        }
    }

    private void SetDummyGimmick()
    {
        int currentGimmickNo = StageManager.Instance.currentGimmickNo;

        if (currentGimmickNo == 0 || !StageManager.Instance.JudgeRemainingGimmickCount())
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

        GameObject prefab = stageData.gimmick1Object;

        switch (currentGimmickNo)
        {
            case 1:
                prefab = stageData.gimmick1Object;
                break;
            case 2:
                prefab = stageData.gimmick2Object;
                break;
            case 3:
                prefab = stageData.gimmick3Object;
                break;
            case 4:
                prefab = stageData.gimmick4Object;
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

        if (IsUGUIHit(Input.mousePosition) || !gimmick.Set())
		{
            Cancel(gimmick);
		}
		else
		{
			StageManager.Instance.IncCurrentGimmickCount();
		}

		yield break;
    }

    private void Cancel(GimmickObjectController gimmick)
    {
        Debug.Log("Cancel");
        Destroy(gimmick.gameObject);
    }

    private Vector3 GetGridClickPosition()
    {
        return GridManager.GetGridPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f));
    }

    private static bool IsUGUIHit(Vector3 _scrPos)
    { 
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = _scrPos;
        List<RaycastResult> result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, result);
        return (result.Count > 0);
    }
}
