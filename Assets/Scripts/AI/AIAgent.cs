using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AIAgent : MonoBehaviour
{
    bool busy = false;
    MoveCharacter moveCharacter;
    Queue<Pair<ActivityType, float>> burnedOutActivities;
    LocationOfInterest location;
    Vector3 actualPosition;
    float activityTimer;
    int timesRenewed;
    

    bool lastFrameWasMoving;

    // Start is called before the first frame update
    void Start()
    {
        moveCharacter = GetComponent<MoveCharacter>();
        burnedOutActivities = new Queue<Pair<ActivityType, float>>();
        timesRenewed = 0;
        lastFrameWasMoving = false;
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

        if (busy && !InLine() && !moveCharacter.IsMoving())
        {
            if (lastFrameWasMoving)
            {
                StartTimer();
            }
            else
            {
                activityTimer -= Time.deltaTime;
                if (activityTimer <= 0)
                {
                    Leave();
                }
            }

        }
        lastFrameWasMoving = moveCharacter.IsMoving();
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

    public Vector3 GetActualPosition()
    {
        return actualPosition;
    }

    public void SetObjective(Vector3 position)
    {
        actualPosition = position;
        moveCharacter.MoveTo(position);
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
        location = loc;
        SetObjective(loc.GetPosition());
        loc.AddAgent(this);
        busy = true;
        lastFrameWasMoving = true;
    }

    bool TryAndRenew()
    {
        if (location.renewable)
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

    public void Leave()
    {
        if (!InLine() && TryAndRenew())
        {
            StartTimer();
            return;
        }

        if (!InLine())
            burnedOutActivities.Enqueue(new Pair<ActivityType, float>(location.activityType, Random.Range(5f, 10f)));
        location.FinishedObjective(this);
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
}
