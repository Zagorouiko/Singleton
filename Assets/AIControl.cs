﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour {

	
	UnityEngine.AI.NavMeshAgent agent;
    Animator anim;
    Vector3 lastGoal;


	// Use this for initialization
	void Start () {
		
		agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        anim.SetBool("isWalking", true);
        PickGoalLocation();
	}

    void PickGoalLocation()
    {
        lastGoal = agent.destination;
        GameObject goalPosition = GameEnvironment.Singleton.GetRandomGoal();
        agent.SetDestination(goalPosition.transform.position);
    }

	
	// Update is called once per frame
	void Update () {
        if (agent.remainingDistance < 1) //At the goal
        {
            PickGoalLocation();
        }

        foreach (GameObject gameObject in GameEnvironment.Singleton.GetObstacles)
        {
            float distance = Vector3.Distance(gameObject.transform.position, transform.position);
            if (distance < 5 && Random.Range(0, 100) < 5) {
                agent.SetDestination(lastGoal);
            } else if (distance < 1)
            {
                GameEnvironment.Singleton.RemoveObstacles(gameObject);
                break;
            }
        }
	}
}
