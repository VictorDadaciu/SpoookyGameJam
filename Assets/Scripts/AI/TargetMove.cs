using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    AIAgent agent;
    AnimationBehaviour animations;
    public LocationOfInterest spawnLocation;
    public LocationOfInterest outsideDoor;
    bool startTimer = false;
    float timeUntilAfterPickUp = 2f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<AIAgent>();
        agent.GoTo(spawnLocation);
        animations = GetComponent<AnimationBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Physics.CheckSphere(transform.position - new Vector3(0f, 4f, 0f), 2f, 1 << LayerMask.NameToLayer("Pula")))
        {
            animations.TriggerPickUp();
            startTimer = true;
        }

        if (startTimer)
        {
            timeUntilAfterPickUp -= Time.deltaTime;
            if (timeUntilAfterPickUp < 0)
            {
                agent.GoTo(outsideDoor);
            }
        }
    }
}
