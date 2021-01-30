using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //wektor potrzebny do obrotu przedmiotu
    public Vector3 rotationPoint;
    //zmienna potrzebna do odliczania czasu opadniecia klocka
    private float ItIsTime;
    //czas z jakim b�dzie opada� klocek
    public float TimeToFall = 0.8f;
    //
    
    public static int StageWidth = 20;
    public static int StageHeight = 40;
    private static Transform[,] grid = new Transform[StageWidth, StageHeight];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

}
