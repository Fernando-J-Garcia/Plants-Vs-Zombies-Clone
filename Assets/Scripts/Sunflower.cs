using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunflower : MonoBehaviour
{
    public GameObject SunBlob;
    public Transform FirePoint;
    public float power;
    public float FireRate;
    float fireCountDown;
    // Start is called before the first frame update
    void Update()
    {
        if (!GetComponent<Tower>().isActive)
        {
            return;
        }
        //Shoot according to fire rate
        if (fireCountDown <= 0)
        {
            Shoot();
            fireCountDown = 1 / FireRate;
        }

        fireCountDown -= Time.deltaTime;
    }

    void Shoot()
    {
        /*
        Quaternion rotation = FirePoint.transform.rotation;
        Vector3 rdm = new Vector3(0, Random.Range(-50, 50), 0);
        rotation.SetLookRotation(rdm);
        */

        //Create new bullet 
        GameObject bullet = Instantiate(SunBlob, FirePoint.position, FirePoint.rotation);

        float rdm = Random.Range(-0.5f, 0.5f);
        Vector3 force = new Vector3(rdm, 1, 0.5f) * power;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = force;

        //if something hasn't been hit in 7 seconds.Destroy ourselves
        Destroy(bullet, Settings.SunBlobLifeDurartion);
    }

}
