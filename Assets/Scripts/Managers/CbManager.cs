using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CbManager : Singleton<CbManager>
{
    public delegate void OnCBSwitch(int cbIndex, bool cbOn);
    public static event OnCBSwitch OnConveyorBeltSwitch;



    [SerializeField] ConveyorBelt[] conveyorBelts;
    [SerializeField] CurvedCB cb2Pusher;
    [SerializeField] PuhserExit cb6Pusher;
    Queue<int> cb4Queue;

    bool cb4Loaded = false;

    private void Start()
    {
        cb4Queue = new Queue<int>();
    }

    private void Update()
    {
        //Check to see if there is a box waiting to go on CB4 and CB4 is empty.
        if (cb4Queue.Count != 0 && !cb4Loaded)
        {
            //Debug.Log("Queue count is: " + cb4Queue.Count);
            int nextBoxTo4 = cb4Queue.Dequeue();

            switch (nextBoxTo4)
            {
                case 2:
                    cb2Pusher.currentPusherState = CurvedCB.PusherState.PUSHING;
                    cb4Loaded = true;
                    break;
                case 3:
                    StartCoroutine(ActivateCbAfter2Seconds(3));                   
                    cb4Loaded = true;
                    break;
            }
        }
    }

    public void BoxAtEndCB(int cbIndex)
    {
      // Debug.Log("BoxAtEndCB: " + cbIndex);
        switch (cbIndex)
        {
            case 2:
                cb4Queue.Enqueue(cbIndex);
                OnConveyorBeltSwitch(cbIndex, false);
                break;

            case 3:
                if(cb4Loaded)
                {
                    cb4Queue.Enqueue(cbIndex);
                    OnConveyorBeltSwitch(cbIndex, false);
                }
                else
                {
                    cb4Loaded = true;
                }

                break;

            case 4:
                if (conveyorBelts[5].onBelt.Count == 0)
                {
                     //Debug.Log("cb 5 is open");
                    OnConveyorBeltSwitch(4, true);
                }
                else
                {
                    cb4Loaded = true;
                    OnConveyorBeltSwitch(cbIndex, false);
                }
                break;

            case 5:
                OnConveyorBeltSwitch(cbIndex, false);
                StartCoroutine(ActivateCbAfter5Seconds(5));
                break;


            default:
                break;
        }


    }

    IEnumerator ActivateCbAfter2Seconds(int index)
    {
        yield return new WaitForSeconds(2);
        OnConveyorBeltSwitch(index, true);
    }

    IEnumerator ActivateCbAfter5Seconds(int index)
    {
        yield return new WaitForSeconds(5);
        OnConveyorBeltSwitch(index, true);
    }


    public void BoxExitedCB(int index)
    {
        //Debug.Log("Box exited CB :" + index);
        switch (index)
        {
            case 2:
                OnConveyorBeltSwitch(index, true);
                break;
            case 4:
                cb4Loaded = false;
                break;
            case 5:
                //Debug.Log("Box exited CB :" + index);
                if (cb4Loaded)
                {
                    OnConveyorBeltSwitch(4, true);
                }
                cb6Pusher.currentPusherState = PuhserExit.PusherState.PUSHING;
                break;

            default:
                break;
        }

    }
}
