using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager
{

    /// <summary>
    /// 指定座標のグリッド座標(左上)を返す
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static Vector3 GetGridPosition(Vector3 position)
    {
        
        Vector3 gridPos = position;

        gridPos.x = Mathf.Floor(gridPos.x);

        gridPos.y = Mathf.Ceil(gridPos.y);

        Debug.Log($"現在の座標={position},グリッド座標={gridPos}");

        return Vector3.zero;
    }

    //private void Update()
    //{
    //    GetGridPosition(transform.position);
    //}
}
