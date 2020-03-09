using UnityEngine;
using UnityEngine.UI;

public class StartameCamera : MonoBehaviour
{
    //public GameObject startCamera;
    public float time;
    public float maxDistanceToBuilding;
    public GameObject startCanvas;
    public GameObject mainCanvas;
    public GameObject fadeCanvas;
    //public GameObject buildingSpawner;
    private GameObject allBuildings;
    private int targetBuildingIndex;

    private bool buidlingsDone = true;
    private EStatus doneMoving = EStatus.hasNotStarted;
    private bool fadeToBlackDone = false;
    private bool fadeBackDone = false;

    private float maxFadeTime = 2f;
    private float timer = 0f;


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
            if (timer > time/3)
            {
                buidlingsDone = false;
                timer = 0f;

                targetBuildingIndex = FindClosestTarget();
                if (targetBuildingIndex == -1)
                {
                    Debug.Log($"ERROR: StartGameCamera.cs targetBuildingIndex is {targetBuildingIndex}, could not find a target buidling within range");
                }
                else
                {
                    MoveTowardsTarget(targetBuildingIndex);
                }
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else if (doneMoving == EStatus.hasNotStarted)
        {
            TooClose(targetBuildingIndex);
        }
        else if (doneMoving == EStatus.start)
        {
            // fade to black, disable start canvas, enable main canvas
            // fade back to "white"
            // use my own timer for this

            if (!fadeToBlackDone)
            {
                if (Mathf.Approximately(timer, 0f))
                {
                    FadeToBlack();
                    timer += Time.deltaTime;
                }
                else if (timer > maxFadeTime)
                {
                    timer = 0f;
                    startCanvas.SetActive(false);
                    mainCanvas.SetActive(true);
                    fadeToBlackDone = true;
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }
            else if (!fadeBackDone)
            {
                if (Mathf.Approximately(timer, 0f))
                {
                    FadeFromBlack();
                    timer += Time.deltaTime;
                }
                else if (timer > maxFadeTime)
                {
                    timer = 0f;
                    //startCanvas.SetActive(false);
                    //mainCanvas.SetActive(true);
                    fadeBackDone = true;
                    doneMoving = EStatus.completed;
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }
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
                //Debug.Log($"{allBuildings.transform.GetChild(i).name} has a distance of {distance}");
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
        Debug.Log("MovingTowards");
        iTween.MoveTo(this.gameObject, iTween.Hash("position", allBuildings.transform.GetChild(buildingIndex).transform.position,
                                                   "time", time, "looktarget", allBuildings.transform.GetChild(buildingIndex).transform.position,
                                                   "looktime", 10f, "easetype", "easeInOutQuad"));
    }

    private void ChangeCameras()
    {
        // fade to black and now inside ship

    }

    private void DoneMoving()
    {
        Debug.Log($"Done moving towards target");
        doneMoving = EStatus.start;
    }

    private void TooClose(int index)
    {
        float distance = Vector3.Distance(this.transform.position, allBuildings.transform.GetChild(index).transform.position);
        if (distance < maxDistanceToBuilding)
        {
            Debug.Log($"Done moving towards target");
            doneMoving = EStatus.start;
        }
    }

    private void FadeToBlack()
    {
        Debug.Log("Fade to black");
        fadeCanvas.GetComponent<Image>().CrossFadeAlpha(1f, 2f, false);
    }

    private void FadeFromBlack()
    {
        Debug.Log("Fade from black");
        fadeCanvas.GetComponent<Image>().CrossFadeAlpha(0f, 2f, false);
    }
}
