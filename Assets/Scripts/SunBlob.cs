using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBlob : MonoBehaviour
{
    bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MouseRaycast.GotHit)
        {
            RaycastHit hit = MouseRaycast.HitInfo;
            if (hit.collider.gameObject == this.gameObject && isDead == false)
            {
                AudioManager.instance.Play("Collect");
                isDead = true;
                GetComponent<Rigidbody>().velocity = Vector3.up;
                PlayerStats.Instance.Money += 25;
                Destroy(gameObject,0.1f);
            }
        }
    }
}
