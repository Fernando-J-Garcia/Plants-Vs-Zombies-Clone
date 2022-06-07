using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunWorldSpawner : MonoBehaviour
{
    public GameObject SunBlob;
    Transform SpawnPoint;
    float FireRate;
    float fireCountDown;
    // Start is called before the first frame update
    void Start()
    {
        GameObject g = new GameObject("SunBlobSpawnPoint");
        SpawnPoint = g.transform;
        SpawnPoint.position = new Vector3(0, 0, 0);

        FireRate = Settings.SunBlobWorldSpawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        //Shoot according to fire rate
        if (fireCountDown <= 0)
        {
            Spawn();
            fireCountDown = 1 / FireRate;
        }

        fireCountDown -= Time.deltaTime;
    }
    void Spawn()
    {
        //Spawn the sun blob somewhere in between the world bounds randomly.
        //TODO make the world bounds be a dynamic variable that can be set no matter what the world size is.
        //THIS IS REALLY BAD TO DO!!
        float rdmX = Random.Range(-2f, 3f);
        float rdmZ = Random.Range(-13f, 0f);

        SpawnPoint.position = new Vector3(rdmX,7,rdmZ);

        //Create new blob
        GameObject bullet = Instantiate(SunBlob, SpawnPoint.position, SpawnPoint.rotation);

        

        //Destroy ourselves after our lifetime runs out
        Destroy(bullet,Settings.SunBlobLifeDurartion);
    }
}
