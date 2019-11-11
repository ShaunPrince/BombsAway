using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float damage;
    public float pushMagnitude;
    public EAllegiance allegiance;

    private Rigidbody rb;
    private bool isDropping = false;
    private Vector3 dropVelocity;

    private List<GameObject> listObjectsToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        float x_push = Random.Range(0.0f, 1.0f);
        float z_push = Random.Range(0.0f, 1.0f);
        dropVelocity = new Vector3(x_push, 0.0f, z_push) * pushMagnitude + GameObject.FindWithTag("Player").GetComponent<Rigidbody>().velocity;

        listObjectsToDestroy = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDropping)
        {
            RotateDown();
        }
    }

    public void Drop()
    {
        isDropping = true;
        rb.isKinematic = false;
        rb.freezeRotation = false;
        rb.useGravity = true;
        rb.velocity = dropVelocity;
    }

    private void RotateDown()
    {
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(360, 0, 0), Time.time * .005f);
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
        Destroy(this.gameObject);
    }

}
