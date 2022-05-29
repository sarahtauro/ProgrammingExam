using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public static ScoreManager instance;

    int Score = 0;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        scoreText.text = Score.ToString() + " Points";
    }

    public void AddPoint()
    {
        Score += 1;
        scoreText.text = Score.ToString() + " Points";
    }
}
