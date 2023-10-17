using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649, 0414

public class AnimatorFunctions : MonoBehaviour
{
    [SerializeField] MenuButtonController menuButtonController;
    public bool disableOnce;

    void PlaySound(AudioClip whichSound){
        if(!disableOnce){
            menuButtonController.audioSource.PlayOneShot (whichSound);
        }else{
            disableOnce = false;
        }
    }
}