using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    AIAgent agent;
    public LocationOfInterest spawnLocation;
    public LocationOfInterest inBedroom;
    public LocationOfInterest outsideDoor;
    public LocationOfInterest inBedroom2;
    public LocationOfInterest kitchen;
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
        if (!target) { return; }

        if (Vector3.Distance(transform.position, inBedroom.transform.position) < 4f)
        {
            agent.Leave(true, true);
            agent.GoTo(outsideDoor);
        }

        if (Vector3.Distance(transform.position, inBedroom2.transform.position) < 4f)
        {
            agent.Leave(true, true);
            agent.GoTo(kitchen);
        }
    }
}
