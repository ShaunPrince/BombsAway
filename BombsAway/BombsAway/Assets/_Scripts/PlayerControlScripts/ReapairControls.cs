using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReapairControls : ControlScheme
{
    public Junction currentSelectedJunction;

    public Selector selector;

    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        selector = GameObject.FindObjectOfType<Selector>();
        currentSelectedJunction = selector.overJunction;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSelectedJunction = selector.overJunction;
        if (Input.GetMouseButtonDown(0))
        {
            currentSelectedJunction.RotateCW();
            audioManager.PlayPipeMove();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            currentSelectedJunction.RotateCCW();
            audioManager.PlayPipeMove();
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
