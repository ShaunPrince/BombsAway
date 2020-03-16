using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetCountUI : MonoBehaviour
{
    //public TextMeshProUGUI targetCountText;
    public PhysicalSingleDigits counterDigits;
    public PhysicalSingleDigits maxDigits;
    private int prevNumTargets;

    private bool setInitial = false;

    // Start is called before the first frame update
    void Start()
    {
        //targetCountText.text = MissionManager.NumberOfRemainingTargets() + "/" + MissionManager.numberOfTargetBuildings;
        counterDigits.SetNumber(MissionManager.NumberOfRemainingTargets());
        maxDigits.SetNumber(MissionManager.numberOfTargetBuildings);
        prevNumTargets = MissionManager.NumberOfRemainingTargets();
    }

    // Update is called once per frame
    void Update()
    {
        if (!setInitial)
        {
            counterDigits.SetNumber(MissionManager.NumberOfRemainingTargets());
            maxDigits.SetNumber(MissionManager.numberOfTargetBuildings);
            prevNumTargets = MissionManager.NumberOfRemainingTargets();
            setInitial = true;
        }
        if (MissionManager.NumberOfRemainingTargets() != prevNumTargets)
        {
            int buildings = MissionManager.NumberOfRemainingTargets() < 0 ? 0 : MissionManager.NumberOfRemainingTargets();
            //targetCountText.text = buildings + "/" + MissionManager.numberOfTargetBuildings;
            counterDigits.SetNumber(buildings);
            prevNumTargets = MissionManager.NumberOfRemainingTargets();
        }
    }
}
