using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LocationOfInterest : MonoBehaviour
{
    bool occupied;
    public Vector2 timeToSpend;
    float occuppyingTimer;

    AIAgent occupyingAgent;

    // Start is called before the first frame update
    void Start()
    {
        occupied = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (occupied) 
        {
            occuppyingTimer -= Time.deltaTime;
            if (occuppyingTimer <= 0)
            {
                SetOccupied(false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position, 0.2f);
    }

    public bool GetOccupied()
    {
        return occupied;
    }

    public void SetOccupied(bool occupy)
    {
        occupied = occupy;
        if (!occupied)
        {
            SetOccupyingAgent(null);
        }
        else
        {
            StartTimer();
        }
    }

    public void SetOccupyingAgent(AIAgent agent)
    {
        if (agent == null)
            occupyingAgent.ResetObjective(this);
        occupyingAgent = agent;
    }

    public void StartTimer()
    {
        occuppyingTimer = Random.Range(timeToSpend.x, timeToSpend.y);
    }
}
