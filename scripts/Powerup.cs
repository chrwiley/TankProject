using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Powerup  {

    public float speedModifier;
    public float healthModifier;
    public float maxHealthModifier;
    public float fireRateModifier;

    public float duration;  //how long power ups last

    public bool isPermanent; // is the powerup permanent or not

    public void OnActivate (TankData target)
    {
        target.moveForwardSpeed += speedModifier;
        target.currentHealth += healthModifier;
        target.maxHealth += maxHealthModifier;
        target.fireRate += fireRateModifier;
    }

    public void OnDeactivate (TankData target)
    {
        target.moveForwardSpeed -= speedModifier;
        target.currentHealth -= healthModifier;
        target.maxHealth -= maxHealthModifier;
        target.fireRate -= fireRateModifier;
    }




}
