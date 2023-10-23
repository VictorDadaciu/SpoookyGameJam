using UnityEngine;
using UnityEngine.UI;

public class DebugButton : MonoBehaviour
{
    // Reference to the PuzzleManager script
    [SerializeField] private PuzzleManager puzzleManager;

    private Button debugButton;

    void Start()
    {
        // Get a reference to the Button component on this GameObject
        debugButton = GetComponent<Button>();

        // Add an onClick listener to the button to call PuzzleCompleted
        debugButton.onClick.AddListener(CallPuzzleCompleted);
    }

    // Function to call the PuzzleCompleted function
    void CallPuzzleCompleted()
    {
        // Check if the PuzzleManager script is assigned
        if (puzzleManager != null)
        {
            // Call the PuzzleCompleted function
            puzzleManager.PuzzleCompleted();
        }
    }
}
