using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCanvas : MonoBehaviour
{
    public List<GameObject> buttons;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject button in buttons)
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
        foreach (GameObject button in buttons)
        {
            if (button.GetComponent<FadeButton>())
            {
                button.SetActive(true);
                button.GetComponent<FadeButton>().FadeIn();
            }
        }
    }
}
