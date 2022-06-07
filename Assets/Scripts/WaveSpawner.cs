using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public int EnemyTypes;
    public Transform[] SpawnPoints;
    public GameObject[] EnemiesToSpawn;
    float waveCountDown;
    int spawnCount = 0;
    int waveIndex = 0;
    public Wave[] waves;

    int[] amountOfEnemies;
    List<GameObject> Enemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        amountOfEnemies =  new int[EnemyTypes];
        //check if the array is empty 
        if (waves == null)
        {
            Debug.LogError("Error: The Array enemiesToSpawn is empty: At script WaveSpawner");
            return;
        }
        //Check for dead enemies
        InvokeRepeating("CheckForEnemies", 5f, 3f);
        //Fill array with script int variables
        //(SEE FUNCTION FOR MORE INFO)
        FillEnemyArray();
    }

    // Update is called once per frame
    void Update()
    {
        //All enemies from current wave have been spawned
        //So we stop the wave timer
        if (WaveComplete())
        {
            //Debug.Log("Wave Finished!");

            //if enemies are all dead start new wave.
            if (Enemies.Count <= 0)
            {
                //playerStats.Waves += 1;
                StartNewWave();
            }
            return;
        }

        //When timer reaches 0
        if (waveCountDown <= 0)
        {
            //spawn enemy.
            SpawnEnemy();

            //reset the timer
            waveCountDown = 1 / waves[waveIndex].rate;
        }
        waveCountDown -= Time.deltaTime;
    }
    void CheckForEnemies()
    {
        //If there is no gameobject in the list remove it
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i] == null)
            {
                Enemies.RemoveAt(i);
            }
        }
    }
    void StartNewWave()
    {
        //Stop when there is no more waves
        if (waveIndex == waves.Length - 1)
            return;

        //reset counter and start new wave
        waveIndex += 1;
        spawnCount = 0;
        Debug.Log("Wave =" + waveIndex);
        //DisplayText.GetComponent<DisplayText>().DisplayNewText("Wave " + (waveIndex + 1));

        FillEnemyArray();
    }
    void SpawnEnemy()
    {
        bool flag = false;
        int attempts = 0;

        //loop until we found a Enemy that can be spawned
        while (!flag)
        {
            if (attempts >= 100)
            {
                Debug.LogError("Failed to Find Enemy to spawn: Attempted: " + attempts);
                flag = true;
            }

            //Generate random number based on how many enemies to spawn
            int rdm = Random.Range(0, amountOfEnemies.Length);
            int rdm2 = Random.Range(0, SpawnPoints.Length);

            //We need to be able to connect the array that holds the enemiesToSpawn
            //To check on the amount of enemies and connect that to see if it can can spawn
            //EX: rdm lands on 2 which is EnemiesToSpawn[2] which equals to kamikaze
            //Enemy. We then go and check on amountOfEnemies to see if amountOfEnemies.Kami-
            //kaze Enemy is not equal to 0 which means to stop spawning that certain type of enemy.


            //Enemy can be spawned
            if (amountOfEnemies[rdm] != 0)
            {
                //Subtract the index
                amountOfEnemies[rdm] -= 1;

                GameObject e = Instantiate(EnemiesToSpawn[rdm], SpawnPoints[rdm2]);
                //Add Enemy 
                Enemies.Add(e);

                //Stop loop
                flag = true;

            }
            attempts += 1;
        }


        //incrase the spawn count
        spawnCount += 1;
    }
    void FillEnemyArray()
    {
        //Fill the enemy array with the Amount of enemies script variables. This is to allow for
        //naming shown in the inspector. The ints in the script get stored into the array based
        //on the index 
        for (int i = 0; i < amountOfEnemies.Length; i++)
        {
            switch (i)
            {
                case 0:
                    amountOfEnemies[i] = waves[waveIndex].StandardZombie;
                break;
                case 1:
                    amountOfEnemies[i] = waves[waveIndex].ConeZombie;
                break;
            }
        }
    }
    bool WaveComplete()
    {
        for (int i = 0; i < amountOfEnemies.Length; i++)
        {
            if (amountOfEnemies[i] > 0)
                return false;

            if (amountOfEnemies[i] <= 0 && i == amountOfEnemies.Length - 1)
                return true;
        }
        return false;
    }
}
