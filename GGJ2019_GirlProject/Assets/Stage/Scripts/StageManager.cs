﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステージの開始、終了、キャラクター出現数を判定する
/// とりあえず汎用的な名前
/// ScriptableObjectか何かで開始、終了、出現数、タイムの条件なんかを作りたい
/// 参照用でもある便利クラスのため、要リファクタ
/// </summary>
public class StageManager : MonoBehaviour
{
	private static StageManager _instance;
	public static StageManager Instance
	{
		get { return _instance; }
	}

	[SerializeField]
	public StageData stageData;

	[SerializeField]
	GameObject clearPanel;

	[SerializeField]
	GameObject gameOverPanel;

	// -------------------------------- 以下デバッグ確認用

	[SerializeField]
	bool isEnd;

	// 画面から消えた女の子数
	// ゴール、障害物で消失したらカウントアップ
	[SerializeField]
	int deletedGirlCount;

	// 現在の時間
	[SerializeField]
	public float currentTime;

	// 現在の対策ギミック使用数
	[SerializeField]
	public int currentUsedGimmick1;
	[SerializeField]
	public int currentUsedGimmick2;
	[SerializeField]
	public int currentUsedGimmick3;
	[SerializeField]
	public int currentUsedGimmick4;

	// 現在選択中のギミック
	[SerializeField]
	public int currentGimmickNo;

	// ゴールした女の子の数
	[SerializeField]
	public int goalGirlCount;

	void Awake()
	{
		if(_instance == null)
		{
			_instance = this;
		}
		else
		{
			Destroy(this);
		}
	}
    void Start()
    {
		// 初期化
		isEnd = false;
		deletedGirlCount = 0;
		currentTime = 0;
		currentUsedGimmick1 = 0;
		currentUsedGimmick2 = 0;
		currentUsedGimmick3 = 0;
		currentUsedGimmick4 = 0;
		goalGirlCount = 0;
    }

    void Update()
    {
		// 終了処理
		if (JudgeIsEnd())
		{
			if (goalGirlCount > 0)
			{
				// クリア処理
				StageClear();
			}
			else
			{
				// ゲームオーバー処理
				StageGameOver();
			}
		}
    }

	/// <summary>
	/// 終了判定
	/// </summary>
	/// <returns></returns>
	bool JudgeIsEnd()
	{
		// 時間制限か
		if(stageData.timeLimit <= currentTime)
		{
			isEnd = true;
		}

		// 女の子が全て消失したか
		if (stageData.spawnGirls <= deletedGirlCount)
		{
			isEnd = true;
		}

		return isEnd;
	}

	/// <summary>
	/// 使用されたギミックをインクリメント
	/// </summary>
	public void IncCurrentGimmickCount()
	{
		// FIXME 定数にしたい
		switch(currentGimmickNo)
		{
			case 1:
				currentUsedGimmick1++;
				break;
			case 2:
				currentUsedGimmick2++;
				break;
			case 3:
				currentUsedGimmick3++;
				break;
			case 4:
				currentUsedGimmick4++;
				break;
			default:
				// 例外とかはやらない想定
				break;
		}
	}

	/// <summary>
	/// 残り回数がある場合はTrueになる
	/// </summary>
	/// <returns></returns>
	public bool JudgeRemainingGimmickCount()
	{
		switch(currentGimmickNo)
		{
			case 1:
				return currentUsedGimmick1 < stageData.gimmick1Limit;
			case 2:
				return currentUsedGimmick2 < stageData.gimmick2Limit;
			case 3:
				return currentUsedGimmick3 < stageData.gimmick3Limit;
			case 4:
				return currentUsedGimmick4 < stageData.gimmick4Limit;
			default:
				// 例外とかはやらない想定
				return false;
		}
	}

	/// <summary>
	/// ゴールした女の子
	/// </summary>
	public void IncGoalGirlCount()
	{
		goalGirlCount++;
	}

	/// <summary>
	/// 消滅した女の子
	/// </summary>
	public void IncDeleteGirlCount()
	{
		deletedGirlCount++;
	}

	/// <summary>
	/// ステージクリア
	/// </summary>
	void StageClear()
	{
		Debug.Log("stage clear");
		clearPanel.SetActive(true);
		RecordManager.Instance.SaveRecord(stageData.stageId, stageData.spawnGirls, goalGirlCount, currentTime);
	}

	/// <summary>
	/// ゲームオーバー
	/// </summary>
	void StageGameOver()
	{
		Debug.Log("gameover");
		gameOverPanel.SetActive(true);
	}

    public void OnClickGimmick(int gimmicNo)
    {
        currentGimmickNo = gimmicNo;
    }

    /*
	public void OnClickGimmick1()
	{
		currentGimmickNo = 1;
	}

	public void OnClickGimmick2()
	{
		currentGimmickNo = 2;
	}

	public void OnClickGimmick3()
	{
		currentGimmickNo = 3;
	}

	public void OnClickGimmick4()
	{
		currentGimmickNo = 4;
	}
    */
}
