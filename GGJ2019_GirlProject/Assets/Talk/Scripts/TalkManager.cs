using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// TODO
/// - どっちも: アニメーションを指定
/// - どっちも: 画像を指定
/// - TalkData: アニメーション速度を指定
/// - TalkData: 画像反転設定
/// - TalkData: 画面遷移とかのイベント系
/// - Manager: 吹き出しにする
/// - TalkData: 吹き出し種類を指定できるように
/// - どっちも: キャラ配置の拡張が面倒なので何とかしたい
/// 
/// </summary>
public class TalkManager : MonoBehaviour
{
	[SerializeField]
	public TalkData talkData;

	[SerializeField]
	public GameObject leftCharacter;

	[SerializeField]
	public GameObject rightCharacter;

	// TODO 吹き出しにする
	[SerializeField]
	public Text leftText;
	// TODO 吹き出しにする
	[SerializeField]
	public Text rightText;

	[SerializeField]
	int currentTalkInfo;
	[SerializeField]
	bool isEnd;
	[SerializeField]
	bool isWordEnd;

    // Start is called before the first frame update
    void Start()
    {
		currentTalkInfo = 0;
		isEnd = false;
		isWordEnd = false;

		leftText.text = ""; 
		rightText.text = "";

		TalkUpdate();
    }

	// FIXME 文字列で ""現在の"" シーン名を渡すというくそコード
	// OnClick時に呼ぶので、設定ミス用注意！！
	public void OnClickNext(string scene)
	{
		int last = talkData.talkInfoList.Count - 1;
		isEnd = (last <= currentTalkInfo);

		if (!isWordEnd)
		{
			// 表示が最後まで行ってない場合は何もしない
			return;
		}
		else if (isEnd)
		{
			Debug.Log("isend");
			// 画面遷移とかいろいろ
			TransitionScene(scene);
			return;
		}
		else
		{
			currentTalkInfo++;
			TalkUpdate();
		}

	}

	void TalkUpdate()
	{
		// リセット
		leftText.text = ""; 
		rightText.text = "";

		TalkData.TalkInfo info = talkData.talkInfoList[currentTalkInfo];

		var leftCharacterAnimator = leftCharacter.GetComponent<Animator>();
		leftCharacterAnimator.runtimeAnimatorController = info.leftCharacter;
		//leftCharacterAnimator.SetBool("isTalk", false);


		var rightCharacterAnimator = rightCharacter.GetComponent<Animator>();
		rightCharacterAnimator.runtimeAnimatorController = info.rightCharacter;
		//rightCharacterAnimator.SetBool("isTalk", false);

		// TODO TalkSideを受け取って吹き出しを生成したい
		if(info.talkSide == TalkData.TalkInfo.TalkSide.TALK_LEFT)
		{
			DisplayWord(leftText, info.word, info.wordSpeed, leftCharacterAnimator);
			//leftCharacterAnimator.SetBool("isTalk", true);
			leftCharacterAnimator.speed = info.leftCharacterAnimationSpeed;
		}
		else
		{
			DisplayWord(rightText, info.word, info.wordSpeed, rightCharacterAnimator);
			//rightCharacterAnimator.SetBool("isTalk", true);
			rightCharacterAnimator.speed = info.rightCharacterAnimationSpeed;
		}

		var leftCharacterImage = leftCharacter.GetComponent<SpriteRenderer>();
		var rightCharacterImage = leftCharacter.GetComponent<SpriteRenderer>();
	}

	// TODO ちょっと使いにくいので改修する必要あり。UniRxは外部機能のため、コルーチン、async/awaitをつかう
	async void DisplayWord(Text text, string word, float speed, Animator animator)
	{
		int messageCount = 0;
		text.text = "";
		isWordEnd = false;

		while (word.Length > messageCount)
		{
			text.text += word[messageCount];
			messageCount++;
			await Task.Delay(TimeSpan.FromSeconds(speed));
		}

		//animator.SetBool("isTalk", false);

		isWordEnd = true;
	}

	/// <summary>
	/// FIXME シーン遷移　シーンの文字を受け取って分岐するくそ
	/// 現在のシーン名を渡すと次のシーンに移る
	/// </summary>
	/// <param name="scene"></param>
	void TransitionScene(string scene)
	{
		switch (scene)
		{
			case "Tutorial":
				SceneManager.LoadScene("ReleaseScenes/TalkStage1");
				break;
			case "TalkStage1":
				SceneManager.LoadScene("ReleaseScenes/Stage1");
				break;
			case "TalkStage2":
				SceneManager.LoadScene("ReleaseScenes/Stage2");
				break;
			case "TalkStage3":
				SceneManager.LoadScene("ReleaseScenes/Stage3");
				break;
			case "Ending":
				SceneManager.LoadScene("ReleaseScenes/Title");
				break;
		}
	}
}
