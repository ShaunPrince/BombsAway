using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float lookSensitivity;

    private Camera myCamera;
    //private GameObject myGun;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        myCamera = this.GetComponentInChildren<Camera>();
        //myGun = this.transform.GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Look();
    }

    private void Look()
    {
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");
        if (myCamera.transform.localRotation.eulerAngles.x + -deltaY * lookSensitivity >= 280 ||
        myCamera.transform.localRotation.eulerAngles.x + -deltaY * lookSensitivity <= 80)
        {
            myCamera.transform.Rotate(-deltaY * lookSensitivity, 0f, 0f);
        }
        if (this.transform.localRotation.eulerAngles.y + deltaX * lookSensitivity >= 280 ||
        this.transform.localRotation.eulerAngles.y + deltaX * lookSensitivity <= 80)
        {
            this.transform.Rotate(0f, deltaX * lookSensitivity, 0f);
        }
    }
}
