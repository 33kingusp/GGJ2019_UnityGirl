using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObject/StageData")]
public class StageData : ScriptableObject
{
	[SerializeField]
	[Tooltip("このファイルのメモ")]
	public int stageId;

	[SerializeField]
	[Tooltip("このファイルのメモ")]
	public string memo;

	[SerializeField]
	[Tooltip("女の子の出現数")]
	public int spawnGirls;

	[SerializeField]
	[Tooltip("女の子の間隔秒数")]
	public float spawnGirlsIntervalTime;

	[SerializeField]
	[Tooltip("タイムリミット")]
	public float timeLimit;

	[SerializeField]
	[Tooltip("ギミック1のprefab")]
	public GameObject gimmick1Object;

	[SerializeField]
	[Tooltip("ギミック1の制限数")]
	public int gimmick1Limit;

	[SerializeField]
	[Tooltip("ギミック2のprefab")]
	public GameObject gimmick2Object;

	[SerializeField]
	[Tooltip("ギミック2の制限数")]
	public int gimmick2Limit;

	[SerializeField]
	[Tooltip("ギミック3のprefab")]
	public GameObject gimmick3Object;

	[SerializeField]
	[Tooltip("ギミック3の制限数")]
	public int gimmick3Limit;

	[SerializeField]
	[Tooltip("ギミック4のprefab")]
	public GameObject gimmick4Object;

	[SerializeField]
	[Tooltip("ギミック4の制限数")]
	public int gimmick4Limit;
}
