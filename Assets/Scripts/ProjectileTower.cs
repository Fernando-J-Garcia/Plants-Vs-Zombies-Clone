using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTower : MonoBehaviour
{    
    public float Range = 10;
    public float RotationSpeed = 5;    
    public float FireRate = 2f;

    public GameObject BulletPrefab;
    public Transform FirePoint;

    float fireCountDown = 0;
    private GameObject bulletsParent;//Parent all the spawned bullets so that is doesn't clutter the scene manager
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        if(animator == null)
        {
            Debug.LogError("Missing animator Component");
        }

        bulletsParent = new GameObject("Bullets");
        bulletsParent.transform.SetParent(transform);
    }

    // Update is called once per frame
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
        animator.SetTrigger("Shoot");

        //Create new bullet 
        GameObject bullet = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        bullet.transform.SetParent(bulletsParent.transform);

        //if something hasn't been hit in 7 seconds.Destroy ourselves
        Destroy(bullet, 7);
    }

    void OnDrawGizmosSelected()
    {
        //Show tower radius in scene
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
