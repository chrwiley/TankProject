using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTanks : MonoBehaviour {

    public float numberEnemyTanks;

    public TankData data;
    public GameObject enemyTankPrefab;
    public Transform enemyTankSpawn;
    public GameObject playerTankPrefab;
    public Transform playerTankSpawn;


    // Use this for initialization
    void Start ()
    {
        CreateTanks();

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void CreateTanks() //create the enemy tanks 
    {
        var playerTank = (GameObject)Instantiate(playerTankPrefab, playerTankSpawn.position, playerTankSpawn.rotation);

        for (int i = 0; i <= numberEnemyTanks; i++)
        {
            var enemyTank = (GameObject)Instantiate(enemyTankPrefab, enemyTankSpawn.position, enemyTankSpawn.rotation);
        }
    }
}
