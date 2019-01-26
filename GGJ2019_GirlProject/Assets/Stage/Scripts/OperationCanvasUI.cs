using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperationCanvasUI : MonoBehaviour
{
	[SerializeField]
	public GameObject gimmick1Button;

	[SerializeField]
	public GameObject gimmick2Button;

	[SerializeField]
	public GameObject gimmick3Button;

	[SerializeField]
	public GameObject gimmick4Button;

	[SerializeField]
	public Text gimmick1Text;

	[SerializeField]
	public Text gimmick2Text;

	[SerializeField]
	public Text gimmick3Text;

	[SerializeField]
	public Text gimmick4Text;


	StageData stageData;

	void Start()
	{
		stageData = StageManager.Instance.stageData;
		SetEffectiveGimmickButton();
	}

	void Update()
	{
		UpdateText();
	}

	void SetEffectiveGimmickButton()
	{
		if (stageData.gimmick1Object == null)
		{
			gimmick1Button.SetActive(true);
		}

		if (stageData.gimmick2Object == null)
		{
			gimmick2Button.SetActive(true);
		}

		if (stageData.gimmick3Object == null)
		{
			gimmick3Button.SetActive(true);
		}

		if (stageData.gimmick4Object == null)
		{
			gimmick4Button.SetActive(true);
		}
	}

	void UpdateText()
	{
		gimmick1Text.text = (stageData.gimmick1Limit - StageManager.Instance.currentUsedGimmick1).ToString();
		gimmick2Text.text = (stageData.gimmick2Limit - StageManager.Instance.currentUsedGimmick2).ToString();
		gimmick3Text.text = (stageData.gimmick3Limit - StageManager.Instance.currentUsedGimmick3).ToString();
		gimmick4Text.text = (stageData.gimmick4Limit - StageManager.Instance.currentUsedGimmick4).ToString();
	}
}
