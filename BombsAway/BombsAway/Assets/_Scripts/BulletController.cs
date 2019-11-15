using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float bulletDamage;
    private float timeAlive;
    private Vector3 lastPosition;
    private Vector3 currentPosition;

    public EAllegiance allegiance;

    // Start is called before the first frame update
    void Start()
    {
        bulletDamage = 1.0f;
        timeAlive = 0.0f;
        lastPosition = this.transform.position;
        currentPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        lastPosition = currentPosition;
        currentPosition = this.transform.position;
        timeAlive += Time.deltaTime;
        if (timeAlive >= 10.0f)
        {
            Destroy(this.gameObject);
        }
        CheckForHit();
    }

    public void SetBulletDamage(float damage)
    {
        bulletDamage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        HitObject(collision.collider);
    }

    private void OnCollisionEnter(Collider other)
    {
        if (!other.tag.Equals("Player") || !other.tag.Equals("Bullet"))
        {
            HitObject(other);
        }
    }

    private void CheckForHit()
    {
        
        RaycastHit hit;
        //LayerMask layerMask = LayerMask.GetMask("Bullet");
        float distance = Vector3.Distance(lastPosition, currentPosition);
        if (Physics.Raycast(lastPosition, transform.TransformDirection(Vector3.forward), out hit, distance) && !hit.collider.tag.Equals("Bullet"))
        {
            //Debug.Log("Hit");
            HitObject(hit.collider);
        }
    }

    private void HitObject(Collider other)
    {
        // Debug.Log(other.gameObject.layer.ToString());
        if (other.gameObject.GetComponentInParent<DamageableEntity>() != null)
        {
            //Debug.Log($"1: {this.transform.gameObject.name} -> {other.transform.parent.gameObject.name}");
            other.GetComponentInParent<DamageableEntity>().TakeDamage(bulletDamage,allegiance);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.layer.ToString() != "8")
        {
            //Debug.Log($"2: {this.transform.gameObject.name} -> {other.transform.gameObject.name}");
            Destroy(this.gameObject);
        }
    }
}
