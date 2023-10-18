using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
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
        agent.SetDestination(position);
    }

    public void MoveTo(float x, float y, float z)
    {
        MoveTo(new Vector3(x, y, z));
    }

    public bool IsMoving()
    {
        return agent.velocity.magnitude > 0.01f;
    }
}
