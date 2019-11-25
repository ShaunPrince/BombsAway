using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReapairControls : ControlScheme
{
    public Junction currentSelectedJunction;

    public Selector selector;
    // Start is called before the first frame update
    void Start()
    {
        selector = GameObject.FindObjectOfType<Selector>();
        currentSelectedJunction = selector.overJunction;
        
    }

    // Update is called once per frame
    void Update()
    {
        currentSelectedJunction = selector.overJunction;
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentSelectedJunction.RotateCW();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            currentSelectedJunction.RotateCCW();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            //Debug.Log(0);
            selector.MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            selector.MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            selector.MoveDown();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            selector.MoveLeft();
        }
    }


}
