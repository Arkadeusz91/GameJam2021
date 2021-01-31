using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalScore : MonoBehaviour
{
    public static int YourFinalScore;
    private Text finalScoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        finalScoreText = GetComponent<Text>();
        YourFinalScore = Score_Counter.YourScore;
        finalScoreText.text = "Your Final Score:\n" + YourFinalScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadSceneAsync("Menu",LoadSceneMode.Single);
        }   
    }
}
