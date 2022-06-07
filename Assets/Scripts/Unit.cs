using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit: MonoBehaviour {
    /// <summary>
    /// Takes Care of all the attributes and if it dies
    /// </summary>
    /// 
    [SerializeField]
    private int health = 100;
    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private int attackDamage = 1;
    [SerializeField]
    private float attackRate = 0.5f;

    public int value = 10;

    private bool canMove = true;
    private bool isGrounded = false;

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }
    public bool CanMove
    {
        get
        {
            return canMove;
        }
        set
        {
            canMove = value;
            if(canMove)
            {
                GetComponent<Rigidbody>().WakeUp();
            }
            if(!canMove)
            {
                GetComponent<Rigidbody>().WakeUp();
            }
        }
    }

    public int AttackDamage
    {
        get
        {
            return attackDamage;
        }
        set
        {
            attackDamage = value;
        }
    }

    public float AttackRate
    {
        get
        {
            return attackRate;
        }
        set
        {
            attackRate = value;
        }
    }

    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }
        set
        {
            isGrounded = value;
        }
    }

    PlayerStats playerStats;

    void Start()
    {
        playerStats = PlayerStats.Instance;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        //if health is 0
        if(health <= 0)
        {
            //kill me and add my death value to players money
            Destroy(gameObject,0.05f);

            //playerStats.Money += value;
            //playerStats.UpdateMoneyText();
        }
    }

}
