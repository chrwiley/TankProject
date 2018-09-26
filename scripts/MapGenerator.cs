using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public int rows;
    public int cols;

    public int mapSeed;

    private float roomWidth = 50.0f;
    private float roomHeight = 50.0f;
    private Room[,] grid; //two number array for rooms

    public GameObject[] gridPrefabs;

    


	// Use this for initialization
	void Start ()
    {
        GenerateGrid();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public int DateToInt(DateTime dateToUse)
    {
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }

    //return a random room
    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];
    }

    //for loop to generate map
    public void GenerateGrid()
    {
       UnityEngine.Random.seed = DateToInt(DateTime.Now); // sets the seed for random generation

        grid = new Room[cols, rows]; //clears out grid for new generation of map

        for (int i = 0; i<rows; i++) // for each row
        {
            for (int j = 0; j<cols; j++) //for each column
            {
                float xPosition = roomWidth * j;  //where is the room going 
                float zPosition = roomHeight * i;

                Vector3 newPosition = new Vector3(xPosition, 0.0f, zPosition); //determine the grid 

                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject; //create the room

                
                tempRoomObj.transform.parent = this.transform; //creates the parent

                tempRoomObj.name = "Room_" + j + "," + i;  //gives the room a name

                Room tempRoom = tempRoomObj.GetComponent<Room>();  //gets the room object

                if (i==0) //if on bottom row, need north door open
                {
                    tempRoom.doorNorth.SetActive(false);
                }
                else if (i==rows-1) //if on top row, need south door open
                {
                    tempRoom.doorSouth.SetActive(false);
                }
                else //everything in the middle needs both North and South open
                {
                    tempRoom.doorNorth.SetActive(false);
                    tempRoom.doorSouth.SetActive(false);
                }

                if (j==0) //if in first column, need east door open
                {
                    
                }
                else if (j==cols-1) //if in last column, need west door open
                {
                    tempRoom.doorWest.SetActive(false);
                }
                else //everything in the middle needs both east and west doors open
                {
                    tempRoom.doorEast.SetActive(false);
                    tempRoom.doorWest.SetActive(false);
                }
                
                grid[j, i] = tempRoom; // stores it in the array


            }
        }

    }
}
