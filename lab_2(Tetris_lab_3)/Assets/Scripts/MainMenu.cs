﻿using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.UI;
 
 public class MainMenu : MonoBehaviour
 {
     public void StartGame()
     {
         Application.LoadLevel("Level");
     }
 
     public void HighScore()
     {
         Application.LoadLevel("HighScore");
     }
 }