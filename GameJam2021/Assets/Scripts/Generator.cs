using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    public GameObject[] tetriminoArray;

    public bool isNothingControlled = true;

    private bool first = true;

    public GameObject nextTetrimino = null;
    
    public int disabledObjectId = 0;

    public GameObject disabledObject = null;

    public int lastLevel = 1;
    
    int invisibleId = 1;

    // Start is called before the first frame update
    void Start()
    {
        disabledObjectId = Random.Range(0, tetriminoArray.Length);
        disabledObject = Instantiate(tetriminoArray[disabledObjectId]);
        disabledObject.transform.position += new Vector3(18, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        int random = Random.Range(1, 10);
        int currentLevel = Level_Script.YourLevel;
        if (currentLevel>=30)
        {
            if (currentLevel>lastLevel)
            {
                ReappearBlocks(invisibleId);
                invisibleId = Random.Range(1, 8);
            }
            InvisibleBlock(invisibleId);
        }
        if (currentLevel > lastLevel)
        {
            lastLevel = currentLevel;
            Destroy(disabledObject);
            disabledObjectId = Random.Range(0, tetriminoArray.Length);
            disabledObject = Instantiate(tetriminoArray[disabledObjectId]);
            disabledObject.transform.position += new Vector3(18, 0, 0);
            disableInterface(random);
        }
        if (isNothingControlled)
        {
            generate();
        }
    }

    

    public void generate()
    {
        int random = Random.Range(0, tetriminoArray.Length);
        while (true)
        {
            if (!random.Equals(disabledObjectId))
            {
                break;
            }

            random = Random.Range(0, tetriminoArray.Length);
        }
        if (first)
        {
            nextTetrimino = Instantiate(tetriminoArray[random]);
            first = false;
        }
        nextTetrimino.transform.position=new Vector3(10, 32, 0);
        nextTetrimino.AddComponent<PlayerControl>();
        random = Random.Range(0, tetriminoArray.Length - 1);
        while (true)
        {
            if (!random.Equals(disabledObjectId))
            {
                break;
            }

            random = Random.Range(0, tetriminoArray.Length);
        }
        nextTetrimino = Instantiate(tetriminoArray[random]);
        isNothingControlled = false;
    }
    
    

    public void disableInterface(int id)
    {
        GameObject.Find("/Canvas/Score_Text").GetComponent<Text>().enabled=true;
        GameObject.Find("/Canvas/Level_Text").GetComponent<Text>().enabled=true;
        GameObject.Find("/Canvas/Lost_Tetrimino_Text").GetComponent<Text>().enabled = true;
        GameObject.Find("/Canvas/Next_Tetrimino_Text").GetComponent<Text>().enabled = true;
        foreach (MeshRenderer renderer in disabledObject.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.enabled = true;
        }
        GameObject.Find("Frame01").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("Frame02").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("Stage").GetComponent<MeshRenderer>().enabled = true;
        if (Level_Script.YourLevel >= 10)
        {
            switch (id)
        	{
          	  case 1:
          	      GameObject.Find("/Canvas/Score_Text").GetComponent<Text>().enabled = false;
          	      break;
          	  case 2:
         	       GameObject.Find("/Canvas/Level_Text").GetComponent<Text>().enabled = false;
         	       break;
              case 3:
                  // foreach (MeshRenderer renderer in nextTetrimino.GetComponentsInChildren<MeshRenderer>())
                  // {
                  //     renderer.enabled = false;
                  // }
                  // break;
              case 4:
                  foreach (MeshRenderer renderer in disabledObject.GetComponentsInChildren<MeshRenderer>())
                  {
                      renderer.enabled = false;
                  }
                  break;
              case 5:
                  GameObject.Find("/Canvas/Lost_Tetrimino_Text").GetComponent<Text>().enabled = false;
                  break;
              case 6:
                  GameObject.Find("/Canvas/Next_Tetrimino_Text").GetComponent<Text>().enabled = false;
                  break;
              case 7:
                  GameObject.Find("Frame01").GetComponent<MeshRenderer>().enabled = false;
                  break;
              case 8:
                  GameObject.Find("Frame02").GetComponent<MeshRenderer>().enabled = false;
                  break;
              case 9:
              case 10:
                  GameObject.Find("Stage").GetComponent<MeshRenderer>().enabled = false;
                  break;
            }
        }
    }

    public void InvisibleBlock(int id)
    {
        String name;
        switch (id)
        {
            case 1:
                name = "TetriminoI";
                break;
            case 2:
                name = "TetriminoJ";
                break;
            case 3:
                name = "TetriminoL";
                break;
            case 4:
                name = "TetriminoO";
                break;
            case 5:
                name = "TetriminoS";
                break;
            case 6:
                name = "TetriminoZ";
                break;
            default:
                name = "TetriminoT";
                break;
        }

        foreach (GameObject block in GameObject.FindGameObjectsWithTag(name))
        {
            foreach (MeshRenderer renderer in block.GetComponentsInChildren<MeshRenderer>())
            {
                renderer.enabled = false;
            }
        }
    }
    
    public void ReappearBlocks(int id)
    {
        String name;
        switch (id)
        {
            case 1:
                name = "TetriminoI";
                break;
            case 2:
                name = "TetriminoJ";
                break;
            case 3:
                name = "TetriminoL";
                break;
            case 4:
                name = "TetriminoO";
                break;
            case 5:
                name = "TetriminoS";
                break;
            case 6:
                name = "TetriminoZ";
                break;
            default:
                name = "TetriminoT";
                break;
        }

        foreach (GameObject block in GameObject.FindGameObjectsWithTag(name))
        {
            foreach (MeshRenderer renderer in block.GetComponentsInChildren<MeshRenderer>())
            {
                renderer.enabled = true;
            }
        }
    }
    
}
