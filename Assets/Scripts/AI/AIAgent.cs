using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AIAgent : MonoBehaviour
{
    bool busy = false;
    MoveCharacter moveCharacter;
    AnimationBehaviour animations;
    Queue<Pair<ActivityType, float>> burnedOutActivities;
    LocationOfInterest location;
    float activityTimer;
    int timesRenewed;
    

    // Start is called before the first frame update
    void Start()
    {
        moveCharacter = GetComponent<MoveCharacter>();
        animations = GetComponent<AnimationBehaviour>();
        burnedOutActivities = new Queue<Pair<ActivityType, float>>();
        timesRenewed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var pair in burnedOutActivities)
        {
            pair.item2 -= Time.deltaTime;
            if (pair.item2 <= 0)
            {
                burnedOutActivities.Dequeue();
                break;
            }
        }

        if (busy && !InLine())
        {
            if (moveCharacter.GetMoveState() == MoveState.JustStopped)
            {
                StartTimer();
                animations.SetDancing(location.activityType == ActivityType.Dance);
                animations.SetTalking(location.activityType == ActivityType.Talk);
                animations.SetArguing(location.activityType == ActivityType.Argue);
            }
            else if (moveCharacter.GetMoveState() == MoveState.Idle)
            {
                activityTimer -= Time.deltaTime;
                if (activityTimer <= 0)
                {
                    Leave();
                }
            }
        }

        if (busy && moveCharacter.GetMoveState() == MoveState.Idle)
        {
            RotateTowards(location.GetLookAt());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.4f);
    }

    void RotateTowards(Vector3 target)
    {
        target.y = 0f;
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, 0f);
    }

    public bool CanGoTo(LocationOfInterest location)
    {
        foreach (var pair in burnedOutActivities)
        {
            if (pair.item1 == location.activityType)
                return false;
        }
        return true;
    }

    public Vector3 GetObjectivePosition()
    {
        return moveCharacter.GetObjectivePosition();
    }

    public bool IsBusy()
    {
        return busy;
    }

    void StartTimer()
    {
        StartTimer(location.timeToSpend);
    }

    void StartTimer(Vector2 range)
    {
        activityTimer = Random.Range(range.x, range.y);
    }

    void StartTimer(float x, float y)
    {
        StartTimer(new Vector2(x, y));
    }

    public void GoTo(LocationOfInterest loc)
    {
        animations.SetDancing(false);
        animations.SetArguing(false);
        animations.SetTalking(false);
        location = loc;
        moveCharacter.MoveTo(loc.GetPosition());
        loc.AddAgent(this);
        busy = true;
    }

    bool TryAndRenew()
    {
        if (location != null && location.renewable)
        {
            float renewChance = timesRenewed * 0.25f + 0.3f;
            float renewRoll = Random.Range(0f, 1f);
            if (renewRoll > renewChance)
            {
                timesRenewed++;
                return true;
            }
        }
        return false;
    }

    public void Leave(bool noBurnout=false, bool force=false)
    {
        if (!InLine() && TryAndRenew() && !force)
        {
            StartTimer();
            return;
        }

        if (!InLine() && !noBurnout)
        {
            burnedOutActivities.Enqueue(new Pair<ActivityType, float>(location.activityType, location.burnoutTime));
        }

        if (location != null)
        {
            location.FinishedObjective(this);
        }
        location = null;
        busy = false;
        timesRenewed = 0;
    }

    public bool InLine()
    {
        if (location != null && location.next != null)
            return true;

        return false;
    }

    public void Kill()
    {
        animations.TriggerElectrocute();
        enabled = false;
        moveCharacter.enabled = false;
        StartCoroutine(LoadCreditsSceneAsync());
        
    }
    
    IEnumerator LoadCreditsSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("CreditsScene");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
