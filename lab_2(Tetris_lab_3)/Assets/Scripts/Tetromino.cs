using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    public ConfigGame config;

    public ConfigGame Config => config;

    float fall = 0;
    private float continuonsVerticalSpeed = 0.05f;
    private float continuonsHorizonatalSpeed = 0.1f;
    private float buttonDownWaitMax = 0.2f;

    private float verticalTimer = 0;
    private float horizontalTimer = 0;
    private float buttonDownWaitTimerHorizontal = 0;
    private float buttonDownWaitTimerVertical = 0;


    private bool moveImmediateHorizontal = false;
    private bool movedImediteVertical = false;

    void Update()
    {
        if (!PauseMenu.gameIsPaused)
        {
            CheckUserInput();
        }
    }

    void CheckUserInput()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moveImmediateHorizontal = false;
            horizontalTimer = 0;
            buttonDownWaitTimerHorizontal = 0;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            movedImediteVertical = false;
            verticalTimer = 0;
            buttonDownWaitTimerVertical = 0;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Right();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Left();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Up();
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Time.time - fall >= Config.FallSpeed)
        {
            Down();
        }
    }

    void Left()
    {
        if (moveImmediateHorizontal)
        {
            if (buttonDownWaitTimerHorizontal < buttonDownWaitMax)
            {
                buttonDownWaitTimerHorizontal += Time.deltaTime;
                return;
            }

            if (horizontalTimer < continuonsHorizonatalSpeed)
            {
                horizontalTimer += Time.deltaTime;
                return;
            }
        }

        if (!moveImmediateHorizontal)
        {
            moveImmediateHorizontal = true;
        }

        horizontalTimer = 0;

        transform.position += new Vector3(-1, 0, 0);
        if (IsValidGridPos())
        {
            UpdateGrid();
        }
        else
        {
            transform.position += new Vector3(1, 0, 0);
        }
    }

    void Right()
    {
        if (moveImmediateHorizontal)
        {
            if (buttonDownWaitTimerHorizontal < buttonDownWaitMax)
            {
                buttonDownWaitTimerHorizontal += Time.deltaTime;
                return;
            }

            if (horizontalTimer < continuonsHorizonatalSpeed)
            {
                horizontalTimer += Time.deltaTime;
                return;
            }
        }

        if (!moveImmediateHorizontal)
        {
            moveImmediateHorizontal = true;
        }

        horizontalTimer = 0;
        transform.position += new Vector3(1, 0, 0);
        if (IsValidGridPos())
        {
            UpdateGrid();
        }
        else
        {
            transform.position += new Vector3(-1, 0, 0);
        }
    }

    void Up()
    {
        transform.Rotate(0, 0, 90);
        if (IsValidGridPos())
        {
            UpdateGrid();
        }
        else
        {
            transform.Rotate(0, 0, -90);
        }
    }

    void Down()
    {
        if (movedImediteVertical)
        {
            if (buttonDownWaitTimerVertical < buttonDownWaitMax)
            {
                buttonDownWaitTimerVertical += Time.deltaTime;
                return;
            }

            if (verticalTimer < continuonsVerticalSpeed)
            {
                verticalTimer += Time.deltaTime;
                return;
            }
        }

        if (!movedImediteVertical)
        {
            movedImediteVertical = true;
        }

        verticalTimer = 0;


        transform.position += new Vector3(0, -1, 0);
        if (IsValidGridPos())
        {
            UpdateGrid();
        }
        else
        {
            transform.position += new Vector3(0, 1, 0);
            FindObjectOfType<Game>().DeleteFullRows();

            if (FindObjectOfType<Game>().CheckIsAboveGrid(this))
            {
                FindObjectOfType<Game>().GameOver();
            }

            FindObjectOfType<Game>().SpawnNextTetromino();


            enabled = false;
        }

        fall = Time.time;
    }


    bool IsValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Game.RoundVec2(child.position);

            if (!Game.InsideBorder(v))
                return false;

            if (Game.grid[(int) v.x, (int) v.y] != null &&
                Game.grid[(int) v.x, (int) v.y].parent != transform)
                return false;
        }

        return true;
    }

    void UpdateGrid()
    {
        for (int y = 0; y < Game.gridHeight; ++y)
        for (int x = 0; x < Game.gridWidth; ++x)
            if (Game.grid[x, y] != null)
                if (Game.grid[x, y].parent == transform)
                    Game.grid[x, y] = null;

        foreach (Transform child in transform)
        {
            Vector2 v = Game.RoundVec2(child.position);
            Game.grid[(int) v.x, (int) v.y] = child;
        }
    }
}