using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{


    public float moveForwardSpeed = 3; //meters per second
    public float moveBackwardSpeed = 3;
    public float turnLeftSpeed = 180;//degrees per second
    public float turnRightSpeed = 180;

    public int playerNumber; //tank numbers

    public AudioClip tankSound;


    public float fireForce = 1000; //force of shell
    public float fireRate = 4; //time delay of refire

    public float maxHealth = 100;  //maximum health
    public float currentHealth;

    public Transform fireTransform;
    public float minLaunchForce = 15f; //smallest force
    public float maxLaunchForce = 30f; //largest force
    public float maxChargeTime = 0.75f;

    public float closeEnough = 1.0f;  //how close do we want enemy AI to get 
    public float fleeDistance = 1.0f; //run, run away
    public float avoidanceTime = 2.0f;
    public float stateEnterTime; 
    public float aiSenseRadius = 1;
    public float restingHealRate = 1;
    public float lastShootTime=0;

}

