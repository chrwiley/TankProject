using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour {

    public TankData data;
    
     

    public void TakeDamage(int amount) //subtracts damage from health
    {
        var currentHealth = TankData.maxHealth;
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Dead!");
        }
    }
}


