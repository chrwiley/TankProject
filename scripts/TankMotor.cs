using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMotor : MonoBehaviour
{
    public TankData data;
    private Transform tf;

    private CharacterController characterController; //holds our character controller component
    public GameObject shellPrefab;
    public Transform shellSpawn;

    


    

    // Use this for initialization
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();  //stores in a variable
        tf = gameObject.GetComponent<Transform>();

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

    //stub function RotateTowards(Target, speed)  if we need to move it returns true, otherwise we are already in the right direction
    public bool RotateTowards (Vector3 target, float speed)
    {
        Vector3 vectorToTarget = target - tf.position;

        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget);  //slows things down a bit

        if (targetRotation == tf.rotation)
        {
            return false;
        }

        tf.rotation = Quaternion.RotateTowards(tf.rotation, targetRotation, data.turnLeftSpeed * Time.deltaTime);


        return true;
    }

    public void TakeDamage(int amount) //subtracts damage from health
    {
        var currentHealth = data.maxHealth;
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Dead!");
        }
    }

}