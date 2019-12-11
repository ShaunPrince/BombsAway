using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateRadar : MonoBehaviour
{
    public float secondsPerRotation;
    private Rigidbody rb;
    //private Image sprite;

    public GameObject radarswipe;
    public GameObject parentTransform;
    private Vector3 oldparentTransform;
    // Start is called before the first frame update
    void Awake()
    {
        rb = this.GetComponentInChildren<Rigidbody>();
        //sprite = radarswipe.GetComponent<Image>();
        oldparentTransform = parentTransform.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //figuring out how much the parent rotated delta to offset the radar swipe on z axis
        Vector3 newAngle = oldparentTransform - parentTransform.transform.rotation.eulerAngles;
        newAngle = new Vector3(newAngle.x, newAngle.z, newAngle.y);

        // radar beamer
        Vector3 curRotation = rb.transform.rotation.eulerAngles;
        Vector3 newRotation = curRotation + new Vector3(0, 360 / (secondsPerRotation * 50), 0);
        rb.MoveRotation(Quaternion.Euler(newRotation));

        //radar swipe
        Vector3 newSRotation = radarswipe.transform.rotation.eulerAngles -
                newAngle + new Vector3(0, 0, ( 360 / (secondsPerRotation * 50))*-1);
        radarswipe.transform.SetPositionAndRotation(radarswipe.transform.position, Quaternion.Euler(newSRotation));

        if (oldparentTransform != parentTransform.transform.rotation.eulerAngles)
        {
            oldparentTransform = parentTransform.transform.rotation.eulerAngles;
        }
    }
}
