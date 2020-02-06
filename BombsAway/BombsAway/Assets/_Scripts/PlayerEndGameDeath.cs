using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEndGameDeath : MonoBehaviour
{
    public GameObject deathCamObject;
    public GameObject deathCanvas;
    //public GameObject deathTexture;
    public GameObject explosionFX;
    public GameObject canvasToDisable;
    public GameObject selectionWheelToDisable;
    private GameObject player;

    private bool hasDied = false;
    // Start is called before the first frame update

    private void Start()
    {
        player = GameObject.Find("PlayerPlane");
        deathCamObject.SetActive(false);
    }

    public void ShowPlayerDying()
    {
        if (!hasDied)
        {
            // explode
            GameObject explosion = Instantiate(explosionFX, player.transform.position, player.transform.rotation);

            // spin the death cam around player
            deathCamObject.SetActive(true);
            iTween.RotateBy(deathCamObject, iTween.Hash("amount", new Vector3(0, 1, 0),
                                                        "time", 50f, "easetype", "linear",
                                                        "looptype", "loop"));

            selectionWheelToDisable.SetActive(false);
            StartCoroutine(DisableCanvas());

            // tween from main camera to new death cam
            //deathTexture.SetActive(true);
            deathCanvas.SetActive(true);
            GameObject deathTexture = GameObject.Find("DeathCamTexture");
            deathTexture.gameObject.AddComponent<CameraTween>();
            deathTexture.gameObject.GetComponent<CameraTween>().FadeIn();

            hasDied = true;
        }
               
    }

    private IEnumerator DisableCanvas()
    {
        yield return new WaitForSeconds(2f);
        canvasToDisable.SetActive(false);
    }
}
