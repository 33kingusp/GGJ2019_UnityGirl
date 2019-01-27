using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スタートポイントの処理
/// </summary>
public class StartPoint : MonoBehaviour
{
	// FIXME こういういっぱいオブジェクト入れるところをListにしたい…

	[SerializeField]
	public GameObject girl1;

	[SerializeField]
	public GameObject girl2;

	[SerializeField]
	public GameObject girl3;

	[SerializeField]
	public GameObject girl4;

    void Start()
    {
		var stageData = StageManager.Instance.stageData;

		StartCoroutine(
			SpawnGirl(stageData.spawnGirlsIntervalTime, stageData.spawnGirls)
		);
    }

	IEnumerator SpawnGirl(float intervalTime, int spawnGirl)
	{
		for (int i = 0; i < spawnGirl; i++)
		{
			yield return new WaitForSeconds(intervalTime);

			int randomNum = Random.Range(1,5);

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
				case 4:
					girl = girl4;
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
