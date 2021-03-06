﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerEndGame : MonoBehaviour
{
    public GameObject endGameCamObject;
    public GameObject endGameCanvas;
    //public GameObject deathTexture;
    public GameObject explosionFX;
    public GameObject canvasToDisable;
    public GameObject selectionWheelToDisable;
    private GameObject player;
    private GameObject bombBay;

    private bool gameEnded = false;
    // Start is called before the first frame update

    private void Start()
    {
        player = GameObject.Find("PlayerPlane");
        bombBay = GameObject.FindWithTag("BombBayStation");
        endGameCamObject.SetActive(false);
    }

    public void ShowPlayerWinning()
    {
        if (!gameEnded)
        {
            bool playerWon = true;
            SetScoreCamerasAndControls(playerWon);
            SetEndReasonWon();

            // after 20 seconds, switch screens?
            gameEnded = true;
        }

    }

    public void ShowPlayerRanOutOfBombs()
    {
        if (!gameEnded)
        {
            SetScoreCamerasAndControls(false);
            SetDeathReasonBombs();

            gameEnded = true;
        }
    }

    public void ShowPlayerDesertedMission()
    {
        if (!gameEnded)
        {
            SetScoreCamerasAndControls(false);
            SetDeathReasonDeserter();

            gameEnded = true;
        }
    }

    public void ShowPlayerDying()
    {
        if (!gameEnded)
        {
            // explode
            GameObject explosion = Instantiate(explosionFX, player.transform.position, player.transform.rotation);

            SetScoreCamerasAndControls(false);
            SetDeathReasonDied();

            gameEnded = true;
        }
               
    }

    private IEnumerator DisableCanvas()
    {
        yield return new WaitForSeconds(2f);
        canvasToDisable.SetActive(false);
    }

    private void SetScoreCamerasAndControls(bool playerWon)
    {
        // set the player's score
        endGameCanvas.transform.Find("Score Text").GetComponent<TMP_Text>().text = "Score: " + MissionManager.playerScore;

        // spin the death cam around player
        // deparent the death cam so it does not continue to "move with player"

        if (!playerWon) endGameCamObject.transform.parent = null;

        endGameCamObject.SetActive(true);
        iTween.RotateBy(endGameCamObject, iTween.Hash("amount", new Vector3(0, 1, 0),
                                                    "time", 50f, "easetype", "linear",
                                                    "looptype", "loop"));

        // Dissable all stations and selection wheel
        StationManager.currentlyActiveControlScheme.SetActiveControl(false);
        selectionWheelToDisable.SetActive(false);
        StartCoroutine(DisableCanvas());

        // turn off colliders on player so they can not be hit any more
        Transform models = player.transform.Find("Models");
        models.GetComponent<CapsuleCollider>().enabled = false;
        models.GetComponent<BoxCollider>().enabled = false;

        // hide bombs
        foreach (Transform child in bombBay.transform)
        {
            if (child.name == "Bomb(Clone)") child.gameObject.SetActive(false);
        }

        // tween from main camera to new death cam
        //deathTexture.SetActive(true);
        endGameCanvas.SetActive(true);
        GameObject deathTexture = GameObject.Find("EndGameCamTexture");
        deathTexture.gameObject.AddComponent<CameraTween>();
        deathTexture.gameObject.GetComponent<CameraTween>().FadeIn();
    }

    private void SetEndReasonWon()
    {
        // trigger reason for death display
        Transform deathReason = endGameCanvas.transform.Find("DeathReason");
        Color green = Color.green;
        green.a = 0f;
        deathReason.GetComponent<TMP_Text>().color = green;
        deathReason.GetComponent<TMP_Text>().text = "- SUCCESS -\nALL TARGETS DESTROYED";
        deathReason.gameObject.GetComponent<FadeAndFlashText>().setActive = true;
    }

    private void SetDeathReasonDied()
    {
        // trigger reason for death display
        Transform deathReason = endGameCanvas.transform.Find("DeathReason");
        deathReason.GetComponent<TMP_Text>().text = "- FAILED -";
        deathReason.gameObject.GetComponent<FadeAndFlashText>().setActive = true;
    }

    private void SetDeathReasonBombs()
    {
        // trigger reason for death display
        Transform deathReason = endGameCanvas.transform.Find("DeathReason");
        deathReason.GetComponent<TMP_Text>().text = "0 BOMBS     REMAINING\n" + MissionManager.NumberOfRemainingTargets() + " TARGETS REMAINING";
        deathReason.gameObject.GetComponent<FadeAndFlashText>().setActive = true;
    }

    private void SetDeathReasonDeserter()
    {
        // trigger reason for death display
        Transform deathReason = endGameCanvas.transform.Find("DeathReason");
        deathReason.GetComponent<TMP_Text>().text = "- DESERTED MISSION -\n" + MissionManager.NumberOfRemainingTargets() + " TARGETS REMAINING";
        deathReason.gameObject.GetComponent<FadeAndFlashText>().setActive = true;
    }
}
