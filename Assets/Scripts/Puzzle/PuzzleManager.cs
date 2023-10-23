using System.Collections;
using UnityEngine;
using TMPro; 

public class PuzzleManager : MonoBehaviour
{
    [Header("Puzzle Data")]
    [SerializeField]
    private int currentPuzzle = 1;
    private int puzzleTimer = 30;
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
        
        //TODO start puzzle once tutorial completed
        StartPuzzle(currentPuzzle);
    }

    void StartPuzzle(int puzzleNumber)
    {
        puzzleTimerText.text = "";
        StartCoroutine(PuzzleTimer(puzzleTimer));
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
                string colorCode = "green";

                if (timer <= 4 && timer >= 3)
                    colorCode = "orange";
                else if (timer <= 2)
                    colorCode = "red";

                puzzleTimerText.text = $"Time left: <color={colorCode}>{timer:F0} sec</color>";
            }

            timer --;
            yield return new WaitForSeconds(1f);
        }

        PuzzleFailed();
    }
}