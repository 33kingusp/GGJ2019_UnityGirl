using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public void StartTutorial()
    {	
        SceneManager.LoadScene("ReleaseScenes/TalkTutorial");
    }

	public void SkipTutorial()
	{
		SceneManager.LoadScene("ReleaseScenes/TalkStage1");
	}
}
