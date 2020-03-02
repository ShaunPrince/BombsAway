using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSliderAnim : MonoBehaviour
{
    private bool UpState;
    private Animator anim;
    void Start()
    {
        UpState = false;
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void PlayDifferentAnimations()
    {
        if (UpState)
        { anim.Play("VolumeSliderDown");
            UpState = false;
        }
        else
        {
            anim.Play("VolumeSliderUp");
            UpState = true;
        }
            
    }
}
