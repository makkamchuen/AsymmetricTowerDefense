using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    private GameObject target;

    [SerializeField] private float damage = 20f; //fake Value
    [SerializeField] private float maxDistance = 7f; //fake Value
    [SerializeField] private float minDistance = 3f;
    [SerializeField] private bool faceRight = true;
    [SerializeField] private float speed = 1f; //fake Value
    [SerializeField] GameObject hitEffect = null;
    [SerializeField][FMODUnity.EventRef] string hitSound;

    private float distanceTravelled = 0f;
    private SpriteRenderer _spriteRenderer;
    private Vector3 direction;
    private HashSet<string> _targetTags;
    private FlyOrGround _flyOrGround;
    private Transform fx;
    private bool _isFacingRight = false;

    private void Start()
    {
        _spriteRenderer = transform.Find("arrow").GetComponent<SpriteRenderer>();
        fx = transform.Find("FX");
    }

    public void InitDirection(Vector3 v3)
    {
        direction = v3;
        if (direction.x > 0)
        {
            _isFacingRight = true;
            Debug.Log("Right");
        }
        if (direction.x < 0)
        {
            Debug.Log("Left");
        }
    }

    public void AddDamage(float value)
    {
        damage += value;
    }

    public float GetMinDistance()
    {
        return minDistance;
    }

    public float GetMaxDistance()
    {
        return maxDistance;
    }

    public void SetTargets(HashSet<string> targetTags)
    {
        _targetTags = targetTags;
    }

    public void SetFlyOrGround(FlyOrGround flyOrGround)
    {
        _flyOrGround = flyOrGround;
    }

    void Update()
    {
        // should it follow moving object?
        // Nah
        if (distanceTravelled >= maxDistance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            transform.Translate(direction / speed * Time.deltaTime);
            distanceTravelled += speed * Time.deltaTime;
        }

        FaceTarget();
        RestrictRotation();
    }

    // public void SetTarget(GameObject target)
    // {
    //     this.target = target;
    //
    //     //get direction from target
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (_targetTags.Contains(other.tag))
        {
            Actor otherActor = other.GetComponent<Actor>();
            if (otherActor != null && ((int) otherActor.GetBaseStats().FlyOrGround & (int) _flyOrGround) != 0 && !otherActor.GetHealth().GetIsDead())
            {
                otherActor.GetHealth().Hit(damage, hitEffect, hitSound);
                Destroy(this.gameObject);
                // do I have to destory it here or Actor.Update will do it
                //if (otherActor.GetHealth().GetCurrentHealth() <= 0)
                //{
                //    Destroy(otherActor.gameObject);
                //}
            }
        }

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

    private void FaceTarget()
    {
        if (_isFacingRight)
        {
            _spriteRenderer.flipY = false;
            fx.localPosition = new Vector3(-0.3f, fx.localPosition.y, fx.localPosition.z);
            fx.eulerAngles = new Vector3(180f, 90f, 45f);
        }
        else
        {
            _spriteRenderer.flipY = true;
            fx.localPosition = new Vector3(1f, fx.localPosition.y, fx.localPosition.z);
            fx.eulerAngles = new Vector3(180f, -90f, -45f);
        }
    }

    private void RestrictRotation()
    {
        Quaternion newTransformRotation = transform.rotation;
        transform.rotation = new Quaternion(
            newTransformRotation.x,
            0f,
            newTransformRotation.z,
            newTransformRotation.w
        );
    }
}