using UnityEngine.Audio;
using UnityEngine;

public class AudioDriver : MonoBehaviour
{
    public Sound[] sound;
    
    string PuzzleAudioPath = "GameAudio/PuzzleAudio/";
    string SceneAudioPath = "GameAudio/SceneAudio/";

    void Start() 
    {
        //if (<something>) playAudio(<file>);
        // Commented out because it was giving compile errors.
    }
}