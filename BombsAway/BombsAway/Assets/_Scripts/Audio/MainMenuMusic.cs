using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMusic : MonoBehaviour
{
    private AudioSource menuMusic;
    private float transitionTime;
    private float startVol;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        menuMusic = this.GetComponent<AudioSource>();
        transitionTime = 0.0f;
        startVol = menuMusic.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (transitionTime > 0.0f)
        {
            menuMusic.volume -= menuMusic.volume * Time.deltaTime;
            transitionTime -= Time.deltaTime;
            if (transitionTime < 0.0f)
            {
                menuMusic.Stop();
                menuMusic.volume = startVol;
                Destroy(this.gameObject);
            }
        }
    }

    public void FadeMusicOut()
    {
        transitionTime = 3.0f;
    }
}
