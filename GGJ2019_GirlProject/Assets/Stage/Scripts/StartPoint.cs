using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スタートポイントの処理
/// </summary>
public class StartPoint : MonoBehaviour
{
	[SerializeField]
	public GameObject girl;

	[SerializeField]
	public StageManager stageManager;

    void Start()
    {
		var stageData = stageManager.stageData;

		StartCoroutine(
			SpawnGirl(stageData.spawnGirlsIntervalTime, stageData.spawnGirls)
		);
    }

    // Update is called once per frame
    void Update()
    {
    }

	IEnumerator SpawnGirl(float intervalTime, int spawnGirl)
	{
		for (int i = 0; i < spawnGirl; i++)
		{
			yield return new WaitForSeconds(intervalTime);
			Instantiate(girl, this.transform.position, Quaternion.identity);
		}
	}
}
