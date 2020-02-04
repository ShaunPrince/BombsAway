using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombReloadingAnimation : MonoBehaviour
{
    public GameObject modelRef;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadAnimation(float amount, float reloadTime)
    {
        Debug.Log($"Moving from {modelRef.transform.position} to {modelRef.transform.position + new Vector3(0, amount, 0)}");
        iTween.MoveBy(modelRef, iTween.Hash("amount", new Vector3(amount, 0, 0),
                                                   "time", reloadTime, "easetype", "easeOutBounce"));
    }
}
