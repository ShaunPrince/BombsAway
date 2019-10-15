using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage;
    private float timeAlive;
    private Vector3 lastPosition;
    private Vector3 currentPostition;

    // Start is called before the first frame update
    void Start()
    {
        timeAlive = 0.0f;
        currentPostition = this.transform.position;
        lastPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //lastPosition = currentPostition;
        //currentPostition = this.transform.position;
        timeAlive += Time.deltaTime;
        if (timeAlive >= 10.0f)
        {
            Destroy(this.gameObject);
        }
        //CheckForHit();
    }

    private void OnCollisionEnter(Collision collision)
    {
        HitObject(collision.collider);
    }

    private void OnCollisionEnter(Collider other)
    {
        if (!other.tag.Equals("Player"))
        {
            HitObject(other);
        }
    }

    private void CheckForHit()
    {
        
        Vector3 backwards = this.transform.forward * -1.0f;
        //Debug.Log(backwards);
        if (Physics.Raycast(currentPostition, backwards, Vector3.Distance(currentPostition, lastPosition)))
        {
            Debug.Log("Hit");
        }
    }

    private void HitObject(Collider other)
    {
        if(other.gameObject.GetComponent<DamageableEntity>() != null)
        {
            other.GetComponent<DamageableEntity>().TakeDamage(damage);
            GameObject.Destroy(this.gameObject);
        }
        if (!(other.tag.Equals("Player") || other.tag.Equals("Ground")))
        {
            Debug.Log("Hit");
            //Destroy(other.gameObject);
        }
    }
}
