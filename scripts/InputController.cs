using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    public TankMotor motor;  //call other scripts 
    public TankData data;
    public TankFiring firing;

    
    private const char W = 'w';  //had to make WASD characters to work in the switch case
    private const char S = 's';
    private const char A = 'a';
    private const char D = 'd';

    

    private float currentLaunchForce;  //information on the bullets
    private float chargeSpeed;
    private bool fired;  //has it been fired or not 
    private float nextFire;  //fire delay timer 

    public enum InputScheme { WASD, ArrowKeys };  //enum for movement
    public InputScheme input = InputScheme.WASD;

    private void Update()
    {
        switch (input)
        {
            case InputScheme.WASD: //case for using WASD for player 1
                if (Input.GetKey(KeyCode.W))
                {
                    motor.Move(data.moveForwardSpeed);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    motor.Move(-data.moveBackwardSpeed);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    motor.Rotate(-data.turnLeftSpeed);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    motor.Rotate(data.turnRightSpeed);
                }
                break;

        }

        switch (input)
        {
            case InputScheme.ArrowKeys:  //case for using arrows for player 2
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    motor.Move(data.moveForwardSpeed);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    motor.Move(-data.moveBackwardSpeed);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    motor.Rotate(data.turnRightSpeed);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    motor.Rotate(-data.turnLeftSpeed);
                }
                break;
        }

        // Track the current state of the fire button and make decisions based on the current launch force.

        if (Time.time > data.fireRate)
        {
            if (currentLaunchForce >= data.maxLaunchForce && !fired)
            {
                // at max xharge not yet fired
                currentLaunchForce = data.maxLaunchForce;
                firing.Fire();
            }
            else if (Input.GetButtonDown("Fire1"))
            {
                //has the button been pressed for the first time
                fired = false;
                currentLaunchForce = data.minLaunchForce; //sets back to minimum

            }
            else if (Input.GetButton("Fire1") && !fired)
            {
                //holding the button
                currentLaunchForce += chargeSpeed * Time.deltaTime;  //increases force accordingly


            }
            else if (Input.GetButtonUp("Fire1") && !fired)
            {
                //release the button
                firing.Fire();
            }
            nextFire = Time.time + data.fireRate;
        }

        if (data.playerNumber == 2) //continuous shooting for player 2 
        {
            if (Time.time > data.fireRate)
            {
                nextFire = Time.time + data.fireRate;
                firing.Fire();
            }
        }



                
    }
}



    


