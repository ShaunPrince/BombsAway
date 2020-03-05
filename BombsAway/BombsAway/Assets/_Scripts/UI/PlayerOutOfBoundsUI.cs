using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerOutOfBoundsUI : MonoBehaviour
{
    public GameObject outOfBoundsText;
    public bool playerOutOfBoundsForTooLong = false;

    private float maxCounter = 60f;
    private float currentCounter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        outOfBoundsText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!MissionManager.PlayerInBounds())
        {
            outOfBoundsText.SetActive(true);
            UpdateCounter();
            UpdateOutOfBoundsCounter();
        }
        else
        {
            outOfBoundsText.SetActive(false);
            if (currentCounter > 0f) currentCounter = 0f;
        }
    }

    private void UpdateOutOfBoundsCounter()
    {
        outOfBoundsText.GetComponent<TMP_Text>().text = "PLEASE TURN AROUND\n" + ((int)maxCounter - (int)currentCounter);
    }

    private void UpdateCounter()
    {
        if (currentCounter < maxCounter)
        {
            currentCounter += Time.deltaTime;
        }
        if (currentCounter >= maxCounter)
        {
            currentCounter = maxCounter;
            playerOutOfBoundsForTooLong = true;
        }
    }
}
