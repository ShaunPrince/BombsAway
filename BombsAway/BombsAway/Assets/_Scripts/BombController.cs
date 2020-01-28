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

    private AudioSource BombExplode;

    private bool isDropping = false;
    private Vector3 lastPosition;
    private Vector3 currentPosition;

    private List<GameObject> listObjectsToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        BombExplode = this.GetComponent<AudioSource>();
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

    private void RotateDown()
    {
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(360, 0, 0), Time.time * .001f);
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
        //Debug.Log("Exploded on collision with: " + collision.collider.name);
        Explode();
    }

    private void Explode()
    {
        BombExplode.Play();

        //Debug.Log("Exploding");
        // TEMP DELETE LATER

        //TEMPBombExplosion explosion = GameObject.FindWithTag("BombBayStation").GetComponent<TEMPBombExplosion>();
        //explosion.MakeExplosion(this.transform.position);

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
        this.transform.GetChild(2).gameObject.SetActive(true);
        StartCoroutine(DelayCoroutine());


    }

    IEnumerator DelayCoroutine()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(BombMapMarkerTime);
        GameObject.Destroy(this.gameObject);

    }

}
