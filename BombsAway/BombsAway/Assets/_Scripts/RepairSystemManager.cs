using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairSystemManager : MonoBehaviour
{
    public TankController[] steamTanks;
    public float probOfSteamLoss;
    public float minSteamLoss;
    public float maxSteamLoss;

    public float damageToDealToPlayerPerUnfilledTank;
    
    private float timeSinceLastInternalDamage;
    public float timeBetweenInternalDamageTicks;
    // Start is called before the first frame update
    void Awake()
    {
        steamTanks = GameObject.FindObjectsOfType<TankController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAndApplyInternalDamage();
    }

    public void RollForSteamLoss()
    {
        float randF = Random.Range(0f, 1f);
        Debug.Log(randF);
        if(randF <= probOfSteamLoss)
        {
            int rIndex = Random.Range(0, steamTanks.Length - 1);
            steamTanks[rIndex].LoseSteam(Random.Range(minSteamLoss, maxSteamLoss));
        }
    }

    private void CheckAndApplyInternalDamage()
    {
        if(timeSinceLastInternalDamage >= timeBetweenInternalDamageTicks)
        {
            float internalDmg = damageToDealToPlayerPerUnfilledTank * GetNumOfUnfilledTanks();
            this.GetComponentInParent<PlayerDamageEntity>().TakeDamage(internalDmg, EAllegiance.Internal);
            timeSinceLastInternalDamage -= timeBetweenInternalDamageTicks;
        }
        else
        {
            timeSinceLastInternalDamage += Time.deltaTime;
        }
    }

    private int GetNumOfUnfilledTanks()
    {
        int count = 0;
        foreach(TankController tc in steamTanks)
        {
            if(tc.IsFilled() == false)
            {
                count += 1;
            }
        }
        return count;
    }
}
