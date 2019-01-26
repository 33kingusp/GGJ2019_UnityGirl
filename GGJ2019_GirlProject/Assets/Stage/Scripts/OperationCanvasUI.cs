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

	[SerializeField]
	public Sprite square;

	[SerializeField]
	public Sprite rightTriangular;

	[SerializeField]
	public Sprite leftTriangular;
	
	[SerializeField]
	public Sprite tool;

	StageData stageData;

	void Start()
	{
		stageData = StageManager.Instance.stageData;
		SetEffectiveGimmickButton();
	}

	void Update()
	{
		UpdateText();
		UpdateImage();
	}

	void SetEffectiveGimmickButton()
	{
		if (stageData.gimmick1Object == null)
		{
			gimmick1Button.SetActive(false);
		}

		if (stageData.gimmick2Object == null)
		{
			gimmick2Button.SetActive(false);
		}

		if (stageData.gimmick3Object == null)
		{
			gimmick3Button.SetActive(false);
		}

		if (stageData.gimmick4Object == null)
		{
			gimmick4Button.SetActive(false);
		}
	}

	void UpdateText()
	{
		gimmick1Text.text = (stageData.gimmick1Limit - StageManager.Instance.currentUsedGimmick1).ToString();
		gimmick2Text.text = (stageData.gimmick2Limit - StageManager.Instance.currentUsedGimmick2).ToString();
		gimmick3Text.text = (stageData.gimmick3Limit - StageManager.Instance.currentUsedGimmick3).ToString();
		gimmick4Text.text = (stageData.gimmick4Limit - StageManager.Instance.currentUsedGimmick4).ToString();
	}

	void UpdateImage()
	{
		// FIXME クソコード　直して
		if (stageData.gimmick1Object != null)
		{
			string gimmick1Name = stageData.gimmick1Object.name;
			Image gimmick1ButtonImage = gimmick1Button.GetComponent<Image>();
			ChangeButtonImage(gimmick1Name, gimmick1ButtonImage);
		}

		if (stageData.gimmick2Object != null)
		{
			string gimmick2Name = stageData.gimmick2Object.name;
			Image gimmick2ButtonImage = gimmick2Button.GetComponent<Image>();
			ChangeButtonImage(gimmick2Name, gimmick2ButtonImage);
		}

		if (stageData.gimmick3Object != null)
		{
			string gimmick3Name = stageData.gimmick3Object.name;
			Image gimmick3ButtonImage = gimmick3Button.GetComponent<Image>();
			ChangeButtonImage(gimmick3Name, gimmick3ButtonImage);
		}

		if (stageData.gimmick4Object != null)
		{
			string gimmick4Name = stageData.gimmick4Object.name;
			Image gimmick4ButtonImage = gimmick4Button.GetComponent<Image>();
			ChangeButtonImage(gimmick4Name, gimmick4ButtonImage);
		}

	}

	void ChangeButtonImage(string name, Image image)
	{
		switch(name)
		{
			case "GimmickReturnSlope":
				image.sprite = rightTriangular;
				break;
			case "GimmickSlope":
				image.sprite = leftTriangular;
				break;
			case "GimmickBox":
				image.sprite = square;
				break;
			default:
				// TODO とりあえずそのままにしています…
				break;
		}
		
	}
}
