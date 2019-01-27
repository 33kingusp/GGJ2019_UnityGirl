using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{

	[SerializeField]
	public Text timeText;

    void Update()
    {
		var time = StageManager.Instance.GetRemainigTime();
		timeText.text = "あと " + time + " 秒";
    }
}
