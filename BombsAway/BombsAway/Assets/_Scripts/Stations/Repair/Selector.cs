using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    public Junction overJunction;

    private Rigidbody rb;

    private void Awake()
    {
        rb = this.GetComponentInParent<Rigidbody>();
    }
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        Junction temp = other.gameObject.GetComponentInParent<Junction>();
        if(temp != null)
        {
            overJunction = temp;
        }
    }

    public void MoveUp()
    {
        //Debug.Log(1);
        if (rb.transform.localPosition.y < -4)
        {
            //Debug.Log(2);
            rb.MovePosition(rb.transform.position + Vector3.up * 4);
        }
    }

    public void MoveRight()
    {
        if (rb.transform.localPosition.x < 4)
        {
            rb.MovePosition(rb.transform.position + Vector3.right * 4);
        }
    }

    public void MoveDown()
    {
        if (rb.transform.localPosition.y > -12)
        {
            rb.MovePosition(rb.transform.position + Vector3.down* 4);
        }
    }

    public void MoveLeft()
    {
        if (rb.transform.localPosition.x > -4)
        {
            rb.MovePosition(rb.transform.position + Vector3.left * 4);
        }
    }
}
