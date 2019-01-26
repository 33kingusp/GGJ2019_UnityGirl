using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObject/StageData")]
public class StageData : ScriptableObject
{
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
	public int timeLimit;

	[SerializeField]
	[Tooltip("ギミック1の制限数")]
	public int gimmick1Limit;

	[SerializeField]
	[Tooltip("ギミック2の制限数")]
	public int gimmick2Limit;

	[SerializeField]
	[Tooltip("ギミック3の制限数")]
	public int gimmick3Limit;

	[SerializeField]
	[Tooltip("ギミック4の制限数")]
	public int gimmick4Limit;
}
