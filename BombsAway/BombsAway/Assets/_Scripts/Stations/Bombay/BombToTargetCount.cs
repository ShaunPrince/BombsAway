using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombToTargetCount : MonoBehaviour
{
    // If player has dropped last bomb and it has exploded
    // and there are still remaining targets, end the game
    public BombBayControls bombBay;

    public bool NoMoreBombsButStillTargets()
    {
        if (LastBombExploded() && TargetsStillRemaining()) return true;
        else return false;
    }

    private bool LastBombExploded()
    {
        if (bombBay.numOfBombs <= 0)
        {
            if (bombBay.GetMostRecentDroppedBomb() != null)
            {
                if (bombBay.GetMostRecentDroppedBomb().GetComponent<BombController>().HasExploded())
                {
                    return true;
                }
            } 
        }
        return false;
    }

    private bool TargetsStillRemaining()
    {
        if (MissionManager.NumberOfRemainingTargets() > 0) return true;
        else return false;
    }
}
