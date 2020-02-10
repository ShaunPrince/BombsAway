using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVictory : MonoBehaviour
{
    private bool gameEnded = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MissionManager.HasPlayerWon() && !gameEnded)
        {
            // Trigger end game cam and show player
            // make player invinsible so that they can no longer win
            this.GetComponent<PlayerEndGame>().ShowPlayerWinning();
            gameEnded = true;
        }
        else if (this.GetComponent<BombToTargetCount>().NoMoreBombsButStillTargets() && !gameEnded)
        {
            // display warning that out of bombs
            this.GetComponent<PlayerEndGame>().ShowPlayerRanOutOfBombs();
            gameEnded = true;
        }
    }
}
