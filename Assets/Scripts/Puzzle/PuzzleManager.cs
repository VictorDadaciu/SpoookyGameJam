using System.Collections;
using UnityEngine;
using TMPro; 

public class PuzzleManager : MonoBehaviour
{
    [Header("Puzzle Data")]
    [SerializeField]
    private int currentPuzzle = 1;
    private int puzzleTimer = 30;
    [SerializeField]
    float countdownThreshold = 5f;

    [Header("TODO UI Data")]
    public TextMeshProUGUI puzzleTimerText;
    
    private string todo1 = "1. Go to Master Bedroom";
    private string todo2 = "2. Go to the Storage Room";
    private string todo3 = "3. Go to the Kitchen";
    
    [SerializeField]
    private TextMeshProUGUI todoItem1Text;
    [SerializeField]
    private TextMeshProUGUI todoItem2Text;
    [SerializeField]
    private TextMeshProUGUI todoItem3Text;
    
    [Header("Dialogue Data")]
    [SerializeField]
    private DialogueGiver[] dialogueGivers;

    public AIAgent target;
    [SerializeField] public LocationOfInterest[] locations;
    [SerializeField] public AIAgent[] arguers;
    public AIAgent drunkClown;

    void Start()
    
    {
        todoItem1Text.text = todo1;
        todoItem2Text.text = todo2;
        todoItem3Text.text = todo3;
        
        if (dialogueGivers.Length > 0)
        {
            // Call the StartDialogue function on the first element of the array
            dialogueGivers[0].Send();
        }
        else
        {
            Debug.LogWarning("No DialogueGiver scripts assigned to the array.");
        }
    }

    public void StartPuzzle(int puzzleNumber)
    {
        //puzzleTimerText.text = "";
        //StartCoroutine(PuzzleTimer(puzzleTimer));
        SendTargetTo(puzzleNumber);

        if (puzzleNumber == 3)
        {
            foreach (AIAgent agent in arguers)
            {
                agent.GoTo(locations[5]);
            }
        } 
        else if (puzzleNumber == 2)
        {
            drunkClown.GoTo(locations[6]);
        }
    }

    void SendTargetTo(int puzzleNumber) 
    {
        Debug.Log("Sending target to puzzle no "+currentPuzzle);
        target.GoTo(locations[puzzleNumber]);
    }

    public void PuzzleCompleted()
    {
        currentPuzzle++;
        Debug.Log("Currently in puzzle no "+currentPuzzle);
        if (currentPuzzle >= 6)
        {
            Debug.Log("All puzzles completed!");
            //TODO show death?
            //TODO roll credits
        }
        else
        {
            Debug.Log("Starting puzzle");
            StartPuzzle(currentPuzzle);
        }

        // Update TODO list based on the currentPuzzle
        if (currentPuzzle == 3)
        {
            //TODO add dialog
            todoItem1Text.text = "<s>" + todo1 + "</s>"; 
        }
        else if (currentPuzzle == 5)
        {
            //TODO add dialog
            todoItem2Text.text = "<s>" + todo2 + "</s>";
        }
        else if (currentPuzzle == 6)
        {
            //TODO add dialog
            todoItem3Text.text = "<s>" + todo3 + "</s>";
        }
    }

    void PuzzleFailed()
    {
        Debug.Log("Puzzle failed! Restarting...");
        StartPuzzle(0);
    }

    IEnumerator PuzzleTimer(int time)
    {
        int  timer = time;

        while (timer >= 0)
        {
            if (timer <= countdownThreshold)
            {
                string colorCode = "white";

                if (timer <= 15 && timer >= 6)
                    colorCode = "orange";
                else if (timer <= 5)
                    colorCode = "red";

                puzzleTimerText.text = $"<color={colorCode}><b>{timer:F0}</b></color>";
            }

            timer --;
            yield return new WaitForSeconds(1f);
        }

        //PuzzleFailed();
    }
}