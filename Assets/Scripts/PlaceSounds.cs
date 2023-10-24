using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceSounds : MonoBehaviour
{
    public GameObject soundObject;
    [SerializeField] public AudioSource[] sounds;
    [SerializeField] public Color[] colors;

    public PuzzleManager puzzleManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && SoundWheelController.SoundId != 0) // Right click
        {
            RaycastHit hit;
            if (CalculatePlaceablePosition(out hit))
            {
                PlaySound instantiatedSound = Instantiate(soundObject, hit.point, Quaternion.identity).GetComponent<PlaySound>();
                instantiatedSound.SetColor(colors[SoundWheelController.SoundId - 1]);
                instantiatedSound.SetAudioSource(sounds[SoundWheelController.SoundId - 1]);
                instantiatedSound.PlayAudioSource();

                if (ShouldTriggerNextPuzzle(hit))
                {
                    puzzleManager.StartPuzzle(SoundWheelController.SoundId);
                }
            }
        }
    }

    bool ShouldTriggerNextPuzzle(RaycastHit hit)
    {
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("LivingRoom") && SoundWheelController.SoundId == 2)
        {
            return true;
        }
        return false;
    }

    int LayersToInt()
    {
        return 1 << LayerMask.NameToLayer("Walkable") | 1 << LayerMask.NameToLayer("LivingRoom");
    }

    bool CalculatePlaceablePosition(out RaycastHit hitOut)
    {
        hitOut = new RaycastHit();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f, LayersToInt()))
        {
            hitOut = hit;
            return true;
        }

        return false;
    }
}
