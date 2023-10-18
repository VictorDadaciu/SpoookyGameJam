using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public enum ActivityType
{
    Loiter,
    GetDrinks,
    Bathroom,
    Sit,
    Dance
}

public class LocationOfInterest : MonoBehaviour
{
    public Vector2 timeToSpend;

    public LocationOfInterest next;
    public LocationOfInterest prev;

    [SerializeField] public ActivityType activityType;
    public float radius;

    List<AIAgent> agents;
    int maxAgents;

    public bool renewable;

    // Start is called before the first frame update
    void Start()
    {
        maxAgents = Mathf.CeilToInt(radius * radius);
        agents = new List<AIAgent>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Get up to n agents from the previous location
    List<AIAgent> GetNewAgents(int n)
    {
        List<AIAgent> newAgents = new List<AIAgent>();
        foreach (AIAgent agent in agents)
        {
            if (newAgents.Count == n)
                break;

            newAgents.Add(agent);
        }

        return newAgents;
    }

    public void FinishedObjective(AIAgent agent)
    {
        RemoveAgent(agent);
        if (prev != null)
        {
            var newAgents = prev.GetNewAgents(maxAgents - agents.Count);
            foreach (AIAgent newAgent in newAgents)
            {
                newAgent.Leave();
                newAgent.GoTo(this);
            }
        }
    }

    public bool IsFull()
    {
        return agents.Count >= maxAgents;
    }

    public bool ClosestToEndOfChain()
    {
        if (next == null)
            return true;

        return next.IsFull();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void AddAgent(AIAgent agent)
    {
        if (IsFull())
            return;

        agents.Add(agent);
    }

    public void RemoveAgent(AIAgent agent)
    {
        agents.Remove(agent);
    }

    float GetRandomDistance()
    {
        return Mathf.Log(Random.Range(0.05f, 0.95f) + 1 + agents.Count * 0.72f / maxAgents);
    }

    public Vector3 CalculatePosition()
    {
        float distanceFromCenter = radius * GetRandomDistance();
        float angle = Random.Range(-Mathf.PI, Mathf.PI);

        float x = Mathf.Cos(angle) * distanceFromCenter;
        float z = Mathf.Sin(angle) * distanceFromCenter;

        return transform.position + new Vector3(x, transform.position.y, z);
    }

    public Vector3 GetPosition()
    {
        bool tooClose;
        Vector3 possiblePosition;
        do
        {
            tooClose = false;
            possiblePosition = CalculatePosition();
            foreach(AIAgent agent in agents)
            {
                if (Vector3.Distance(agent.GetActualPosition(), possiblePosition) < 0.8f)
                {
                    tooClose = true;
                    break;
                }

            }
        } while (tooClose);
        return possiblePosition;
    }
}
