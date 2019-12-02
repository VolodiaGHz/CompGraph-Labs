using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    public Text gmOverScore;

    private void Start()
    {
    }

    public void PlayAgaine()
    {
        Application.LoadLevel("Level");
    }

    public void MainMenu()
    {
        Application.LoadLevel("MainMenu");
    }

    public void HighScore()
    {
        Application.LoadLevel("HighScore");
    }
}