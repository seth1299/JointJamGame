using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private Canvas Main_Menu_Canvas, Credits_Menu_Canvas;
    [SerializeField]
    private Canvas[] Menus;

    void Start()
    {
        foreach (Canvas Menu in Menus)
        {
            if ( Menu.CompareTag("MainMenu") )
            {
                Menu.enabled = true;
            }
            else
            {
                Menu.enabled = false;
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void MainMenu()
    {
        SwitchMenu(1);
    }

    public void CreditsMenu()
    {
        SwitchMenu(2);
    }

    public void SwitchMenu(int value)
    {
        switch (value)
        {
            case 1:
            Main_Menu_Canvas.enabled = true;
            Credits_Menu_Canvas.enabled = false;
            break;

            default:
            Main_Menu_Canvas.enabled = false;
            Credits_Menu_Canvas.enabled = true;
            break;

        }
    }
}