using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkPerson : MonoBehaviour
{
    AIAgent agent;
    AnimationBehaviour animations;
    public LocationOfInterest spawnLocation;

    void Start()
    {
        agent = GetComponent<AIAgent>();
        agent.GoTo(spawnLocation);
        animations = GetComponent<AnimationBehaviour>();
        animations.SetDrunk(true);
    }
}
