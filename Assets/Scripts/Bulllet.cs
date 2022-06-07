using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulllet : MonoBehaviour {

    public float Speed = 5;
    public int Damage = 50;
    [Tooltip("Explosion Radius - Set to 0 for Normal Bullet ")]
    public float radius = 8f;

    private bool spawnedWithNoTarget;

    public GameObject Particles;

    Transform target;
    public void Seek(Transform t)
    {
        target = t;
    }
	
	// Update is called once per frame
    void Update () {
        ///
        /// NOTE: If a bullet is going backward its PROBABLY due to
        /// the rotation of the firepoint. Make sure the RED axis is
        /// pointing to where the gun is FACING. 
        ///

        transform.Translate(transform.forward * (5 * Time.deltaTime), Space.World);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            
            Unit unit = collider.gameObject.GetComponent<Unit>();
            if(unit == null)
            {
                Debug.Log(collider.gameObject.name+ " Does not contain Unit script");
            }
            //For singe shot towers
            if (radius <= 0)
            {
                unit.TakeDamage(Damage);
            }
            else//For Towers that can shoot multiple enemeies. Like an explosion.
            {
                Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, radius);

                //Grab all the enemies thats withn the shot radius
                //And damage them all.
                for (int i = 0; i < cols.Length; i++)
                {
                    unit = cols[i].GetComponent<Unit>();

                    if (unit != null)
                    {
                        unit.TakeDamage(Damage);
                    }
                }
            }
            if (Particles != null)
            {
                //TODO: Consider making an object pool and use them to turn the
                //particles on and off.
                GameObject ps = Instantiate(Particles, transform.position, Quaternion.identity);
                Destroy(ps, 0.6f);
            }
            else
            {
                //Debug.Log(this.gameObject.name + "has no particles :(");
            }

            AudioManager.instance.Play("Hit");
            Destroy(gameObject);
        }
        if (collider.gameObject.tag == "Plant")
        {
            Physics.IgnoreCollision(collider, GetComponent<Collider>());
        }
    }
}
