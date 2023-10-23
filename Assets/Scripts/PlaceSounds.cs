using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceSounds : MonoBehaviour
{
    public GameObject soundObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right click
        {
            Vector3 pos;
            if (CalculatePlaceablePosition(out pos))
            {
                PlaySound instantiatedSound = Instantiate(soundObject, pos, Quaternion.identity).GetComponent<PlaySound>();
                instantiatedSound.SetAudioSource(null);
                instantiatedSound.PlayAudioSource();
            }
        }
    }

    bool CalculatePlaceablePosition(out Vector3 pos)
    {
        pos = Vector3.zero;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 60.0f, 1 << LayerMask.NameToLayer("Walkable")))
        {
            pos = hit.point;
            return true;
        }
        return false;
    }
}
