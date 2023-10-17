using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject playerComponent;
    public static bool GameIsPaused = false;
    public GameObject saveMenuUI;
    public GameObject pauseMenuUI;
    public GameObject inventoryCanvas;
   // public GameObject dialogueCanvas;
   

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        } 
    }

    public void Resume()
    {
        Cursor.visible = false;
        saveMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
       // dialogueCanvas.SetActive(true);
        inventoryCanvas.SetActive(false);
    }

    void Pause()
    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
       
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        //dialogueCanvas.SetActive(false);
        inventoryCanvas.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SaveGame()
    {
        //TODO
    }

    public void LoadGame()
    {
        //TODO
    }
}
