using UnityEngine;
using UnityEngine.UI;

public class StartameCamera : MonoBehaviour
{
    public bool skip = false;
    //public GameObject startCamera;
    public float time;
    public float maxDistanceToBuilding;
    public GameObject startCamera;
    public GameObject targetText;
    public GameObject startCanvas;
    public GameObject mainCanvas;
    public GameObject fadeCanvas;
    public GameObject playerCenter;
    public Rigidbody playerRigidbody;
    public PlayerFragmentOnDeath hider;
    public GameObject selectionWheelToDisable;
    //public GameObject buildingSpawner;
    private GameObject allBuildings;
    private int targetBuildingIndex;

    private bool buidlingsDone = true;
    private EStatus doneMoving = EStatus.hasNotStarted;
    private bool fadeToBlackDone = false;
    private bool fadeBackDone = false;

    private float maxFadeTime = 1f;
    private float timer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        if (!skip)
        {
            allBuildings = GameObject.FindWithTag("BuildingSpawner");
            startCanvas.SetActive(true);
            mainCanvas.SetActive(false);
            hider.HideObjects();
            playerRigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            mainCanvas.SetActive(true);
            startCanvas.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!skip)
        {
            if (!MissionManager.FinishedChoosingTargets())
            {
                // show black Screen
                //Debug.Log($"loading");
                selectionWheelToDisable.SetActive(false);
            }
            else if (MissionManager.FinishedChoosingTargets() && buidlingsDone)
            {
                if (Mathf.Approximately(timer, 0f))
                {
                    //Debug.Log("Done loading");
                    targetBuildingIndex = FindClosestTarget();
                    this.transform.position = allBuildings.transform.GetChild(targetBuildingIndex).transform.position;
                    startCamera.GetComponent<CameraTween>().FadeIn();
                    targetText.GetComponent<FadeText>().FadeIn();
                    InitialCameraMovements();
                    timer += Time.deltaTime;
                }
                else if (timer > time / 3)
                {
                    buidlingsDone = false;
                    timer = 0f;

                    if (targetBuildingIndex == -1)
                    {
                        Debug.Log($"ERROR: StartGameCamera.cs targetBuildingIndex is {targetBuildingIndex}, could not find a target buidling within range");
                    }
                    else
                    {
                        this.transform.parent = null;
                        //MoveTowardsTarget(targetBuildingIndex);
                        targetText.GetComponent<FadeText>().FadeOut();
                        MoveTowardsPlayer();
                    }
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }
            else if (doneMoving == EStatus.hasNotStarted)
            {
                TooClose();
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
                        hider.ShowObjects();
                        selectionWheelToDisable.SetActive(true);
                        timer += Time.deltaTime;
                    }
                    else if (timer > maxFadeTime)
                    {
                        timer = 0f;
                        //startCanvas.SetActive(false);
                        //mainCanvas.SetActive(true);
                        fadeBackDone = true;
                        startCanvas.SetActive(false);
                        playerRigidbody.constraints = RigidbodyConstraints.None;
                        playerRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                        doneMoving = EStatus.completed;
                    }
                    else
                    {
                        timer += Time.deltaTime;
                    }
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
        //Debug.Log($"{allBuildings.transform.childCount}");
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
        //Debug.Log("MovingTowards");
        iTween.MoveTo(this.gameObject, iTween.Hash("position", allBuildings.transform.GetChild(buildingIndex).transform.position,
                                                   "time", time, "looktarget", allBuildings.transform.GetChild(buildingIndex).transform.position,
                                                   "looktime", 2f, "easetype", "easeInOutExpo"));
    }

    private void MoveTowardsPlayer()
    {
        //Debug.Log("MovingTowards");
        iTween.MoveTo(this.gameObject, iTween.Hash("position", playerCenter.transform.position,
                                                   "time", time, "looktarget", playerCenter.transform.position,
                                                   "looktime", 6f, "easetype", "easeInOutExpo"));

        iTween.RotateTo(this.transform.GetChild(0).gameObject, iTween.Hash("rotation", Vector3.zero, "islocal", true,
                                                                         "time", time/2, "easetype", "linear"));
    }

    private void InitialCameraMovements()
    {
        Vector3 upPos = new Vector3(this.transform.position.x, this.transform.position.y + 300f, this.transform.position.z);
        iTween.MoveTo(this.gameObject, iTween.Hash("position", upPos,
                                                   "time", time/3, "easetype", "linear"));

        Vector3 newPos = new Vector3(0f, -this.transform.GetChild(0).localPosition.y, 0f);
        iTween.MoveTo(this.transform.GetChild(0).gameObject, iTween.Hash("position", newPos, "islocal", true,
                                                                         "time", time, "easetype", "linear"));
    }

    private void ChangeCameras()
    {
        // fade to black and now inside ship

    }

    private void DoneMoving()
    {
        //Debug.Log($"Done moving towards target");
        doneMoving = EStatus.start;
    }

    private void TooClose()
    {
        float distance = Mathf.Abs(Vector3.Distance(playerCenter.transform.position, this.transform.position));
        //Debug.Log($"{distance} from player");
        if (distance < maxDistanceToBuilding)
        {
            //Debug.Log($"Done moving towards target");
            doneMoving = EStatus.start;
        }
    }

    private void FadeToBlack()
    {
        //Debug.Log("Fade to black");
        //fadeCanvas.GetComponent<Image>().CrossFadeAlpha(1f, 2f, false);
        startCamera.GetComponent<CameraTween>().FadeOut();
    }

    private void FadeFromBlack()
    {
        //Debug.Log("Fade from black");
        fadeCanvas.GetComponent<CameraTween>().FadeOut();
    }
}
