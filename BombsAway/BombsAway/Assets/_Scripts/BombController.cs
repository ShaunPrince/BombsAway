using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float damage;
    public float pushMagnitude;
    public EAllegiance allegiance;

    private Rigidbody rb;
    private bool isDropping;
    private Vector3 dropVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        isDropping = false;
        float x_push = Random.Range(0.0f, 1.0f);
        float z_push = Random.Range(0.0f, 1.0f);
        dropVelocity = new Vector3(x_push, 0.0f, z_push) * pushMagnitude;
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
        this.transform.parent = null;
        rb.velocity = dropVelocity;
    }

    private void RotateDown()
    {
        if (this.transform.eulerAngles.x < 180)
        {
            this.transform.Rotate(0.5f, 0.0f, 0.0f, Space.Self);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        HitObject(collision.collider);
    }

    private void OnCollisionEnter(Collider other)
    {
        if (!other.tag.Equals("Player") || !other.tag.Equals("Bullet") || !other.tag.Equals("Bomb"))
        {
            HitObject(other);
        }
    }

    private void HitObject(Collider other)
    {
        if (other.gameObject.GetComponentInParent<DamageableEntity>() != null)
        {
            other.GetComponentInParent<DamageableEntity>().TakeDamage(damage, allegiance);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.layer.ToString() != "8")
        {
            Destroy(this.gameObject);
        }
    }
}
