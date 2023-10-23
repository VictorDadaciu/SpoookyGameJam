using System.Collections;
using UnityEngine;
using TMPro; // Include the TMPro namespace

public class RoomManager : MonoBehaviour
{
    int currentPuzzle = 0;
    int puzzleTimer = 30;
    float countdownThreshold = 5f;

    public TextMeshProUGUI puzzleTimerText;

    void Start()
    {
        StartPuzzle(currentPuzzle);
    }

    void StartPuzzle(int puzzleNumber)
    {
        puzzleTimerText.text = "";
        StartCoroutine(PuzzleTimer(puzzleTimer));
    }

    void PuzzleCompleted()
    {
        currentPuzzle++;
        if (currentPuzzle >= 6)
        {
            Debug.Log("All puzzles completed!");
        }
        else
        {
            StartPuzzle(currentPuzzle);
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