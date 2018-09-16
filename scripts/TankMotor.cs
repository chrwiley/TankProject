using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMotor : MonoBehaviour
{
    public TankData data;

    private CharacterController characterController; //holds our character controller component
    public GameObject shellPrefab;
    public Transform shellSpawn;

    

    // Use this for initialization
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();  //stores in a variable

    }

    
    //Move function
    public void Move(float speed)
    {
        Vector3 speedVector = transform.forward * speed; //creates vector, points it in the right direction applies speed

        characterController.SimpleMove(speedVector); //calls simple move and applies time.delta time to convert to meters per second
    }

    //turn function
    public void Rotate(float speed)
    {
        Vector3 rotateVector = Vector3.up * speed * Time.deltaTime;  //creates vector, starts the rotation, applies speed, applies time.deltatime

        transform.Rotate(rotateVector, Space.Self); // rotates in local space
    }

   

}