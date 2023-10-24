using System.Collections;
using UnityEngine;
using TMPro; 

public class PuzzleManager : MonoBehaviour
{
    [Header("Puzzle Data")]
    [SerializeField]
    private int currentPuzzle = 1;
    private int puzzleTimer = 10;
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
    [SerializeField] public AIAgent[] kissers; 

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

        if (puzzleNumber == 3)
        {
            foreach (AIAgent agent in arguers)
            {
                agent.Leave(true, true);
                agent.GoTo(locations[5]);
            }
        } 
        else if (puzzleNumber == 2)
        {
            drunkClown.Leave(true, true);
            drunkClown.GoTo(locations[6]);
        }
        else if (puzzleNumber == 1)
        {
            foreach (AIAgent agent in kissers)
            {
                agent.Leave(true, true);
                agent.GoTo(locations[5]);
            }
        }
        else if (puzzleNumber == 4)
        {
            target.Kill();
            return;
        }
        SendTargetTo(puzzleNumber);
    }

    void SendTargetTo(int puzzleNumber) 
    {
        target.Leave(true, true);
        target.GoTo(locations[puzzleNumber]);
    }
}