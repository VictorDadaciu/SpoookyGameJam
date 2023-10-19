using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBehaviour : MonoBehaviour
{
    Animator animator;
    MoveCharacter moveCharacter;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        moveCharacter = GetComponent<MoveCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Walking", moveCharacter.IsMoving());
    }
}
