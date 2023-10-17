using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool isContinue;
    public void NewGame()
    {
        StartCoroutine(LoadAsyncScene());
    }
    public void ContinueGame()
    {
        isContinue = true;
        StartCoroutine(LoadAsyncScene());
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Ovidiu");


        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    //
}

