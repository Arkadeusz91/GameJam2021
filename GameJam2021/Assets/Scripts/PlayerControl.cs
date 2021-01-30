using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    //wektor potrzebny do obrotu przedmiotu
    public Vector3 rotationPoint;
    //zmienna potrzebna do odliczania czasu opadniecia klocka
    private float ItIsTime;
    //czas z jakim b�dzie opada� klocek
    public float TimeToFall = 0.91f - ((float)Level_Script.YourLevel * 0.04f);
    //

    public static int StageWidth = 20;
    public static int StageHeight = 40;
    public static Transform[,] grid = new Transform[StageWidth, StageHeight];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (checkCompleteLines())
        {
            case 0:
                break;
            case 1:
                Score_Counter.YourScore += 20;
                break;
            case 2:
                Score_Counter.YourScore += 50;
                break;
            case 3:
                Score_Counter.YourScore += 80;
                break;
            default:
                Score_Counter.YourScore += 120;
                break;
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-2, 0, 0);
            if(!StopThisMove())
                    transform.position += new Vector3(2, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(2, 0, 0);
            if (!StopThisMove())
                    transform.position += new Vector3(-2, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 2), 90);
            if (!StopThisMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 2), -90);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, -2, 0);
            Score_Counter.YourScore += 15;
            if (!StopThisMove())
            {
                transform.position += new Vector3(0, 2, 0);
                this.enabled = false;
                AddToGrid();
                FindObjectOfType<Generator>().generate();
            }
                
        }

        if (Time.time - ItIsTime > TimeToFall)
        {
            transform.position += new Vector3(0, -2, 0);
            Score_Counter.YourScore += 10;
            ItIsTime = Time.time;
            if (!StopThisMove()) 
            {
                transform.position += new Vector3(0, 2, 0);
                AddToGrid();
                this.enabled = false;
                FindObjectOfType<Generator>().generate();
            }
                    
        }
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int locationX = Mathf.RoundToInt(children.transform.position.x);
            int locationY = Mathf.RoundToInt(children.transform.position.y);

            if (locationY >= StageHeight-2)
            {
                SceneManager.LoadSceneAsync("GameOver",LoadSceneMode.Single);
            }

            grid[locationX, locationY] = children;
        }
    }

    //skrypt na podstawie bool ktory bedzie sprawdzal czy nasz tetrimino wykracza po za stage (nie udalo mi sie z kolizjami)
    bool StopThisMove()
    {
        foreach (Transform children in transform)
        {
            int locationX = Mathf.RoundToInt(children.transform.position.x);
            int locationY = Mathf.RoundToInt(children.transform.position.y);

            if (locationX < 0 || locationX >= StageWidth || locationY < 0 || locationY >= StageHeight)
            {
                return false;
            }

            if (grid[locationX, locationY] != null)
                return false;
        }

        return true;
    }

    int checkCompleteLines()
    {
        int completedLines = 0;
        for (int i = 0; i < StageHeight; i+=2)
        {
            if (lineComplete(i))
            {
                completedLines += 1;
                deleteLine(i);
                dropBlocks(i);
            }
        }
		return completedLines;
    }
    
    bool lineComplete(int i)
    {
        for (int j = 0; j < StageWidth; j += 2)
        {
            if (grid[j,i] == null)
            {
                return false;
            }
        }
    
        return true;
    }

    void deleteLine(int i)
    {
        for (int j = 0; j < StageWidth; j += 2)
        {
            Destroy(grid[j,i].gameObject);
            grid[j,i] = null;
        }
    }

    void dropBlocks(int i)
    {
        for (int j = i; j < StageHeight; j+=2)
        {
            for (int k = 0; k < StageWidth; k+=2)
            {
                if (grid[k, j] != null)
                {
                    grid[k, j-2] = grid[k, j];
                    grid[k, j] = null;
                    grid[k, j - 2].transform.position -= new Vector3(0, 2, 0);
                }
            }
        }
    }

}
