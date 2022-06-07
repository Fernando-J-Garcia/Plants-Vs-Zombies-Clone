using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawnMower : MonoBehaviour
{
    private bool isTriggered = false;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            isTriggered = true;
            Destroy(gameObject, 5f);
        }
        if(isTriggered)
        {
            //Move To the right INFINITELY
            Vector3 pos = new Vector3(0, 0, (-8 * Time.deltaTime));
            this.gameObject.transform.position -= pos;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!isTriggered)
        {
            isTriggered = true;
            Destroy(gameObject, 5f);
        }
        Destroy(collision.gameObject, 0.5f);
    }
}
