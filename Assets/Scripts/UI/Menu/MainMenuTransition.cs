using UnityEngine;

#pragma warning disable 0649, 0414

public class MainMenuTransition : MonoBehaviour
{
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject spaceButton;
    [SerializeField] private GameObject[] menuButtons;

    [SerializeField] private float blendFactor = 0.005f;
    [SerializeField] private float desiredLogoY = 180.0f;
    
    private RectTransform logoRectTransform;
    private MenuButtonController menuButtonController;
    private bool spacePressed = false;


    void Start()
    {
        menuButtonController = GetComponent<MenuButtonController>();
        menuButtonController.enabled = false;
        logoRectTransform = logo.GetComponent<RectTransform>();

        foreach(var button in menuButtons)
        {
            button.SetActive(false);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;
            spaceButton.SetActive(false);
        }

        if(spacePressed)
        {
            if (logoRectTransform.anchoredPosition.y < desiredLogoY)
            {
                logoRectTransform.anchoredPosition = new Vector2(logoRectTransform.anchoredPosition.x,
                    logoRectTransform.anchoredPosition.y + blendFactor);
            }
            else
            {
                foreach (var button in menuButtons)
                {
                    button.SetActive(true);
                }
                menuButtonController.enabled = true;
            }
        }
    }
}
