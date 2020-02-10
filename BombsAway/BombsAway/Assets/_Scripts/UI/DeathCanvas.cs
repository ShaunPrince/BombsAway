using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCanvas : MonoBehaviour
{
    public List<GameObject> uiElements;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject button in uiElements)
        {
            button.SetActive(false);
        }

        StartCoroutine(DelayFade());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DelayFade()
    {
        yield return new WaitForSeconds(2f);
        foreach (GameObject uiElement in uiElements)
        {
            if (uiElement.GetComponent<FadeButton>())
            {
                uiElement.SetActive(true);
                uiElement.GetComponent<FadeButton>().FadeIn();
            }
            if (uiElement.GetComponent<FadeText>())
            {
                uiElement.SetActive(true);
                uiElement.GetComponent<FadeText>().FadeIn();
            }
            if (uiElement.GetComponent<FadeAndFlashText>())
            {
                if (uiElement.GetComponent<FadeAndFlashText>().setActive)
                {
                    uiElement.SetActive(true);
                    uiElement.GetComponent<FadeAndFlashText>().FlashDeathReason();
                }
            }
        }
    }
}
