using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UndestroyableGameObject : MonoBehaviour
{
    void Awake()
        {
            // Rename this to whatever you name the first scene, which must be order 0 in the Build Index.
            if (SceneManager.GetActiveScene().name == "AudioManagerScene")
            {
                SceneManager.LoadScene("MainMenu");
            }

            DontDestroyOnLoad(this);
        }
}