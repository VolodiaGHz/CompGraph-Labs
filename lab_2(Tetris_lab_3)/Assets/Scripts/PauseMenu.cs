using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    public Text timeToStart;


    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Game>().gameStart == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
//            GameIsPaused = !GameIsPaused;
                if (gameIsPaused == true)
                {
                    StartCoroutine(Resume());
                }
                else if (gameIsPaused == false)
                {
                    Pause();
                    gameIsPaused = true;
                }
            }
        }
    }


    IEnumerator Resume()
    {
        timeToStart.text = "3";
        yield return new WaitForSeconds(1f);
        timeToStart.text = "2";
        yield return new WaitForSeconds(1f);
        timeToStart.text = "1";
        yield return new WaitForSeconds(1f);
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        timeToStart.text = " ";
    }
}