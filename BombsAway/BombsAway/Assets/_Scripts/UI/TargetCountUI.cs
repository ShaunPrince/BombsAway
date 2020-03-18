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

    // Start is called before the first frame update
    void Start()
    {
        //targetCountText.text = MissionManager.NumberOfRemainingTargets() + "/" + MissionManager.numberOfTargetBuildings;
        counterDigits.SetNumber(MissionManager.NumberOfRemainingTargets());
        maxDigits.SetNumber(MissionManager.numberOfTargetBuildings);
        prevNumTargets = MissionManager.NumberOfRemainingTargets();
        //Debug.Log($"{counterDigits.GetNumber()}, {maxDigits.GetNumber()}, {prevNumTargets}");
    }

    // Update is called once per frame
    void Update()
    {
        if (MissionManager.NumberOfRemainingTargets() != prevNumTargets)
        {
            int buildings = MissionManager.NumberOfRemainingTargets() < 0 ? 0 : MissionManager.NumberOfRemainingTargets();
            //targetCountText.text = buildings + "/" + MissionManager.numberOfTargetBuildings;
            counterDigits.SetNumber(buildings);
            prevNumTargets = MissionManager.NumberOfRemainingTargets();
        }
    }
}
