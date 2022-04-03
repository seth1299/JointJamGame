using UnityEngine;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("This is how fast the player moves.")]
    private float Speed;

    // SprintingSpeed is how fast the character moves while sprinting and CrouchingSpeed is how fast the character moves while crouching. InitialSpeed is just Speed but not ever modified.
    private float SprintingSpeed, CrouchingSpeed, InitialSpeed;

    [SerializeField]
    [Tooltip("This is how many lives the player has.")]
    private int Lives;
    
    [SerializeField]
    [Tooltip("This is the Character Controller for the player's character.")]
    private CharacterController Controller;

    [SerializeField]
    [Tooltip("This is the Game Manager that controls a lot of stuff for the game.")]
    private GameManager Game_Manager;

    // This changes the player's lives by whatever value is passed into the function.
    public void ChangeLives(int amount)
    {
        Lives += amount;
    }

    // This sets the player's lives to the value passed into the function.
    public void SetLives(int amount)
    {
        Lives = amount;
    }

    // This just gets the "Lives" variable so it can be used in other scripts
    // while still being protected.
    public int GetLives()
    {
        return Lives;
    }

    void Start()
    {
        SprintingSpeed = Speed * 2;
        CrouchingSpeed = Speed / 2;
        InitialSpeed = Speed;
    }

    void Update()
    {
        // This script will only run if the game isn't currently paused.
        if ( !Game_Manager.GetIsPaused() )
        {
            // This sets the player's horizontal and vertical bearings to variables 
            // to simplify future code that uses them. It also makes a Vector3 variable
            // called "move" that is the orientation the player will be moving towards.
            float x = Input.GetAxis ("Horizontal");
            float z = Input.GetAxis ("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;

            // This handles the player's speed while sprinting and crouching.
            if ( Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) )
            {
                Speed = SprintingSpeed;
            }
            else if ( Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) ) 
            {
                Speed = CrouchingSpeed;
            }
            else
            {
                Speed = InitialSpeed;
            }

            // Using a Character Controller game component, this piece of code moves
            // the character very easily using the Speed value multiplied by deltaTime,
            // which is the amount of time that has passed from the previous frame to
            // the current one.
            Controller.SimpleMove(move * Speed * Time.deltaTime);

        }
        
    }

    public float GetSpeed()
    {
        return Speed;
    }

    public float GetInitialSpeed()
    {
        return InitialSpeed;
    }
}