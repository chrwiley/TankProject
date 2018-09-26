using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour {

    public TankData data;
    public float currentHealth;
     

    public void TakeDamage(int amount) //subtracts damage from health
    {
        float currentHealth = data.maxHealth;
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Dead!");
        }
    }
}


