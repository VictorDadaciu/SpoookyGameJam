using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    AIAgent agent;
    public LocationOfInterest spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<AIAgent>();
        agent.GoTo(spawnLocation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
