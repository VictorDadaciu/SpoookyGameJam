using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SoundWheelButtonController : MonoBehaviour
{
    [SerializeField] private int Id;
    [SerializeField] private Animator AnimatorComponent;
    [SerializeField] private string ItemName;
    [SerializeField] private TextMeshProUGUI ItemText;
    [SerializeField] private Image SelectedItem;
    [SerializeField] private bool Selected = false;
    [SerializeField] private Sprite Icon;

    private void Start()
    {
        AnimatorComponent = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Selected)
        {
            SelectedItem.sprite = Icon;
            ItemText.text = ItemName;
        }
    }

    public void IsSelected()
    {
        Selected = true;
        SoundWheelController.SoundId = Id;
        
    }

    public void IsDeselected()
    {
        Selected = false;
        
        SoundWheelController.SoundId = 0;
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
