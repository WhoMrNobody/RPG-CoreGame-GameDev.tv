using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement{
public class Mover : MonoBehaviour, IAction
{
    [SerializeField] Transform target;
    Health health;
    NavMeshAgent navMeshAgent;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
    }

    private void Update() {

        navMeshAgent.enabled = ! health.IsDead();
        UpdateAnimator();
    }

    public void MoveTo(Vector3 destination){

       navMeshAgent.destination = destination;
       navMeshAgent.isStopped = false;
    }

    public void StartMoveAction(Vector3 destination){

        GetComponent<ActionSchedular>().StartAction(this);
        MoveTo(destination);
    }

    public void Cancel(){

        navMeshAgent.isStopped = true;

    }

    void UpdateAnimator(){

        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
}
}
