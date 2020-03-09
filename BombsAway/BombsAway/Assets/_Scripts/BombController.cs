using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float damage;
    public float pushMagnitude;
    public float dropMagnitude;
    public EAllegiance allegiance;

    public float BombMapMarkerTime;
    public GameObject explosion;
    private bool hasExploded = false;

    //private AudioSource BombExplode;

    private bool isDropping = false;
    private Vector3 lastPosition;
    private Vector3 currentPosition;

    private List<GameObject> listObjectsToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        //BombExplode = this.GetComponent<AudioSource>();
        lastPosition = this.transform.position;
        currentPosition = this.transform.position;
        listObjectsToDestroy = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(this.transform.position, Vector3.down * 1000.0f);
        if (isDropping)
        {
            RotateDown();
            lastPosition = currentPosition;
            currentPosition = this.transform.position;
            CheckForHit();
        }
    }

    public void SetToDrop()
    {
        isDropping = true;
    }

    public bool HasExploded()
    {
        return hasExploded;
    }

    private void RotateDown()
    {
        //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(360, 0, 0), Time.time * .001f);
        this.transform.LookAt(this.transform.position + this.GetComponent<Rigidbody>().velocity);
    }

    private void CheckForHit()
    {

        RaycastHit hit;
        float distance = Vector3.Distance(lastPosition, currentPosition);
        if (Physics.Raycast(lastPosition, transform.TransformDirection(Vector3.down), out hit, distance) && (hit.collider.gameObject.layer == 9 || hit.collider.gameObject.layer == 12))
        {
            //Debug.Log("Exploding on Raycast from hitting: " + hit.collider.name);
            Explode();
        }
    }

    // when an object enters the bombs radius, add it to items to destroy
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"OnTriggerEnter: {other.gameObject}");
        // ignore terrain
        if (other.gameObject.layer != 9 && !listObjectsToDestroy.Contains(other.gameObject))
        {
            listObjectsToDestroy.Add(other.gameObject);
        }
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
        //Debug.Log("Exploded on collision with: " + collision.collider.name);
        Explode();
    }

    private void Explode()
    {
        if (!hasExploded) {
            hasExploded = true;
            //BombExplode.Play();
            GameObject boom = Instantiate(explosion, this.transform.position, this.transform.rotation);
            GameObject.Destroy(boom, 20f);


            // when the bomb crashes, destroy everything within it's radius
            foreach (GameObject objectToDestroy in listObjectsToDestroy)
            {
                //Debug.Log($"Object to destroy: {objectToDestroy}");
                if (objectToDestroy && objectToDestroy.GetComponentInParent<DamageableEntity>() != null)
                {
                    objectToDestroy.GetComponentInParent<DamageableEntity>().TakeDamage(damage, allegiance);
                }
            }
            //Destroy(this.gameObject);
            this.transform.GetChild(1).gameObject.SetActive(true);
            //StartCoroutine(DelayCoroutine());
            GameObject.Destroy(this.gameObject, BombMapMarkerTime);
        }
        
    }

    IEnumerator DelayCoroutine()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(BombMapMarkerTime);
        GameObject.Destroy(this.gameObject);

    }

}
