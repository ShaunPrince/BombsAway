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
            calculateTOF();
            gunTF.LookAt(targetRB.position + (targetRB.velocity * timeOfFlight));
        }

    }

    private void calculateTOF()
    {
        EnemyFireWeapon efw = this.GetComponentInChildren<EnemyFireWeapon>();
        timeOfFlight = Vector3.Distance(this.transform.position, targetRB.transform.position) / efw.projectileSpeed;

    }


}
