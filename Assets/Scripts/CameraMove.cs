using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float acceleration;
    public float maxVelocity;
    public float dampening;

    Vector3 velocity = Vector3.zero;

    void Update()
    {
        velocity *= dampening;
        if (velocity.sqrMagnitude < 0.1f)
            velocity = Vector3.zero;

        Vector3 direction = Vector3.Normalize(new Vector3(HandleMovementDirection(Configs.actions[Actions.MoveCamRight]) - HandleMovementDirection(Configs.actions[Actions.MoveCamLeft]), 0f,
            HandleMovementDirection(Configs.actions[Actions.MoveCamFwd]) - HandleMovementDirection(Configs.actions[Actions.MoveCamBack])));

        direction *= acceleration;
        velocity = new Vector3(Mathf.Clamp(direction.x + velocity.x, -maxVelocity, maxVelocity), 0f, Mathf.Clamp(direction.z + velocity.z, -maxVelocity, maxVelocity));

        transform.Translate(velocity * Time.deltaTime);
    }

    float HandleMovementDirection(KeyCode keyCode)
    {
        return Input.GetKey(keyCode) ? 1f : 0f;
    }
}
