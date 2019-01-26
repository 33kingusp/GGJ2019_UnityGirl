using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TalkData", menuName = "ScriptableObject/TalkData")]
public class TalkData : ScriptableObject
{
	[SerializeField]
	[Tooltip("このファイルのメモ")]
	public string memo;

	[SerializeField]
	public List<TalkInfo> talkInfoList = new List<TalkInfo>();

	[System.Serializable]
	public class TalkInfo
	{
		public enum TalkSide
		{
			TALK_LEFT,
			TALK_RIGHT
		}

		[SerializeField]
		[Tooltip("喋らせる言葉")]
		[TextArea]
		public string word;

		[SerializeField]
		[Tooltip("文字表示速度")]
		public float wordSpeed;

		[SerializeField]
		[Tooltip("喋る人がどっちにいるか")]
		public TalkSide talkSide = TalkSide.TALK_LEFT;

		// TODO アニメーションにするのめんどいからスプライトも選択したい
		[SerializeField]
		[Tooltip("左側キャラクター画像アニメーション")]
		public AnimatorOverrideController leftCharacter;

		[SerializeField]
		[Tooltip("左側キャラクターアニメーションスピード")]
		public float leftCharacterAnimationSpeed;

		[SerializeField]
		[Tooltip("右側キャラクター画像アニメーション")]
		public AnimatorOverrideController rightCharacter;

		[SerializeField]
		[Tooltip("右側キャラクターアニメーションスピード")]
		public float rightCharacterAnimationSpeed;
	}
}
