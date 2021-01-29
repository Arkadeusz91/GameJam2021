using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //zmienna potrzebna do odliczania czasu opadniecia klocka
    private float ItIsTime;
    //czas z jakim bêdzie opada³ klocek
    private float TimeToFall = 0.8f;    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(0, 0, 2);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(0, 0, -2);
        }

        if(Time.time - ItIsTime > TimeToFall)
        {
            transform.position += new Vector3(0, -2, 0);
            ItIsTime = Time.time;
        }
    }
}
