
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{

    public Transform target; //the enemy
    public Transform head;
    float seeRange = 12.0f; //maximum attack distance
                            //will attack if closer than
                            //this to the enemy
    float shootRange = 8.0f;
    float keepDistance = 2.0f; //closest distance to get 
                               //to enemy
    float rotationSpeed = 4.0f;
    float speed = 0.1f;
    float sightAngle = 60f;
    ParticleSystem magicParticles;
    Animator anim;
    GameObject magic;

    bool CanSeeTarget()
    {
        if (Vector3.Distance(head.position, target.position) > seeRange)
            return false;
        return true;
    }

    bool CanShoot()
    {
        Vector3 directionToTarget = target.position - head.position;

        float angle = Vector3.Angle(directionToTarget, head.forward);
        if (Vector3.Distance(head.position, target.position) > seeRange || angle > sightAngle)
            return false;
        return true;
    }

    void Pursue()
    {
        Vector3 position = target.position;
        Vector3 direction = position - head.position;
        direction.y = 0;
        // Rotate towards the target
        head.rotation = Quaternion.Slerp(head.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        // Move the character
        if (direction.magnitude > keepDistance)
        {
            direction = direction.normalized * speed;
            transform.position += direction;
        }
    }
    void Shoot()
    {
        Vector3 position = target.position;
        Vector3 direction = position - head.position;
        direction.y = 0;
        // Rotate towards the target
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    void TurnOnMagic()
    {
        magicParticles.Play();
    }
    void TurnOffMagic()
    {
        magicParticles.Stop();
    }

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        magic = GameObject.Find("Magic");
        magicParticles = magic.GetComponent<ParticleSystem>();
        magicParticles.Stop();
    }
   
    void Update()
    {
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Standing_Walk_Forward")
            speed = 0.1f;
        else
            speed = 0.0f;
        if (CanSeeTarget())
        {
            if (CanShoot())
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isAttacking", true);
                Shoot();
            }
            else
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isAttacking", false);
                Pursue();
            }
        }
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
            //stand around
        }
    }
    
}
