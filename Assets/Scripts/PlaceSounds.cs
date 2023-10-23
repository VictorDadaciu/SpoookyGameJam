using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceSounds : MonoBehaviour
{
    public GameObject soundObject;
    [SerializeField] public AudioSource[] sounds;
    [SerializeField] public Color[] colors;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && SoundWheelController.SoundId != 0) // Right click
        {
            Vector3 pos;
            if (CalculatePlaceablePosition(out pos))
            {
                PlaySound instantiatedSound = Instantiate(soundObject, pos, Quaternion.identity).GetComponent<PlaySound>();
                instantiatedSound.SetColor(colors[SoundWheelController.SoundId - 1]);
                instantiatedSound.SetAudioSource(sounds[SoundWheelController.SoundId - 1]);
                instantiatedSound.PlayAudioSource();
            }
        }
    }

    bool CalculatePlaceablePosition(out Vector3 pos)
    {
        pos = Vector3.zero;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f, 1 << LayerMask.NameToLayer("Walkable")))
        {
            pos = hit.point;
            return true;
        }
        return false;
    }
}
