using UnityEngine;

public class Credits : MonoBehaviour
{



    void Update()
    {
        // Check for the Escape key to close the application
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}