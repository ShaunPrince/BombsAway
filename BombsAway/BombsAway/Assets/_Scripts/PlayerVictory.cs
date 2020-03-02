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
            Debug.Log("END GAME: Player won, bombed all buidlings");
            // Trigger end game cam and show player
            // make player invinsible so that they can no longer win
            this.GetComponent<PlayerEndGame>().ShowPlayerWinning();
            this.GetComponent<PlayerFragmentOnDeath>().HideObjects();
            gameEnded = true;
        }
        else if (this.GetComponent<BombToTargetCount>().NoMoreBombsButStillTargets() && !gameEnded)
        {
            Debug.Log("END GAME: Player ran out of bombs");
            // display warning that out of bombs
            this.GetComponent<PlayerEndGame>().ShowPlayerRanOutOfBombs();
            this.GetComponent<PlayerFragmentOnDeath>().HideObjects();
            gameEnded = true;
        }
        else if (GameObject.FindWithTag("PlayerUIandCamera").GetComponent<PlayerOutOfBoundsUI>().playerOutOfBoundsForTooLong && !gameEnded)
        {
            Debug.Log("END GAME: Player went out of bounds");
            this.GetComponent<PlayerEndGame>().ShowPlayerDesertedMission();
            this.GetComponent<PlayerFragmentOnDeath>().HideObjects();
            gameEnded = true;
        }
        else if (this.GetComponent<PlayerDamageEntity>().health <= 0 && !gameEnded)
        {
            Debug.Log("END GAME: Player died, ran out of health");
            gameEnded = true;
        }
    }

    public bool HasGameEnded()
    {
        return gameEnded;
    }
}
