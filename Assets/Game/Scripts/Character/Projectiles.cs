using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles: MonoBehaviour
{
    [SerializeField] GameObject target;

    [SerializeField] float force = 20f; //fake Value
    [SerializeField] float reachableDistance = 10f; //fake Value
    [SerializeField] float distanceTravelled = 0f;
    [SerializeField] float speed = 1f; //fake Value
    [SerializeField] Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void initDirection(Vector3 v3)
    {
        //direction = new Vector3(v3);
    }

    void initDirection(float x, float y, float z)
    {
        direction = new Vector3(x, y, z);

    }

    // Update is called once per frame
    void Update()
    {
        // should it follow moving object?
        if (distanceTravelled >= reachableDistance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            transform.Translate(direction / speed * Time.deltaTime);
            distanceTravelled += speed * Time.deltaTime;
        }

    }

    public void SetTarget(GameObject target)
    {
        this.target = target;

        //get direction from target
    }


    private void OnTriggerEnter(Collider other)
    {
        Actor otherActor = other.GetComponent<Actor>();

        if (otherActor != null)
        {
            if (!otherActor.GetHealth().GetIsDead())
            {
                otherActor.GetHealth().Hit(force);

                // do I have to destory it here or Actor.Update will do it
                //if (otherActor.GetHealth().GetCurrentHealth() <= 0)
                //{
                //    Destroy(otherActor.gameObject);
                //}
            }
        }
        Destroy(this.gameObject);
    }

    //private void OnCollisionEnter(Collision collision) //do I need this
    //{
    //    Actor otherActor = other.GetComponent<Actor>();

    //    if (otherActor != null)
    //    {
    //        if (otherActor.GetCurrentHealth() > 0)
    //        {
    //            otherActor.Hit(projectileHit);


    //            // do I have to destory it here or Actor.Update will do it
    //            if (otherActor.GetCurrentHealth() < 0)
    //            {
    //                Destroy(otherActor.gameObject);
    //            }
    //        }
    //    }
    //    Destroy(this.gameObject);
    //}
}
