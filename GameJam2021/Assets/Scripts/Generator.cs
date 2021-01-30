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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isNothingControlled)
        {
            generate();
            isNothingControlled = false;
        }
    }

    public void generate()
    {
        int random = Random.Range(0,tetriminoArray.Length-1);
        GameObject tetrimino = tetriminoArray[random];
        GameObject newTetrimino = Instantiate(tetrimino);
    }
}
