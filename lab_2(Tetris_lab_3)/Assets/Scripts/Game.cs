using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    public ConfigGame config;
    public ConfigGame Config => config;

    public static int gridWidth = 10;
    public static int gridHeight = 20;
    public static Transform[,] grid = new Transform[gridWidth, gridHeight];
    public int currentLevel = 0;
    public Text level;
    public Text time;
    public Text hud_score;
    public Text finish;
    private int numberOfRawsThisTurn = 0;
    public int currentScore = 0;
    private int scoreTemp = 0;
    private int scoreTempTwo = 0;
    private int temp;
    private int one;
    private int two;
    private int three;
    private int four;
    private int five;


    public int finishScore;

    private GameObject previewtetromino;
    private GameObject nextTetromino;

    public bool gameStart = false;

    private Vector2 PreviewTetrominoPosition = new Vector2(-6.5f, 15);


    void Start()
    {
//        StartCoroutine(StartGame());
        SpawnNextTetromino();
        initScores();
    }

    void Update()
    {
        UpdateScore();
        UpdateUi();
        UpdateLevel();
        UpdateSpeed();
    }


    public void UpdateHighScore()
    {
        if (currentScore > one)
        {
            PlayerPrefs.SetInt("second", one);
            PlayerPrefs.SetInt("third", two);
            PlayerPrefs.SetInt("fourth", three);
            PlayerPrefs.SetInt("fifth", four);
            PlayerPrefs.SetInt("first", currentScore);
            currentScore = 0;
        }
        else if (currentScore > two)
        {
            PlayerPrefs.SetInt("third", two);
            PlayerPrefs.SetInt("fourth", three);
            PlayerPrefs.SetInt("fifth", four);
            PlayerPrefs.SetInt("second", currentScore);
            currentScore = 0;
        }
        else if (currentScore > three)
        {
            PlayerPrefs.SetInt("fourth", three);
            PlayerPrefs.SetInt("fifth", four);
            PlayerPrefs.SetInt("third", currentScore);
            currentScore = 0;
        }
        else if (currentScore > four)
        {
            PlayerPrefs.SetInt("fifth", four);
            PlayerPrefs.SetInt("fourth", currentScore);
            currentScore = 0;
        }
        else if (currentScore > five)
        {
            PlayerPrefs.SetInt("fifth", currentScore);
        }
    }

    void initScores()
    {
        one = PlayerPrefs.GetInt("first");
        two = PlayerPrefs.GetInt("second");
        three = PlayerPrefs.GetInt("third");
        four = PlayerPrefs.GetInt("fourth");
        five = PlayerPrefs.GetInt("fifth");
    }

    private IEnumerator StartGame()
    {
        time.text = "3";
        yield return new WaitForSeconds(1f);
        time.text = "2";
        yield return new WaitForSeconds(1f);
        time.text = "1";
        yield return new WaitForSeconds(1f);
        time.text = "GO!";
        yield return new WaitForSeconds(1f);
        time.text = "";
        SpawnNextTetromino();
    }

    void UpdateLevel()
    {
        currentLevel = currentScore / 500;
    }

    void UpdateSpeed()
    {
        Config.FallSpeed = 1.0f - ((float) currentLevel * 0.1f);
    }

    public void UpdateUi()
    {
        level.text = currentLevel.ToString();
    }

    public void UpdateScore()
    {
        if (numberOfRawsThisTurn > 0)
        {
            if (numberOfRawsThisTurn == 1)
            {
                currentScore += Config.ScoreOneLine;
            }
            else if (numberOfRawsThisTurn == 2)
            {
                currentScore += Config.ScoreTwoLine;
            }
            else if (numberOfRawsThisTurn == 3)
            {
                currentScore += Config.ScoreThreeLine;
            }
            else if (numberOfRawsThisTurn == 4)
            {
                currentScore += Config.ScoreFourLine;
            }

            hud_score.text = currentScore.ToString();
            numberOfRawsThisTurn = 0;
        }
    }

    public bool CheckIsAboveGrid(Tetromino tetromino)
    {
        for (int i = 0; i < gridWidth; i++)
        {
            foreach (Transform mino in tetromino.transform)
            {
                Vector2 pos = RoundVec2(mino.position);
                if (pos.y > gridHeight - 1)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void SpawnNextTetromino()
    {
        if (!gameStart)
        {
            gameStart = true;
            int i = Random.Range(0, Config.Groups.Length);
            nextTetromino = Instantiate(Config.Groups[i],
                new Vector3(5.0f, 20.0f),
                Quaternion.identity);
            previewtetromino = Instantiate(Config.Groups[i],
                PreviewTetrominoPosition,
                Quaternion.identity);
        }
        else
        {
            int i = Random.Range(0, Config.Groups.Length);
            previewtetromino.transform.localPosition = new Vector2(5.0f, 20.0f);
            nextTetromino = previewtetromino;
            nextTetromino.GetComponent<Tetromino>().enabled = true;
            previewtetromino = Instantiate(Config.Groups[i],
                PreviewTetrominoPosition,
                Quaternion.identity);
            previewtetromino.GetComponent<Tetromino>().enabled = false;
        }
    }

    public static Vector2 RoundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x),
            Mathf.Round(v.y));
    }

    public static bool InsideBorder(Vector2 pos)
    {
        return ((int) pos.x >= 0 &&
                (int) pos.x < gridWidth &&
                (int) pos.y >= 0);
    }

    public static void DeleteRow(int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public static void DecreaseRow(int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public static void DecreaseRowsAbove(int y)
    {
        for (int i = y; i < gridHeight; ++i)
            DecreaseRow(i);
    }

    public bool IsRowFull(int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }

        numberOfRawsThisTurn++;
        return true;
    }

    public void DeleteFullRows()
    {
        for (int y = 0; y < gridHeight; ++y)
        {
            if (IsRowFull(y))
            {
                DeleteRow(y);
                DecreaseRowsAbove(y + 1);
                --y;
            }
        }
    }

    public void GameOver()
    {
        UpdateHighScore();
        Application.LoadLevel("GameOver");
    }
}