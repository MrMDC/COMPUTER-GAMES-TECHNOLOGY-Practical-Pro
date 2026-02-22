using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public GameObject[] waypoints;
    Graph graph = new Graph();
    int currentWP = 0;
    GameObject currentNode;

    int speed = 8;
    int rotationSpeed = 5;
    float accuracy = 1.0f;

    
    //use tgis for initialization
    // Start is called before the first frame update
    void Start()
    {
        if(waypoints.Length > 0){
            //add all the waypoints to the Graph
            for (int i =0 ; i < waypoints.Length; i++){
                graph.AddNode(waypoints[i],true,true);
            }

            //create edges between the waypoints
            graph.AddEdge(waypoints[0], waypoints[1]);
            graph.AddEdge(waypoints[1], waypoints[2]);
            graph.AddEdge(waypoints[2], waypoints[3]);
            graph.AddEdge(waypoints[3], waypoints[4]);
            graph.AddEdge(waypoints[4], waypoints[5]);
            graph.AddEdge(waypoints[5], waypoints[6]);
            graph.AddEdge(waypoints[6], waypoints[7]);
            graph.AddEdge(waypoints[7], waypoints[8]);
            graph.AddEdge(waypoints[8], waypoints[0]);
        }
        currentNode = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        graph.debugDraw();

        //if there is no path or at the end don't do anything

        if(graph.getPathLength() == 0 || currentWP == graph.getPathLength()){
            this.GetComponent<Animation>().Play("idle");
            return;
        }

        //the node we are closest to at this moment
        currentNode = graph.getPathPoint(currentWP);

        //if we close enough to the current waypoint move to next
        if(Vector3.Distance(graph.getPathPoint(currentWP).transform.position,transform.position) < accuracy){
            currentWP++;
        }
        //if we are not at the end of the path
        if(currentWP < graph.getPathLength()){
            //keep on moving
            Vector3 direction = graph.getPathPoint(currentWP).transform.position 
            - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(direction),rotationSpeed * Time.deltaTime);
            transform.Translate(0,0, Time.deltaTime * speed);
        }
    }

    void OnGUI(){
        GUI.Box(new Rect (10,10,100,90),"Guard's Orders");
        if(GUI.Button(new Rect(20,65,80,20),"Patrol"))
        {

            graph.AStar(waypoints[0],waypoints[8]);

            this.GetComponent<Animation>().Play("run");
            this.GetComponent<Animation>()["run"].wrapMode = WrapMode.Loop;
        }
    }
}
