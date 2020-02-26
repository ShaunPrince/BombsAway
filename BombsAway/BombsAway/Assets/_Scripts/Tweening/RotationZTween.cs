using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationZTween : MonoBehaviour
{
    private float prevRotation;
    private float changeTime = 1f;
    //private Flying player;
    // Start is called before the first frame update
    void Start()
    {
        prevRotation = this.transform.localEulerAngles.z;
        //player = GameObject.FindWithTag("PilotStation").GetComponent<Flying>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TweenRotation(float toRotation)
    {
        Flying player = GameObject.FindWithTag("PilotStation").GetComponent<Flying>();
        changeTime = Mathf.Abs(player.currentForwardSpeed - player.desiredForwardSpeed) / player.forwardAcceleration;

        iTween.ValueTo(this.gameObject, iTween.Hash( "from", prevRotation, "to", toRotation,
                                                     "time", changeTime, "easeType", "linear",
                                                     "onupdate", "RotateZ"));

    }

    public void TweenRotation(float toRotation, float time)
    {
        Flying player = GameObject.FindWithTag("PilotStation").GetComponent<Flying>();
        changeTime = Mathf.Abs(player.currentForwardSpeed - player.desiredForwardSpeed) / player.forwardAcceleration;

        iTween.ValueTo(this.gameObject, iTween.Hash("from", prevRotation, "to", toRotation,
                                                     "time", time, "easeType", "linear",
                                                     "onupdate", "RotateZ"));

    }

    private void RotateZ(float amount)
    {
        Quaternion rotation = this.transform.localRotation;
        this.transform.localRotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y, amount);
        prevRotation = amount;
    } 
}
