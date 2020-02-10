using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVictory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MissionManager.HasPlayerWon())
        {
            // Trigger end game cam and show player
            // make player invinsible so that they can no longer win
            this.GetComponent<PlayerEndGame>().ShowPlayerWinning();
        }
        else if (this.GetComponent<BombToTargetCount>().NoMoreBombsButStillTargets())
        {
            this.GetComponent<PlayerEndGame>().ShowPlayerWinning(); // not winning but yeee
        }
    }
}
