using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInitialBombs : MonoBehaviour
{
    public BombBayControls controlls;
    private bool initial = true;
    // Start is called before the first frame update
    void Update()
    {
        if (initial)
        {
            controlls.InitialLoad();
            initial = false;
        }
    }
}
