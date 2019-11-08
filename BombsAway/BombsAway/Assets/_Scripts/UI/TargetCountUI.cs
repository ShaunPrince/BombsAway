using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetCountUI : MonoBehaviour
{
    public TextMeshProUGUI targetCountText;
    private int prevNumTargets;

    // Start is called before the first frame update
    void Start()
    {
        targetCountText.text = MissionManager.NumberOfRemainingTargets() + "/" + MissionManager.numberOfTargetBuildings;
        prevNumTargets = MissionManager.NumberOfRemainingTargets();
    }

    // Update is called once per frame
    void Update()
    {
        if (MissionManager.NumberOfRemainingTargets() != prevNumTargets)
        {
            targetCountText.text = MissionManager.NumberOfRemainingTargets() + "/" + MissionManager.numberOfTargetBuildings;
            prevNumTargets = MissionManager.NumberOfRemainingTargets();
        }
    }
}
