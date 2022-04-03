using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("This is the Player's game object.")]
    private PlayerController Player;

    [SerializeField]
    [Tooltip("This is the pause menu.")]
    private Canvas PauseMenuCanvas;

    [SerializeField]
    [Tooltip("This is just for testing purposes and SHOULD BE REMOVED BEFORE THE FINAL BUILD.")]
    private TextMeshProUGUI SprintingDebugText;

    [SerializeField]
    [Tooltip("This is the camera attached to the player.")]
    private Camera PlayerCamera;

    [SerializeField]
    [Tooltip("This is the camera that views the whole area (only used in some levels).")]
    private Camera AerialCamera;

    // This boolean keeps track of if the game is paused or not.
    private bool IsPaused;

    private bool PuzzleSolvedYet;

    // This is just set to the game's time scale, so that it doesn't get overwritten.
    private float TIME_SCALE;
    Ray MouseClick;

    void Start()
    {
        IsPaused = false;
        TIME_SCALE = Time.timeScale;
        PauseMenuCanvas.enabled = false;
    }

    public bool GetPuzzleSolvedYet()
    {
        return PuzzleSolvedYet;
    }

    void Update()
    {
        // This handles pausing the game, constantly checking if the player is pressing "P" to pause the game or not.
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            SwitchPaused();

        float PlayerSpeed = Player.GetSpeed(), PlayerInitialSpeed = Player.GetInitialSpeed();

        if ( Input.GetMouseButtonDown(0))
        {
            MouseClick = AerialCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hit2 = Physics.Raycast(MouseClick, out hit, 100.0f);
            if ( hit.collider.gameObject.CompareTag("ChessPiece"))
            {
                ChessPiece temp = hit.collider.gameObject.GetComponent<ChessPiece>();
                Debug.Log(temp.GetCurrentXPosition() + ", " + temp.GetCurrentYPosition());
                temp.FindValidMoves();
            }
            // Debug.DrawRay(MouseClick.origin, MouseClick.direction * 15, Color.red, 100f);
            // Debug.Log(hit.collider.gameObject.name);
        }
        
        if ( IsPaused )
        {
            SprintingDebugText.text = "Game is paused.";
        }
        else if ( PlayerSpeed > PlayerInitialSpeed )
        {
            SprintingDebugText.text = "Sprinting!";
        }
        else if ( PlayerSpeed < PlayerInitialSpeed )
        {
            SprintingDebugText.text = "Crouching!";
        }
        else 
        {
            SprintingDebugText.text = "Walking normally.";
        }
    }

    // This quits the game.
    public void Quit()
    {
        Application.Quit();
    }

    public void SwitchPaused()
    {
        // This changes "IsPaused" from false to true, or from true to false, in one line of code.
        IsPaused = !IsPaused;

        // This checks the "IsPaused" variable and sees if it's true or false, and sets Time.timeScale accordingly.
        switch (IsPaused)
        {
            case true:
            Time.timeScale = 0;
            PauseMenuCanvas.enabled = true;
            break;

            default:
            Time.timeScale = TIME_SCALE;
            PauseMenuCanvas.enabled = false;
            break;
        }
    }

    // This just gets the "IsPaused" variable so it can be used in other scripts
    // while still being protected.
    public bool GetIsPaused()
    {
        return IsPaused;
    }
}