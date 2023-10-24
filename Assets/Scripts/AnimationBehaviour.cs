using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBehaviour : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        bool man = transform.GetChild(0).name.StartsWith("Man");

        animator = GetComponentInChildren<Animator>();

        if (name.StartsWith("Player"))
            return;

        animator.SetFloat("DancingRandom", Random.Range(0, 13) / 12f);

        float manIdle = new float[] { 0f, 0.3f, 0.6f, 0.8f, 1f }[Random.Range(0, 5)];
        float womanIdle = Random.Range(0, 4) / 3f;
        animator.SetFloat("IdleRandom", man ? manIdle : womanIdle);
        float manTalking = Random.Range(0, 2);
        float womanTalking = Random.Range(0, 3) / 2f;
        animator.SetFloat("TalkingRandom", man ? manTalking : womanTalking);
        animator.SetFloat("ArguingRandom", Random.Range(0, 2));
    }

    public void SetWalking(bool walking)
    {
        animator.SetBool("Walking", walking);
    }

    public void SetDancing(bool dancing)
    {
        animator.SetBool("Dancing", dancing);
    }

    public void SetTalking(bool talking)
    {
        animator.SetBool("Talking", talking);
    }

    public void SetArguing(bool arguing)
    {
        animator.SetBool("Arguing", arguing);
    }

    public void SetDrunk(bool drunk)
    {
        animator.SetBool("Drunk", drunk);
    }

    public void TriggerElectrocute()
    {
        animator.SetTrigger("Electrocute");
        animator.SetBool("Dead", true);
    }
}
