using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour {

    
    public float moveForwardSpeed = 3; //meters per second
    public float moveBackwardSpeed = 3;
    public float turnLeftSpeed = 180;//degrees per second
    public float turnRightSpeed = 180;

    public int playerNumber; //tank numbers


    public float fireForce = 1000; //force of shell
    public float fireRate = 4; //time delay of refire

    public Transform fireTransform;
    public float minLaunchForce = 15f; //smallest force
    public float maxLaunchForce = 30f; //largest force
    public float maxChargeTime = 0.75f;

    public const int maxHealth = 100;  //maximum health
}
