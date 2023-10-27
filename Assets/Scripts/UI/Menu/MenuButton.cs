using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable 0649, 0414

public class MenuButton : MonoBehaviour
{
    [SerializeField] MenuButtonController menuButtonController;
    [SerializeField] Animator animator;
    [SerializeField] int thisIndex;
    [SerializeField] bool isSpace = false;void Update()
    {
        if (menuButtonController.index == thisIndex)
        {
            animator.SetBool("selected", true);
            if (Input.GetAxis("Submit") == 1)
            {
                if (!animator.GetBool("pressed"))
                {
                    animator.SetBool("pressed", true);
                    ButtonEvent(thisIndex);
                }
            }
            else if (animator.GetBool("pressed"))
            {
                animator.SetBool("pressed", false);
            }
        }
        else
        {
            animator.SetBool("selected", false);
        }
    }


    public void ButtonEvent(int index)
    {
        if (index == 0 && !isSpace)
        {
            StartCoroutine(LoadAsyncMainScene());
        }

        if (index == 1)
        {
            QuitGame();
        }
        
        
    }


        private void QuitGame()
        {
    #if UNITY_STANDALONE
            Application.Quit(); // Quit the application only in standalone build
    #endif
        }
    IEnumerator LoadAsyncMainScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelScene");
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}