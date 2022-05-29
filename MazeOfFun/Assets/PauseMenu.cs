using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public TextMeshProUGUI text;


    public void PauseOrResumeGame()
    {
        GameIsPaused = !GameIsPaused;
    
        // Change the text of the code
        if(GameIsPaused)
        {
            text.text = "Resume";
        }
        else
        {
            text.text = "Pause";
        }
    }
}
