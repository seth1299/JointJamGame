using UnityEngine;
using System;
using System.Collections;

public class MazePlayerControl : MonoBehaviour
{
    [SerializeField]
    [Tooltip("This is how fast the player moves.")]
    private float speed;

    void Start()
    {
    }

    void Update()
    {
        // This script will only run if the game isn't currently paused.
        // This sets the player's horizontal and vertical bearings to variables 
        // to simplify future code that uses them. It also makes a Vector3 variable
        // called "move" that is the orientation the player will be moving towards.
        float x = Input.GetAxis ("Horizontal");
        float z = Input.GetAxis ("Vertical");
        Vector3 move = transform.right * x + transform.forward * z ;
        transform.position += move * speed;
        // Using a Character Controller game component, this piece of code moves
        // the character very easily using the Speed value multiplied by deltaTime,
        // which is the amount of time that has passed from the previous frame to
        // the current one.
        
    }
}