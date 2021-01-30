using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class Generator : MonoBehaviour
{
    public GameObject[] tetriminoArray;

    public bool isNothingControlled = true;

    private bool first = true;

    public GameObject nextTetrimino = null;
    
    public int disabledObjectId = 0;

    public GameObject disabledObject = null;

    public int lastLevel = 1;

    // Start is called before the first frame update
    void Start()
    {
        disabledObjectId = Random.Range(0, tetriminoArray.Length - 1);
        disabledObject = Instantiate(tetriminoArray[disabledObjectId]);
        disabledObject.transform.position += new Vector3(0, -20, 0);
    }

    // Update is called once per frame
    void Update()
    {
        int currentLevel = Level_Script.YourLevel;
        if (currentLevel > lastLevel)
        {
            lastLevel = currentLevel;
            Destroy(disabledObject);
            disabledObjectId = Random.Range(0, tetriminoArray.Length - 1);
            disabledObject = Instantiate(tetriminoArray[disabledObjectId]);
            disabledObject.transform.position += new Vector3(0, -20, 0);
        }
        if (isNothingControlled)
        {
            generate(); 
        }
    }

    public void generate()
    {
        int random = Random.Range(0, tetriminoArray.Length - 1);
        while (random == disabledObjectId)
        {
            random = Random.Range(0, tetriminoArray.Length - 1); 
        }
        if (first)
        {
            nextTetrimino = Instantiate(tetriminoArray[random]);
            first = false;
        }
        nextTetrimino.transform.position=new Vector3(10, 32, 0);
        nextTetrimino.AddComponent<PlayerControl>();
        random = Random.Range(0, tetriminoArray.Length - 1);
        nextTetrimino = Instantiate(tetriminoArray[random]);
        isNothingControlled = false;
    }
}
