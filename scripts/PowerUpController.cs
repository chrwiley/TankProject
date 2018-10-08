using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{

    private List<Powerup> powerups;  //dynamic list for powerups
    public TankData data;
    

    // Use this for initialization
    void Start ()
    {
        powerups = new List<Powerup>();
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        List<Powerup> expiredPowerups = new List<Powerup>();

        
        //loop through all the powerups
        foreach (Powerup power in powerups)
        {
            power.duration -= Time.deltaTime; //subtracts from power up timer 

            if (power.duration <=0)  //if power up time has run out
            {
                power.OnDeactivate(data);
                expiredPowerups.Add(power);  //add to expired list 
            }
        }

        foreach(Powerup power in expiredPowerups) // update expired list and remove
        {
            power.OnDeactivate(data);
            powerups.Remove(power);

            expiredPowerups.Clear(); //emptys list
        }

	}

    public void Add (Powerup powerup)
    {
        powerup.OnActivate(data);

        if (!powerup.isPermanent) //only adds to list if powerup is not permanent
        {
            powerups.Add(powerup);
        }
        
    }

    public void Remove(Powerup powerup)
    {
        powerup.OnDeactivate(data);
        powerups.Remove(powerup);
    }


}
