using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int prevScore;

    // Start is called before the first frame update
    void Start()
    {
        prevScore = MissionManager.playerScore;
        scoreText.text = "Score: " + prevScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (prevScore != MissionManager.playerScore)
        {
            prevScore = MissionManager.playerScore;
            scoreText.text = "Score: " + prevScore;
        }
    }
}
