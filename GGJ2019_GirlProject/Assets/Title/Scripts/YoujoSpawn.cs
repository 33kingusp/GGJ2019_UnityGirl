using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoujoSpawn : MonoBehaviour
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
		StartCoroutine(
			SpawnGirl()
		);
    }

	IEnumerator SpawnGirl()
	{
		while(true)
		{
			yield return new WaitForSeconds(1.0f);

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
