using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour {

    [HideInInspector]
    public Unit unit;

    bool isAttacking = false;
    float attackCountDown = 0;
    GameObject currentTarget;

    // Use this for initialization
    void Start () {
        unit = GetComponent<Unit>();
	}

	void Update () {
        RaycastHit hitInfo;
        float dist = .3f;
        Ray ray = new Ray(transform.position,transform.forward);
        
        LayerMask layerMask = LayerMask.GetMask("Plant");
        if(Physics.Raycast(ray,out hitInfo,dist,layerMask))
        {
            Debug.DrawLine(ray.origin, hitInfo.point,Color.red);
            //stop moving and start attacking
            unit.CanMove = false;
            isAttacking = true;
            currentTarget = hitInfo.collider.gameObject;
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * dist);
            //start moving and stop attacking
            unit.CanMove = true;
            isAttacking = false;
            currentTarget = null;
        }


        //If base is in attack range
        if(isAttacking && attackCountDown <= 0)
        {
            //attack and set timer for attack rate.
            Attack();
            attackCountDown = 1 / unit.AttackRate;
        }
        //Reset timer
        attackCountDown -= Time.deltaTime;

        if(unit.CanMove == true && unit.IsGrounded == true)
        {
            //Move To the Left INFINITELY
            Vector3 pos = new Vector3(0, 0, (unit.Speed * Time.deltaTime));
            this.gameObject.transform.position -= pos;
        }

        
	}

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            unit.IsGrounded = true;
        }
    }

    void OnCollsionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            unit.IsGrounded = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }

    }
    void Attack()
    {
        currentTarget.GetComponent<Tower>().TakeDamage(25);    
    }

}
