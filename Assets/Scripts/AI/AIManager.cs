using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    List<AIAgent> agents;
    List<LocationOfInterest> locationsOfInterest;

    public GameObject locationsParent;

    // Start is called before the first frame update
    void Start()
    {
        agents = new List<AIAgent>();
        locationsOfInterest = new List<LocationOfInterest>();

        foreach (Transform child in transform)
        {
            agents.Add(child.GetComponent<AIAgent>());
        }

        foreach (Transform child in locationsParent.transform)
        {
            locationsOfInterest.Add(child.GetComponent<LocationOfInterest>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (AIAgent agent in agents)
        {
            if (!agent.IsBusy())
            {
                List<Pair<LocationOfInterest, float>> priorities = new List<Pair<LocationOfInterest, float>>();
                foreach (LocationOfInterest loc in locationsOfInterest)
                {
                    if (!loc.IsFull() && loc.ClosestToEndOfChain())
                    {
                        priorities.Add(new Pair<LocationOfInterest, float>(loc, CalculateInterest(agent, loc)));
                    }
                }

                SortByPriority(ref priorities);
                foreach (Pair<LocationOfInterest, float> priority in priorities)
                {
                    LocationOfInterest bestLoc = priority.item1;
                    if (agent.CanGoTo(bestLoc))
                    {
                        agent.GoTo(bestLoc);
                        break;
                    }
                }
            }
        }
    }

    void SortByPriority(ref List<Pair<LocationOfInterest, float>> priorities)
    {
        priorities.Sort((a, b) => b.item2.CompareTo(a.item2));
    }

    float CalculateInterest(AIAgent agent, LocationOfInterest loc)
    {
        float distance = Vector3.Distance(agent.transform.position, loc.transform.position);

        float priority = 100f - distance;

        return priority;
    }
}
