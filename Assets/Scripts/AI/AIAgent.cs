using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    bool busy = false;
    MoveCharacter moveCharacter;
    Queue<Pair<LocationOfInterest, float>> burnedOutLocations;

    // Start is called before the first frame update
    void Start()
    {
        moveCharacter = GetComponent<MoveCharacter>();
        burnedOutLocations = new Queue<Pair<LocationOfInterest, float>>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var pair in burnedOutLocations)
        {
            pair.item2 -= Time.deltaTime;
            if (pair.item2 <= 0)
            {
                burnedOutLocations.Dequeue();
                break;
            }
        }
    }

    public bool PossibleForOccupying(LocationOfInterest location)
    {
        // insted of adding the specific location as a burnout, maybe add the type of location as a burnout (sa nu se duca la trei baruri diferite)
        foreach (var pair in burnedOutLocations)
        {
            if (pair.item1 == location)
                return false;
        }
        return true;
    }

    public void ResetObjective(LocationOfInterest loc)
    {
        busy = false;
        burnedOutLocations.Enqueue(new Pair<LocationOfInterest, float>(loc, Random.Range(40f, 60f)));
    }

    public void SetObjective(Vector3 position)
    {
        moveCharacter.MoveTo(position);
    }

    public void SetBusy(bool newBusy)
    {
        busy = newBusy;
    }

    public bool GetBusy()
    {
        return busy;
    }
}
