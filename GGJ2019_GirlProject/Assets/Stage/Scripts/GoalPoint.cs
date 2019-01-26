using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゴールポイントの処理:
/// </summary>
public class GoalPoint : MonoBehaviour
{

	[SerializeField]
	StageManager stageManager;

 	void Goal()
	{
		stageManager.IncGoalGirlCount();
	}

	private void OnTriggerEnter(Collider other)
	{
		Goal();
		// TODO 幼女消す
	}
}
