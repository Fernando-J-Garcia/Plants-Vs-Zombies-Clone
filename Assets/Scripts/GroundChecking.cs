using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecking : MonoBehaviour {
    /// <summary>
    /// This script is applied to a ground. it checks 
    /// if a player is on the ground. if so the variable 
    /// isGrounded is set to true
    /// </summary>
    /// 

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Unit>().IsGrounded = true;
        }
    }

    void OnCollsionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Unit>().IsGrounded = false;
        }
    }
}
