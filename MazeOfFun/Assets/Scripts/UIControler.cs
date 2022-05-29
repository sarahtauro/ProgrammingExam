using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControler : MonoBehaviour
{

    public GameObject activeMenu;


    public void AdjustDifficulty(Slider difficultySlider)
    {
        MazeCreator.level = (int)difficultySlider.value;
    }

    public void AdjustVolume(Slider volumeSlider)
    {
        TestSoundManager.Volume = volumeSlider.value;
    }

    public void OpenMenu(GameObject menu)
    {
        activeMenu.SetActive(false);
        menu.SetActive(true);
        activeMenu = menu;
    }


    public void Exit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene("GameArea");
    }
}
