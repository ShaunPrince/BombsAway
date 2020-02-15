using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFragmentOnDeath : MonoBehaviour
{
    public float fragForce;
    public float fragRotatingForce;
    public GameObject model;
    public List<GameObject> objectsToHide;
    private bool isFragged;
    // Start is called before the first frame update
    void Start()
    {
        isFragged = false;
        if (fragForce == 0)
        {
            fragForce = 500;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fragment()
    {

        foreach (Transform fragment in model.transform)
        {
            Rigidbody temp = fragment.GetComponent<Rigidbody>();
            if (temp != null)
            {
                temp.isKinematic = false;
                if (temp.gameObject.name == "Blimp")
                {
                    temp.AddForce(new Vector3(Random.Range(-fragForce * .1f, fragForce * .1f), Random.Range(10, 20), Random.Range(-fragForce * .1f, fragForce * .1f)), ForceMode.VelocityChange);
                    temp.AddTorque(new Vector3(Random.Range(-.06f, -.04f), Random.Range(-fragRotatingForce * .1f, fragRotatingForce * .1f), Random.Range(-fragRotatingForce * .1f, fragRotatingForce * .1f)), ForceMode.VelocityChange);
                }
                else
                {
                    temp.AddForce(new Vector3(Random.Range(-fragForce, fragForce), Random.Range(-fragForce, -fragForce / 2), Random.Range(-fragForce, fragForce)), ForceMode.VelocityChange);
                    temp.AddTorque(new Vector3(Random.Range(-fragRotatingForce, fragRotatingForce), Random.Range(-fragRotatingForce, fragRotatingForce), Random.Range(-fragRotatingForce, fragRotatingForce)), ForceMode.VelocityChange);
                }
            }

        }
        if (this.gameObject.GetComponentInChildren<ParticleSystem>() != null)
        {
            this.gameObject.GetComponentInChildren<ParticleSystem>().gameObject.GetComponentInParent<Rigidbody>().isKinematic = false;
            if (this.gameObject.GetComponentInChildren<ParticleSystem>().isPlaying == false)
            {
                this.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            }


        }
        isFragged = true;
        foreach (Animator A in this.GetComponentsInChildren<Animator>())
        {
            A.SetTrigger("Die");
        }


    }

    public void HideObjects()
    {
        foreach (GameObject item in objectsToHide)
        {
            item.SetActive(false);
        }
    }
}
