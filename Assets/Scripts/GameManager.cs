using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("This is the Player's game object.")]
    private PlayerController Player;

    // This boolean keeps track of if the game is paused or not.
    private bool IsPaused;

    // This is just set to the game's time scale, so that it doesn't get overwritten.
    private float TIME_SCALE;

    void Start()
    {
        IsPaused = false;
        TIME_SCALE = Time.timeScale;
    }

    void Update()
    {
        // This handles pausing the game, constantly checking if the player is pressing "P" to pause the game or not.
        if (Input.GetKeyDown(KeyCode.P))
            SwitchPaused();
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
            break;

            default:
            Time.timeScale = TIME_SCALE;
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