using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージの開始、終了、キャラクター出現数を判定する
/// とりあえず汎用的な名前
/// ScriptableObjectか何かで開始、終了、出現数、タイムの条件なんかを作りたい
/// 参照用でもある便利クラスのため、要リファクタ
/// </summary>
public class StageManager : MonoBehaviour
{
	//[SerializeField]
	// public Stage stageData;

	bool isEnd;

	int spawnedGirlCount;

	int timeLimit;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
