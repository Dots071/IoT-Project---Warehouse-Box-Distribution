using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class CurvedCB : MonoBehaviour
{
    public enum PusherState
    {
        IDLE,
        PUSHING,
        PULLING
    }

    [SerializeField] float pusherSpeed;
    [SerializeField] float maxZPos;

    [SerializeField] int onCbIndex;

    public Vector3 initialPusherPos;

    public PusherState currentPusherState;

    Rigidbody rb;

    private void Start()
    {
        currentPusherState = PusherState.IDLE;
        rb = gameObject.GetComponent<Rigidbody>();
        initialPusherPos = transform.localPosition; ;
    }

    private void Update()
    {
        if (currentPusherState != PusherState.IDLE)
        {
            MovePusher();
        }
    }


    void MovePusher()
    {
            switch (currentPusherState)
            {
                case PusherState.PUSHING:
                    if (transform.localPosition.z > maxZPos)
                    {
                        rb.velocity = Vector3.right * pusherSpeed * Time.deltaTime;
                    }
                    else
                    {
                        currentPusherState = PusherState.PULLING;
                    }
                    break;

                case PusherState.PULLING:
                    if (transform.localPosition.z < initialPusherPos.z)
                    {
                        rb.velocity = Vector3.left * pusherSpeed * Time.deltaTime;
                    }
                    else
                    {
                        //Puser has finished his operation.
                        currentPusherState = PusherState.IDLE;
                        rb.velocity = Vector3.zero;
                        CbManager.Instance.BoxExitedCB(onCbIndex);
                    }
                    break;

                default:
                    break;
            }
    }
}
