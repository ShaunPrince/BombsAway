using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthTank : MonoBehaviour
{
    public GameObject healthTank;
    public float normalizingNum;
    public Color[] healthColors;
    private PlayerDamageEntity player;
    private float maxHealth;
    private float prevHealth;
    private Color prevColor;

    private HealthTankLights tankLights;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerPlane").GetComponent<PlayerDamageEntity>();
        tankLights = this.GetComponent<HealthTankLights>();
        prevHealth = player.health;
        maxHealth = prevHealth;
        prevColor = healthColors[0];
        UpdateTankValue();
        UpdateTankColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.health != prevHealth)
        {
            UpdateTankValue();
            UpdateTankColor();
            prevHealth = player.health;
        }
    }

    private void UpdateTankValue()
    {
        iTween.ValueTo(gameObject, iTween.Hash( "from", prevHealth, "to", player.health,
                                                "time", .2f, "easetype", "linear",
                                                "onupdate", "ChangeHealthHight"));
    }

    private void UpdateTankColor()
    {
        float time = .5f;
        // green
        if (player.health > maxHealth/3 * 2)
        {
            tankLights.SetTankLightMaterial(0);
            iTween.ValueTo(gameObject, iTween.Hash("from", prevColor, "to", healthColors[0],
                                                    "time", time, "easetype", "linear",
                                                    "onupdate", "ChangeColor"));
        }
        // yellow
        else if (player.health > maxHealth / 3 )
        {
            tankLights.SetTankLightMaterial(1);
            iTween.ValueTo(gameObject, iTween.Hash("from", prevColor, "to", healthColors[1],
                                                    "time", time, "easetype", "linear",
                                                    "onupdate", "ChangeColor"));
        }
        // red
        else
        {
            tankLights.SetTankLightMaterial(2);
            iTween.ValueTo(gameObject, iTween.Hash("from", prevColor, "to", healthColors[2],
                                                    "time", time, "easetype", "linear",
                                                    "onupdate", "ChangeColor"));
        }
    }

    private void ChangeColor(Color newColor)
    {
        healthTank.GetComponent<Renderer>().material.SetColor("Color_A0B3E9A3", newColor);
        prevColor = newColor;
    }

    private float NormalizeHealth(float num)
    {
        // normalize the health between -1 and 1
        float percentMaxVal = num / maxHealth;
        float precentBetween02 = percentMaxVal * (normalizingNum * 2);
        return precentBetween02 - normalizingNum;
    }

    private void ChangeHealthHight(float amount)
    {
        healthTank.GetComponent<Renderer>().material.SetFloat("Vector1_59743A23", NormalizeHealth(amount));
    }
}
