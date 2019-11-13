using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadMonitorControl : MonoBehaviour
{

    public GameObject gunner;
    public GameObject MonitorGraphic;
    public GameObject ReloadGraphic;
    public Animation anim;
    public AnimationClip animc;


    private float timer;
    private bool isReloading = false;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        timer = gunner.GetComponent<PlayerGunController>().timeToReload;
        animator = ReloadGraphic.GetComponent<Animator>();
    }

    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            isReloading = gunner.GetComponent<PlayerGunController>().reloading;
            if (isReloading) {
                float speed = anim[animc.name].speed;
                anim[animc.name].speed = anim[animc.name].speed * timer;
                MonitorGraphic.SetActive(true);
            }
        }

        if (!AnimatorIsPlaying()) {
            MonitorGraphic.SetActive(false);
        }
    }
}
