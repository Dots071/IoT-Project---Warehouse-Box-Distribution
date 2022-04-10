using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public int cbIndex;

    public bool isBeltMoving = true;
    public Transform midPoint;
    public Transform endPoint;
    public float speed;

    public List<Box> onBelt;

    [SerializeField] AudioSource audioSource;


    private void OnEnable()
    {
        CbManager.OnConveyorBeltSwitch += HandleCBSwitch;
    }

    private void OnDisable()
    {
        CbManager.OnConveyorBeltSwitch -= HandleCBSwitch;
    }



    void Start()
    {
        onBelt = new List<Box>();  
    }



    void FixedUpdate()
    {
        if (isBeltMoving)
        {
            for (int i = 0; i < onBelt.Count; i++)
            {
                onBelt[i].MoveObjectOnBelt();
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {

            Box temp;
            if (temp = other.GetComponent<Box>())
            {
                if (onBelt.Count == 0)
                    if (audioSource != null)
                        audioSource.Play();

            if (cbIndex == 1 || cbIndex == 0)
                {
                    if (temp.currentCBIndex == -1)
                    {
                    //Debug.Log("Spawning box at table from CB " + cbIndex);
                        
                        BoxSpawner.Instance.SpawnBoxAtTable();
                    }
                }
             

                temp.currentSpeed = speed;
                temp.currentDestPos = midPoint;
                temp.nextDestPos = endPoint;
                temp.currentCBIndex = cbIndex;
                temp.isBoxMoving = isBeltMoving;
            temp.boxIsBlocked = false;
                onBelt.Add(temp);
            }
    }

    void OnTriggerExit(Collider other)
    {
            Box temp;
            if (temp = other.GetComponent<Box>())
            {
                if (onBelt.Contains(temp))
                {
                    onBelt.Remove(temp);
                    CbManager.Instance.BoxExitedCB(cbIndex);
                }

            }

    }



    /*    protected virtual void MoveObjectOnBelt(int index)
        {

                onBelt[index].transform.position = Vector3.MoveTowards(onBelt[index].transform.position, endPoint.position, speed * Time.deltaTime);

        }*/

    protected virtual void HandleCBSwitch(int index, bool cbOn)
    {
        if (cbIndex == index)
        {
            isBeltMoving = cbOn;
            if (audioSource != null && onBelt.Count != 0)
                PlayCBSound(cbOn);
        }       
    }

    void PlayCBSound(bool play)
    {
        if (play && onBelt.Count != 0)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}

