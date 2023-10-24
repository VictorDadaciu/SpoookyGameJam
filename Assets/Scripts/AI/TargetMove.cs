using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    AIAgent agent;
    public LocationOfInterest spawnLocation;
    public LocationOfInterest inBedroom;
    public LocationOfInterest outsideDoor;
    bool target = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<AIAgent>();
        agent.GoTo(spawnLocation);
        target = name.StartsWith("Target");
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        if (!target) { return; }
=======
        
        if (Physics.CheckSphere(transform.position - new Vector3(0f, 4f, 0f), 2f, 1 << LayerMask.NameToLayer("Pula")))
        {
            animations.TriggerPickUp();
            startTimer = true;
        }
>>>>>>> 0886420e9bb3189c5f9b5a84e3ab0e8b09317810

        if (Vector3.Distance(transform.position, inBedroom.transform.position) < 4f)
        {
            agent.Leave(true, true);
            agent.GoTo(outsideDoor);
        }
    }
}
