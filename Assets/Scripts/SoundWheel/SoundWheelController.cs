using System;
using UnityEngine;
using UnityEngine.UI;
public class SoundWheelController : MonoBehaviour
{
    [SerializeField] private Animator AnimatorComponent;
    [SerializeField] private bool SoundWheelSelected = false;
    [SerializeField] private Image SelectedItem;
    [SerializeField] private Sprite NoImage;
    [SerializeField] public static int SoundId;
    [SerializeField] public Sprite[] icons;
    
    private void Update()
    {
        if (Input.GetKeyDown((KeyCode.Q)))
        { 
            SoundWheelSelected = !SoundWheelSelected;
        }

        if (SoundWheelSelected)
        {
            AnimatorComponent.SetBool("OpenSoundWheel", true);
        }
        else
        {
            
            AnimatorComponent.SetBool("OpenSoundWheel", false);
        }

        switch (SoundId)
        {
            case 0:
                 SelectedItem.sprite = NoImage; break;
            case 1:
                 Debug.Log("Item 1");
                 break;
            case 2:
                 Debug.Log("Item 2");
                 break;
            case 3:
                 Debug.Log("Item 3");
                 break;
            case 4:
                 Debug.Log("Item 4");
                 break;
            case 5:
                 Debug.Log("Item 5");
                 break;
            case 6:
                 Debug.Log("Item 6");
                 break;
            case 7:
                 Debug.Log("Item 7");
                 break;
            case 8:
                 Debug.Log("Item 8");
                 break;
            
        }
    }
    
    public void SetSelected(int soundId)
    {
         SoundId = soundId;
         SelectedItem.sprite = icons[soundId];
    }

}
