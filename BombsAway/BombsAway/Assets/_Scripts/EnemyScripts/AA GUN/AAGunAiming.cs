using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAGunAiming : MonoBehaviour
{
    public Rigidbody targetRB;
    public Transform gunTF;
    public float timeOfFlight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(targetRB != null)
        {
            CalculateTOF();
            gunTF.LookAt(targetRB.position + CalculateOffset());
        }

    }

    private void CalculateTOF()
    {
        EnemyFireWeapon efw = this.GetComponentInChildren<EnemyFireWeapon>();
        timeOfFlight = Vector3.Distance(this.transform.position, targetRB.transform.position) / efw.projectileSpeed;

    }

    private Vector3 CalculateOffset()
    {
        return (targetRB.velocity * timeOfFlight) + (Vector3.up * (float)(.5 * -Physics.gravity.y * timeOfFlight * timeOfFlight));
    }


}
