using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveCharacter : MonoBehaviour
{
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 position)
    {
        Debug.DrawLine(transform.position, position, Color.red);
        agent.SetDestination(position);
    }

    public void MoveTo(float x, float y, float z)
    {
        MoveTo(new Vector3(x, y, z));
    }
}
