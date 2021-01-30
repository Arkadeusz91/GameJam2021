using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Script : MonoBehaviour
{
    public static int YourLevel;
    Text Level;

    // Start is called before the first frame update
    void Start()
    {
        Level = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        YourLevel = (Score_Counter.YourScore / 1000) + 1;
        Level.text = "Level:\n" + YourLevel;
    }


}
