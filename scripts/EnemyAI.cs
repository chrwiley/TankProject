using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public TankMotor motor;  //call other scripts 
    public TankData data;
    public TankFiring firing;
    public TankHealth thealth;

    public Transform[] waypoints; //array for waypoints

    private int currentWaypoint = 0;  //information for waypoints 
    
    private Transform tf;

    private float nextFire;  //fire delay timer 

    
    public enum LoopType { Stop, Loop, PingPong }; //for the waypoint loop
    public LoopType loopType;
    private bool isPatrolForward = true;

    public enum AIState { Chase, ChaseAndFire, CheckForFlee, Flee, Rest };
    public AIState aiState = AIState.Chase;

    public Transform target;
    
    public int avoidanceStage = 0;  //information for ai state
    public float exitTime;
    public float maxHealth = 100;


    //saving the transform
    public void Awake()
    {
        tf = gameObject.GetComponent<Transform>();
    }

    private void Update()
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
    {
        //make the enemy tank go through the waypoints 
        if (motor.RotateTowards(waypoints[currentWaypoint].position, data.turnLeftSpeed)) //we are already in the right direction
        {
            //Do nothing!
        }
        else
        {
            //move forward
            motor.Move(data.moveForwardSpeed);
        }

        //how close are we to the waypoint
        if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - tf.position) < (data.closeEnough * data.closeEnough))
        {
            if (loopType == LoopType.Stop)
            {
                //advance
                if (currentWaypoint < waypoints.Length - 1)
                {
                    currentWaypoint++;
                }
            }
            else if (loopType == LoopType.Loop)
            {
                //advance
                if (currentWaypoint < waypoints.Length - 1) //if we are in range keep going
                {
                    currentWaypoint++;
                }
                else
                {
                    currentWaypoint = 0; // go to first waypoint 
                }
            }
            else if (loopType == LoopType.PingPong)
            {
                if (isPatrolForward)
                {
                    //advance
                    if (currentWaypoint < waypoints.Length - 1)
                    {
                        currentWaypoint++;
                    }//keep going if we are in range
                    else
                    {
                        isPatrolForward = false;
                        currentWaypoint--;//reverse direction and go back one
                    }
                }
                else
                {
                    //keep going
                    if (currentWaypoint > 0)
                    {
                        currentWaypoint--;
                    }
                    else
                    {
                        //reverse direction, go to next waypoint
                        isPatrolForward = true;
                        currentWaypoint++;
                    }
                }
            }
        }
        if (aiState == AIState.Chase)
        {
            // do we avoid or do we chase
            if (avoidanceStage != 0)
            {
                DoAvoidance();
            }
            else
            {
                DoChase();
            }

            // what is our health status
            if (thealth.currentHealth < maxHealth * 0.5f)
            {
                ChangeState(AIState.CheckForFlee);
            }
            else if (Vector3.SqrMagnitude(target.position - tf.position) <= (data.aiSenseRadius * data.aiSenseRadius))
            {
                ChangeState(AIState.ChaseAndFire);
            }
        }
        else if (aiState == AIState.ChaseAndFire)
        {
            
            if (avoidanceStage != 0)
            {
                DoAvoidance();  //run away
            }
            else
            {
                DoChase();

                // can we shoot yet?
                if (Time.time > data.lastShootTime + data.fireRate)
                {
                    firing.Fire();
                    data.lastShootTime = Time.time;
                }
            }
            // Check for Transitions
            if (thealth.currentHealth < maxHealth * 0.5f)
            {
                ChangeState(AIState.CheckForFlee);
            }
            else if (Vector3.SqrMagnitude(target.position - tf.position) <= (data.aiSenseRadius * data.aiSenseRadius))
            {
                ChangeState(AIState.Chase);
            }
        }
        else if (aiState == AIState.Flee)
        {
            // Perform Behaviors
            if (avoidanceStage != 0)
            {
                DoAvoidance();
            }
            else
            {
                DoFlee();
            }

            // Check for Transitions
            if (Time.time >= data.stateEnterTime + 30)
            {
                ChangeState(AIState.CheckForFlee);
            }
        }
        else if (aiState == AIState.CheckForFlee)
        {
            // Perform Behaviors
            CheckForFlee();

            // Check for Transitions
            if (Vector3.SqrMagnitude(target.position - tf.position) <= (data.aiSenseRadius * data.aiSenseRadius))
            {
                ChangeState(AIState.Flee);
            }
            else
            {
                ChangeState(AIState.Rest);
            }
        }
        else if (aiState == AIState.Rest)
        {
            // Perform Behaviors
            DoRest();

            // Check for Transitions
            if (Vector3.SqrMagnitude(target.position - tf.position) <= (data.aiSenseRadius * data.aiSenseRadius))
            {
                ChangeState(AIState.Flee);
            }
            else if (thealth.currentHealth >= maxHealth)
            {
                ChangeState(AIState.Chase);
            }
        }
    }
    void DoChase()
    {
        motor.RotateTowards(target.position, data.turnLeftSpeed);

        if (CanMove(data.moveForwardSpeed)) //can we move moveForwardSpeed units away (this is actually looking for collisions
        {
            motor.Move(data.moveForwardSpeed);
        }
        else
        {
            avoidanceStage = 1; //enters the avoidance stage
        }
    }

    void DoAvoidance()
    {
        if (avoidanceStage == 1)
        {
            motor.Rotate(-1 * data.turnLeftSpeed);

            if (CanMove(data.moveForwardSpeed)) //if tank can move go to stage 2
            {
                avoidanceStage = 2;

                exitTime = data.avoidanceTime; //how long will we stay in avoidance
            }

            //Keep going
        }
        else if (avoidanceStage == 2)
        {
            if (CanMove(data.moveForwardSpeed))  //if we can move do it 
            {
                exitTime -= Time.deltaTime;  //counting down
                motor.Move(data.moveForwardSpeed);

                if (exitTime <= 0) //is there avoidance time left
                {
                    avoidanceStage = 0; //return to chase stage
                }
            }
            else
            {
                avoidanceStage = 1; //can't move go back to stage 1
            }
        }
    }

    bool CanMove(float speed)
    {
        RaycastHit hit; //did our raycast hit anything

        if (Physics.Raycast(tf.position, tf.forward, out hit, speed))
        {
            if (!hit.collider.CompareTag("Enemy")) //don't hit me
            {
                return false;
            }

        }


        return true;
    }

    public void CheckForFlee()
    {
        ChangeState(AIState.Flee);
    }

    public void DoRest()
    {
        thealth.currentHealth += data.restingHealRate * Time.deltaTime;  //increase health per second

        thealth.currentHealth = Mathf.Min(thealth.currentHealth, 100);  //when I used data.maxHealth I got an error

    }

    public void ChangeState(AIState newState)
    {
        aiState = newState;  //change state
        data.stateEnterTime = Time.time;  //what time did we change state
    }

    public void DoFlee()
    {
        Vector3 vectorToTarget = target.position - tf.position;  //target position - ai position

        Vector3 vectorAwayFromTarget = -1 * vectorToTarget; //flip by -1 and move away from target 

        vectorAwayFromTarget.Normalize();  //give it a magnitude of 1 

        vectorAwayFromTarget *= data.fleeDistance;   //multiply by itself to give a vector

        Vector3 fleePosition = vectorAwayFromTarget + tf.position;  //position away from target
        motor.RotateTowards(fleePosition, data.turnLeftSpeed);
        motor.Move(data.moveForwardSpeed);
    }
}






