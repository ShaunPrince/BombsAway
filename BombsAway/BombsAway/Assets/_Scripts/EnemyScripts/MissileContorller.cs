using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileContorller : MonoBehaviour
{
    private float missileDamage = 10f;
    [SerializeField]
    private float maxTorque;
    [SerializeField]
    private float increaseDistanceFromCenter;
    [SerializeField]
    private float maxDistanceFromCenter;
    private Transform missileModel;
    private float speed;

    private float distanceToMaxRadius;

    private float percisionOffset = 0f;

    private float timeAlive = 0f;
    private float maxTimeAlive = 80f;
    private Rigidbody rigidbody;

    private Vector3 startPos;
    private Transform playerTransform;
    private Flying flyingComponent;
    //private Vector3 lastPosition;
    //private Vector3 currentPosition;

    public EAllegiance allegiance;

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    // Start is called before the first frame update
    void Awake()
    {
        //currentPosition = this.transform.position;
        //lastPosition = currentPosition;
        rigidbody = this.GetComponent<Rigidbody>();
        rigidbody.centerOfMass = Vector3.zero;

        //rigidbody.AddRelativeTorque(maxTorque, ForceMode.VelocityChange);

        startPos = this.transform.position;
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();

        missileModel = this.transform.GetChild(0).GetChild(0);

        flyingComponent = this.GetComponent<Flying>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeAlive >= maxTimeAlive)
        {
            Destroy(this.gameObject);
        }
        else
        {
            LookTowardsPlayer();
            //MoveTowardsPlayer();
            Corkscrew();

            timeAlive += Time.deltaTime;
        }
    }

    private void LookTowardsPlayer()
    {
        this.transform.LookAt(playerTransform);
        rigidbody.velocity = this.gameObject.transform.forward * speed;

        //Debug.Log($"{this.transform.rotation.y}; {rotation.y}");
    }

    private void MoveTowardsPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(playerTransform.position - this.transform.position, Vector3.up);
        float eulerDirection = rotation.eulerAngles.y;

        flyingComponent.SetDesAlt(playerTransform.GetComponentInChildren<Flying>().currentAltitude);
        flyingComponent.SetDesDir(eulerDirection);
    }

    void Corkscrew()
    {

        // if missile is less then half way to the player, start closing in
        if (Vector3.Distance(missileModel.position, playerTransform.position) < Vector3.Distance(startPos, playerTransform.position)/2 &&
            Vector3.Distance(missileModel.position, playerTransform.position) <= distanceToMaxRadius)
        {
            // do not go below zero
            if (missileModel.localPosition.x - increaseDistanceFromCenter >= 0)
            {
                this.transform.GetChild(0).GetChild(0).localPosition = new Vector3(missileModel.localPosition.x - increaseDistanceFromCenter, 0, 0);
                //Debug.Log($"Decreasing radius { this.transform.GetChild(0).GetChild(0).localPosition}");
            }
        }
        // if missile is more then half way from player, spiral out
        else
        {
            if (missileModel.localPosition.x < maxDistanceFromCenter)
            {
                this.transform.GetChild(0).GetChild(0).localPosition = new Vector3(missileModel.localPosition.x + increaseDistanceFromCenter, 0, 0);
                distanceToMaxRadius = Vector3.Distance(missileModel.position, startPos);
                //Debug.Log($"Increasing radius { this.transform.GetChild(0).GetChild(0).localPosition}");
            }
        }

        this.transform.GetChild(0).transform.Rotate(new Vector3(0, 0, maxTorque));
        this.transform.GetChild(0).GetChild(0).transform.Rotate(new Vector3(0, 1, 0));

        //Debug.Log($"distance to max radius: {distanceToMaxRadius}, {Vector3.Distance(startPos, playerPos) / 2}");
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Missile colliding with {other.transform.gameObject.name}");
        if (other.transform.gameObject.GetComponentInParent<DamageableEntity>() != null)
        {
            //Debug.Log($"1: {this.transform.gameObject.name} -> {other.transform.parent.gameObject.name}");
            other.transform.GetComponentInParent<DamageableEntity>().TakeDamage(missileDamage, allegiance);
            Destroy(this.gameObject);
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log($"Missile colliding with {collision.transform.gameObject.name}");
        if (collision.transform.gameObject.GetComponentInParent<DamageableEntity>() != null)
        {
            FindObjectOfType<AudioManager>().PlayExplosion();

            //Debug.Log($"1: {this.transform.gameObject.name} -> {other.transform.parent.gameObject.name}");
            collision.transform.GetComponentInParent<DamageableEntity>().TakeDamage(missileDamage, allegiance); // hurt player
            this.GetComponent<DamageableEntity>().TakeDamage(missileDamage, EAllegiance.Player); // kill missile
            //Destroy(this.gameObject);
        }
        else //if (collision.transform.gameObject.layer.ToString() != "8")
        {
            //Debug.Log($"2: {this.transform.gameObject.name} -> {other.transform.gameObject.name}");
            //Destroy(this.gameObject);
        }
    }
}
