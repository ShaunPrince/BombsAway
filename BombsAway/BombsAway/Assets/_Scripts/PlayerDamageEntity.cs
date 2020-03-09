using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamageEntity : DamageableEntity
{
    public RepairSystemManager repSysMan;
    public float maxHealth;

    private bool alarmPlaying = false;
    //public GameObject gameManager;
    private void Start()
    {
        maxHealth = health;
    }

    public override void TakeDamage(float incomingDamage, EAllegiance allegianceOfIncomingDamage)
    {
        if (allegianceOfIncomingDamage != allegiance )
        {
            if (allegianceOfIncomingDamage == EAllegiance.Enemy)
            {
                repSysMan.RollForSteamLoss();
            }
            health -= incomingDamage;
            if(health > maxHealth)
            {
                health = maxHealth;
            }
            if (incomingDamage >= 20.0f)
            {
                FindObjectOfType<AudioManager>().PlayMissleHit();
            }
            else
            {
                FindObjectOfType<AudioManager>().PlayGotHit();
            }


            if (health <= 50 && !alarmPlaying)
            {
                FindObjectOfType<AudioManager>().PlayAlarm();
                alarmPlaying = true;
            }
            //Debug.Log(this + " Is taking damage");
            if (health <= 0)
            {
                Die();
            }
            
        }
    }

    private void Die()
    {
        // Show player dying
        this.GetComponent<PlayerEndGame>().ShowPlayerDying();
        AudioSource deathSound = this.GetComponent<AudioSource>();
        deathSound.PlayOneShot(deathSound.clip);

        // Fragment the player
        StartCoroutine(DelayFragmentation());


        //Load Game Over
        //gameManager.GetComponent<PauseGame>().LoadMainMenu();
        //SceneManager.LoadScene("MainMenu");

    }

    private IEnumerator DelayFragmentation()
    {
        yield return new WaitForSeconds(2f);
        PlayerFragmentOnDeath fragment = this.GetComponent<PlayerFragmentOnDeath>();
        if (fragment != null)
        {
            fragment.Fragment();
            fragment.HideObjects();
        }
    }
}
