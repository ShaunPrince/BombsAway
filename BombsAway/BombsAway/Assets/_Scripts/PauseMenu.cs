using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject controlsPage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ViewControls()
    {
        controlsPage.SetActive(!controlsPage.activeSelf);
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }
}
