using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamageEntity : DamageableEntity
{
    public RepairSystemManager repSysMan;
    //public GameObject gameManager;
    public override void TakeDamage(float incomingDamage, EAllegiance allegianceOfIncomingDamage)
    {
        if (allegianceOfIncomingDamage != allegiance && !isInvincible)
        {
            if (allegianceOfIncomingDamage == EAllegiance.Enemy)
            {
                repSysMan.RollForSteamLoss();
            }
            health -= incomingDamage;
            FindObjectOfType<AudioManager>().PlayGotHit();


            if (health <= 50)
            {
                FindObjectOfType<AudioManager>().PlayAlarm();  //need to fix creating instances
 
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
        }
    }
}
