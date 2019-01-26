using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButton : MonoBehaviour
{

    public void ReturnTitle()
    {
        SceneManager.LoadScene("ReleaseScenes/Title");
    }


    public void StartStage1()
    {	
        SceneManager.LoadScene("ReleaseScenes/Stage1");
    }

	public void StartStage2()
	{
        SceneManager.LoadScene("ReleaseScenes/Stage2");
	}

	public void StartStage3()
	{
        SceneManager.LoadScene("ReleaseScenes/Stage3");
	}
}
