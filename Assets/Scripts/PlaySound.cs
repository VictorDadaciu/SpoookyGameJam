using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    AudioSource audioToPlay;
    float timeUntilDelete;
    bool isPlaying = false;

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            timeUntilDelete -= Time.deltaTime;
            if (timeUntilDelete < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetAudioSource(AudioSource source)
    {
        audioToPlay = source;
        if (audioToPlay != null) 
        {
            timeUntilDelete = audioToPlay.clip.length;
        }
        else
        {
            timeUntilDelete = 2f;
        }
    }

    public void PlayAudioSource()
    {
        if (audioToPlay != null)
        {
            audioToPlay.Play();
        }
        isPlaying = true;
    }
}
