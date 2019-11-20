using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float damage;
    public float pushMagnitude;
    public float dropMagnitude;
    public EAllegiance allegiance;

    private AudioSource BombExplode;

    private Rigidbody rb;
    private bool isDropping = false;
    private Vector3 dropVelocity;
    private Vector3 planeVeloctiy;
    private Vector3 lastPosition;
    private Vector3 currentPosition;

    private List<GameObject> listObjectsToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        BombExplode = this.GetComponent<AudioSource>(); 

        rb = this.GetComponent<Rigidbody>();
        float x_push = Random.Range(-1.0f, 1.0f);
        float z_push = Random.Range(-1.0f, 1.0f);
        planeVeloctiy = GameObject.FindWithTag("Player").GetComponent<Rigidbody>().velocity;
        dropVelocity = new Vector3(x_push * pushMagnitude, -1.0f * dropMagnitude, z_push * pushMagnitude);
        lastPosition = this.transform.position;
        currentPosition = this.transform.position;

        listObjectsToDestroy = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDropping)
        {
            RotateDown();
            PushBomb();
            lastPosition = currentPosition;
            currentPosition = this.transform.position;
            //CheckForHit();
        }
    }

    public void Drop()
    {
        isDropping = true;
        rb.isKinematic = false;
        rb.freezeRotation = false;
        rb.useGravity = true;
        rb.velocity = new Vector3(planeVeloctiy.x, planeVeloctiy.y - 100.0f, planeVeloctiy.z);
    }

    private void PushBomb()
    {
        rb.AddForce(dropVelocity);
    }

    private void RotateDown()
    {
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(360, 0, 0), Time.time * .001f);
    }

    private void CheckForHit()
    {

        RaycastHit hit;
        float distance = Vector3.Distance(lastPosition, currentPosition);
        bool didHit = Physics.Raycast(lastPosition, transform.TransformDirection(Vector3.forward), out hit, distance);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            Debug.Log(hit.collider.gameObject.layer);
            Debug.Log("");
        }
        if (didHit && (hit.collider.gameObject.layer == 9 || hit.collider.gameObject.layer == 12))
        {
            Debug.Log(hit.collider.name);
            Explode();
        }
    }

    // when an object enters the bombs radius, add it to items to destroy
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"OnTriggerEnter: {other.gameObject}");
        // ignore terrain
        if (other.gameObject.layer != 9) listObjectsToDestroy.Add(other.gameObject);
    }

    // if the item leaves the bombs radius, remove it from items to destroy
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log($"OnTriggerExit: {other.gameObject}");
        if (listObjectsToDestroy.Contains(other.gameObject))
        {
            listObjectsToDestroy.Remove(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    private void Explode()
    {
        //Debug.Log("Exploding");
        // TEMP DELETE LATER
        TEMPBombExplosion explosion = GameObject.FindWithTag("BombBayStation").GetComponent<TEMPBombExplosion>();
        explosion.MakeExplosion(this.transform.position);

        // when the bomb crashes, destroy everything within it's radius
        foreach (GameObject objectToDestroy in listObjectsToDestroy)
        {
            //Debug.Log($"Object to destroy: {objectToDestroy}");
            if (objectToDestroy && objectToDestroy.GetComponentInParent<DamageableEntity>() != null)
            {
                objectToDestroy.GetComponentInParent<DamageableEntity>().TakeDamage(damage, allegiance);
            }
        }
        BombExplode.Play();
        Destroy(this.gameObject);
    }

}
