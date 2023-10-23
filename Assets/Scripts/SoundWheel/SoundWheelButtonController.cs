using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SoundWheelButtonController : MonoBehaviour
{
    [SerializeField] private Animator AnimatorComponent;
    [SerializeField] private string ItemName;
    [SerializeField] private TextMeshProUGUI ItemText;
    [SerializeField] private SoundWheelController controller;
    [SerializeField] private int soundNumber;
    
    private void Start()
    {
        AnimatorComponent = GetComponent<Animator>();
    }
    
    public void IsSelected()
    {
       controller.SetSelected(soundNumber);
    }

    public void HoverEnter()
    {
        AnimatorComponent.SetBool("Hover", true);
        ItemText.text = ItemName;
        
    }
    public void HoverExit()
    {
        AnimatorComponent.SetBool("Hover", false);
        ItemText.text = "";
        
    }
}
