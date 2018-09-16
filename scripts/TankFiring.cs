using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFiring : MonoBehaviour
{
    //public variables
    //public Rigidbody m_Shell;            //reference shell prefab
    

    public TankData data;
    public GameObject shellPrefab;
    public Transform shellSpawn;

    //private member variables
    private string fireButton;
    private float currentLaunchForce;
    private float chargeSpeed;
    private bool fired;


    private void OnEnable()
    {
        currentLaunchForce = data.minLaunchForce;  //sets the minimum when you starting aiming
    }


    private void Start()
    {
       fireButton = "Fire1"; //

       chargeSpeed = (data.maxLaunchForce - data.minLaunchForce) / data.maxChargeTime;  //how long does it take to fully charge
    }

    
    public void Fire()
    {
        
        // Create the Bullet from the Bullet Prefab
        var shell = (GameObject)Instantiate(shellPrefab,
            shellSpawn.position,
            shellSpawn.rotation);

        // Add velocity to the bullet
        shell.GetComponent<Rigidbody>().velocity = shell.transform.forward * data.fireForce;

        // Destroy the bullet after 2 seconds
        Destroy(shell, 2.0f);
    }
}