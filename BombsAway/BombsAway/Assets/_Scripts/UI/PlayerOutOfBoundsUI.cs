using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerOutOfBoundsUI : MonoBehaviour
{
    public TextMeshProUGUI outOfBoundsText;

    // Start is called before the first frame update
    void Start()
    {
        outOfBoundsText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!MissionManager.PlayerInBounds())
        {
            outOfBoundsText.enabled = true;
        }
        else
        {
            outOfBoundsText.enabled = false;
        }
    }
}
