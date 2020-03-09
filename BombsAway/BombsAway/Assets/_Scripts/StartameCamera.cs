using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartameCamera : MonoBehaviour
{
    //public GameObject startCamera;
    public float time;
    public GameObject startCanvas;
    public GameObject mainCanvas;
    public GameObject fadeCanvas;
    //public GameObject buildingSpawner;
    private GameObject allBuildings;

    private bool buidlingsDone = true;
    private bool doneMoving = false;
    private bool fadeToBlackDone = false;
    private bool fadeBackDone = false;


    // Start is called before the first frame update
    void Start()
    {
        allBuildings = GameObject.FindWithTag("BuildingSpawner");
        startCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (MissionManager.FinishedChoosingTargets() && buidlingsDone)
        {
            buidlingsDone = false;

            int targetBuildingIndex = FindClosestTarget();
            if (targetBuildingIndex == -1)
            {
                Debug.Log($"ERROR: StartGameCamera.cs targetBuildingIndex is {targetBuildingIndex}, could not find a target buidling within range");
            }
            else
            {
                MoveTowardsTarget(targetBuildingIndex);
            }
        }
        else if (doneMoving)
        {
            Debug.Log($"Done moving towards target");

            // fade to black, disable start canvas, enable main canvas
            // fade back to "white"
            // use my own timer for this
        }
    }

    /*
    private bool AllBuildingsDoneSpawning()
    {
        ObjectSpawner[] spawners = buildingSpawner.GetComponents<ObjectSpawner>();
        foreach (ObjectSpawner s in spawners)
        {
            if (s.GetBuildingGenerationStatus() != EStatus.completed) return false;
        }
        return true;
    }*/

    private int FindClosestTarget()
    {
        int closestTargetIndex = -1;
        float closestDistance = float.MaxValue;
        Debug.Log($"{allBuildings.transform.childCount}");
        for (int i = 0; i < allBuildings.transform.childCount; i++)
        {
            if (allBuildings.transform.GetChild(i).GetComponent<TerrainObject>().objectType == ETerrainObjectType.Target)
            {
                float distance = Mathf.Abs(Vector3.Distance(this.transform.position, allBuildings.transform.GetChild(i).transform.position));
                Debug.Log($"{allBuildings.transform.GetChild(i).name} has a distance of {distance}");
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTargetIndex = i;
                }
            }
        }

        return closestTargetIndex;
    }

    private void MoveTowardsTarget(int buildingIndex)
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("position", allBuildings.transform.GetChild(buildingIndex).transform.position,
                                                   "time", time, "looktarget", allBuildings.transform.GetChild(buildingIndex).transform.position,
                                                   "looktime", 10f, "easetype", "easeInOutQuad", "oncomplete", "DoneMoving"));
    }

    private void ChangeCameras()
    {
        // fade to black and now inside ship

    }

    private void DoneMoving()
    {
        doneMoving = true;
    }

    private void FadeToBlack()
    {
        fadeCanvas.GetComponent<Image>().CrossFadeAlpha(255f, 2f, false);
    }

    private void FadeFromBlack()
    {
        fadeCanvas.GetComponent<Image>().CrossFadeAlpha(0f, 2f, false);
    }
}
