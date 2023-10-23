using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.AI;

public enum MoveState
{
    Idle,
    Moving,
    StopIdling,
    JustMoved,
    JustStopped
}


public class MoveCharacter : MonoBehaviour
{
    NavMeshAgent agent;
    AIAgent ai;
    AnimationBehaviour animations;

    MoveState currentState;
    Vector3 objectivePos;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ai = GetComponent<AIAgent>();
        animations = GetComponent<AnimationBehaviour>();
        objectivePos = Vector3.zero;

        currentState = MoveState.Idle;
    }

    void Update()
    {
        bool isMoving = IsMoving();
        bool movingState = currentState == MoveState.Moving || currentState == MoveState.JustMoved;
        bool idleState = currentState == MoveState.Idle || currentState == MoveState.JustStopped || currentState == MoveState.StopIdling;
        if (idleState && isMoving)
        {
            currentState = MoveState.JustMoved;
            animations.SetWalking(true);
        }
        else if (movingState && !isMoving)
        {
            currentState = MoveState.JustStopped;
            animations.SetWalking(false);
        }
        else if (currentState == MoveState.StopIdling && isMoving)
        {
            currentState = MoveState.JustMoved;
            animations.SetWalking(true);
        }
        else if (currentState == MoveState.JustMoved && isMoving)
        {
            currentState = MoveState.Moving;
            animations.SetWalking(true);
        }
        else if (currentState == MoveState.JustStopped && !isMoving)
        {
            currentState = MoveState.Idle;
            animations.SetWalking(false);
        }
    }

    public Vector3 GetObjectivePosition()
    {
        return objectivePos;
    }

    public void MoveTo(Vector3 position)
    {
        agent.SetDestination(position);
        objectivePos = position;
        currentState = MoveState.StopIdling;
    }

    public void MoveTo(float x, float y, float z)
    {
        MoveTo(new Vector3(x, y, z));
    }

    bool IsMoving()
    {
        return Vector3.Distance(transform.position, objectivePos) > 2.5f;
    }

    public MoveState GetMoveState()
    {
        return currentState;
    }
}
