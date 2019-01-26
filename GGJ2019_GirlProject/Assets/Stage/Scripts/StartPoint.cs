using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スタートポイントの処理
/// </summary>
public class StartPoint : MonoBehaviour
{
	[SerializeField]
	public GameObject girl1;

	[SerializeField]
	public GameObject girl2;

	[SerializeField]
	public GameObject girl3;

    void Start()
    {
		var stageData = StageManager.Instance.stageData;

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

			int randomNum = Random.Range(1,4);

			GameObject girl;
			switch (randomNum)
			{
				case 1:
					girl = girl1;
					break;
				case 2:
					girl = girl2;
					break;
				case 3:
					girl = girl3;
					break;
				default:
					// TODO 不審な値が来た時の処理
					girl = girl1;
					break;
			}

			Instantiate(girl, this.transform.position, Quaternion.identity);
		}
	}
}
