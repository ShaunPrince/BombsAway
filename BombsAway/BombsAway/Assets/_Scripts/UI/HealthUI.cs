using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider healthSlider;
    private DamageableEntity playerDamageableEntity;

    private float prevHealthValue;

    // Start is called before the first frame update
    void Start()
    {
        playerDamageableEntity = this.GetComponentInParent<DamageableEntity>();
        prevHealthValue = playerDamageableEntity.health;
        healthSlider.maxValue = playerDamageableEntity.health;
        healthSlider.value = playerDamageableEntity.health;
    }

    // Update is called once per frame
    void Update()
    {
        if ( prevHealthValue != playerDamageableEntity.health )
        {
            healthSlider.value = playerDamageableEntity.health;
            prevHealthValue = playerDamageableEntity.health;
        }
    }
}
