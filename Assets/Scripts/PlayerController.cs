using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    MoveCharacter moveCharacter;

    // Start is called before the first frame update
    void Start()
    {
        moveCharacter = GetComponent<MoveCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right click
        {
            Vector3 pos = Vector3.zero;
            bool canWalk = CalculateWalkablePosition(out pos);
            if (canWalk)
            {
                moveCharacter.MoveTo(pos);
            }
        }
    }

    bool CalculateWalkablePosition(out Vector3 pos)
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
