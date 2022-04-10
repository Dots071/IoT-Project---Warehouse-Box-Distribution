using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuhserExit : MonoBehaviour
{
    public enum PusherState
    {
        IDLE,
        PUSHING,
        PULLING
    }

    [SerializeField] float pusherSpeed;
    //[SerializeField] float maxDistance;

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
                rb.velocity = Vector3.left * pusherSpeed * Time.deltaTime;
               // Debug.Log("Velocity: " + rb.velocity);  
                break;

            case PusherState.PULLING:
                if (transform.localPosition.x < initialPusherPos.x)
                {
                    rb.velocity = Vector3.right * pusherSpeed * Time.deltaTime;
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
