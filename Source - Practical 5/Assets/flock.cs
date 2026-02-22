using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flock : MonoBehaviour
{
    float speed = 0.001f;
    float rotationSpeed = 5.0f;
    Vector3 averagePosition;
    float neighbourDistance = 10.0f;
    float socialDistancing = 10.0f;

    GameObject[] gos;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(0.1f, 1f);
        gos = GameObject.FindGameObjectsWithTag("Seagull");
    }

    // Update is called once per frame
    void Update()
    {
        if (Random. Range(0, 5) < 1){
             ApplyRules();
        }
           
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    void ApplyRules()
    {
        

        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        Vector3 wind = new Vector3(1,0,1);
        Vector3 goalPos = GlobalFlock.goalPos;

        float gSpeed = 0;
        float dist = 0;
        int groupSize = 0;
        foreach (GameObject go in gos){
            if (go != this.gameObject){
            dist = Vector3.Distance(go.transform.position, this.transform.position);
            }

            if (dist <= neighbourDistance){
            vcentre += go.transform.position;

            groupSize++;
                if(dist < + socialDistancing){
                    vavoid = vavoid + (this.transform.position - go.transform.position);
                }
            gSpeed = gSpeed + go.GetComponent<flock>().speed;
            }

        }
        if (groupSize != 0){
        vcentre = vcentre / groupSize + wind + (goalPos - this.transform.position);
        speed = gSpeed / groupSize;

        Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion. LookRotation(direction),rotationSpeed * Time.deltaTime);

        }
    }
}