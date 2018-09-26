using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public Powerup powerup;
    public AudioClip feedback;
    public GameObject spawnedPickup;
    public float spawnDelay;


    private float nextSpawnTime;
    private Transform tf;

    // Use this for initialization
    void Start ()
    {
        tf = gameObject.GetComponent<Transform>();
        nextSpawnTime = Time.time + spawnDelay;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (spawnedPickup == null)  //is there a power up or not 
        {
            if (Time.time > nextSpawnTime) //is it time
            {
                spawnedPickup=Instantiate(spawnedPickup, tf.position, Quaternion.identity) as GameObject;  //spawn it and start timer 
                nextSpawnTime = Time.time + spawnDelay;
            }
        }
        else
        {
            nextSpawnTime = Time.time + spawnDelay;  //postpone
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        PowerUpController powcon = other.GetComponent<PowerUpController>();  //variable to store the other objects power up

        if (powcon != null) // if the object has a power up
        {
            powcon.Add(powerup);  //add to you

            if (feedback != null)
            {
                AudioSource.PlayClipAtPoint(feedback, tf.position, 1.0f);
            }

            Destroy(gameObject);  //destroy the object 
        }

    }
}
