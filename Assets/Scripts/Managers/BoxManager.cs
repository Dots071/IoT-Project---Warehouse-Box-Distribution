/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class BoxManager : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    int currentCbIndex; // The current conveyor belt the box is on.

    ConveyorBelt cb;

    private void OnEnable()
    {
        ConveyorBelt.OnConveyorBeltSwitch += HandleOnCbSwitch;
    }

    private void OnDisable()
    {
        ConveyorBelt.OnConveyorBeltSwitch -= HandleOnCbSwitch;
    }



    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = false;
    }

       private void Update()
       {
           if (navMeshAgent.isActiveAndEnabled && cb != null)
           {
               if (navMeshAgent.remainingDistance < 0.3f)
               {
                cb.CBSwitch(false);
               }
           }
       }*/

    
/*
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ConveyorBelt"))
        {
            cb = other.GetComponent<ConveyorBelt>();
            currentCbIndex = cb.cbIndex;

            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(cb.endPoint.position);
        }
    }*/

    // Handles the box in the event its conveyor belt has turned off/on.
/*    private void HandleOnCbSwitch(int cbIndex, bool cbOn)
    {
        if (currentCbIndex == cbIndex)
        {
            navMeshAgent.isStopped = !cbOn;
            navMeshAgent.enabled = cbOn;
        }
    }*/
//}
