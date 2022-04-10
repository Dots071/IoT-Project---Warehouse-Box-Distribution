using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    int boxType;
    int[] boxTypeArray = new int[20] { 1, 1, 3, 3, 2, 4, 4, 4, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6 }; // should be in a Scriptable Object 

    public bool spawnedAtTable = false;

    public bool boxIsBlocked = false;

    public int currentCBIndex;
    public float currentSpeed = 1;
    public Transform nextDestPos;
    public Transform currentDestPos;

    public bool atEndOfCB = false;

    public Animator boxAnim;
    public bool isBoxMoving = true;
    bool isCB2on = true;
    bool waitingForCB2 = false;

    private void OnEnable()
    {
        CbManager.OnConveyorBeltSwitch += HandleCBSwitch;
    }

    private void OnDisable()
    {
        CbManager.OnConveyorBeltSwitch -= HandleCBSwitch;
    }

    private void Start()
    {
        boxAnim = GetComponent<Animator>();
        if (boxAnim == null)
            Debug.LogError("Couldn't find box animator");
        boxAnim.enabled = false;

        boxType = boxTypeArray[Random.Range(0, boxTypeArray.Length)];
    }

/*
    private void FixedUpdate()
    {
        if (isBoxMoving)
            MoveObjectOnBelt();
    }*/

    private void HandleCBSwitch(int cbIndex, bool cbOn)
    {
/*        if (cbIndex == currentCBIndex)
            isBoxMoving = cbOn;*/

        if (cbIndex == 2)
        {
            isCB2on = cbOn;
            if (waitingForCB2)
                waitingForCB2 = !cbOn;
        }
    }


    public void MoveObjectOnBelt()
    {
        if(!boxAnim.enabled && !boxIsBlocked && currentDestPos != null && !waitingForCB2)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentDestPos.position, currentSpeed * Time.deltaTime);
            BoxReachedDestCheck();
        }
    }

    private void BoxReachedDestCheck()
    {
            if (Vector3.Distance(transform.position, currentDestPos.position) <= 0.1f)
            {
               // Debug.Log("Box reached end at CB: " + currentCBIndex);
                CbManager.Instance.BoxAtEndCB(currentCBIndex);
                if (currentCBIndex >= 7)
                {
                    currentDestPos = null;
                } else if (currentCBIndex == 0 && !isCB2on)
                {
                waitingForCB2 = true;
                currentDestPos = nextDestPos;
                } else 
                {
                    currentDestPos = nextDestPos;
                }
            }
    }

    void ReachedEndOfCB(int onCBIndex)
    {
        TurnOffAnimator();

        atEndOfCB = true;
        CbManager.Instance.BoxAtEndCB(onCBIndex);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("UnloadingTable"))
        {
            currentCBIndex = -1;
        } else if (collision.gameObject.CompareTag("Box"))
        {
                if (FaceForwardObjectCheck(collision.transform.position))
                    boxIsBlocked = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Box"))
        {
            if (FaceForwardObjectCheck(collision.transform.position))
                boxIsBlocked = false;
        }
    }

    private bool FaceForwardObjectCheck(Vector3 targetPos)
    {
        if (currentDestPos != null)
        {
            Vector3 toTarget = (targetPos - transform.position).normalized;
            Vector3 moveDirection = (currentDestPos.position - transform.position).normalized;

            float alpha = Vector3.Dot(toTarget, moveDirection);
            // Debug.Log("Alpha on CB " + currentCBIndex + ": " + alpha);

            if (alpha > 0.80f)
            {
               // Debug.Log("Box is in front on CB: " + currentCBIndex);
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public void AnimateByCBIndex(int cbIndex)
    {
        currentCBIndex = cbIndex;
        switch(cbIndex)
        {
            case 2:
                boxAnim.PlayInFixedTime("BoxOnCB2");
                break;
            case 6:
                PlayLastAnimation();
                break;
        }
    }

    public void PlayLastAnimation()
    {
        switch(boxType)
        {
            case 1:
                boxAnim.PlayInFixedTime("BoxToCB7");
                break;
            case 2:
                boxAnim.PlayInFixedTime("BoxToCB8");
                break;
            case 3:
                boxAnim.PlayInFixedTime("BoxToCB9");
                break;
            case 4:
                boxAnim.PlayInFixedTime("BoxToCB10");
                break;
            case 5:
                boxAnim.PlayInFixedTime("BoxToCB12");
                break;
            case 6:
                boxAnim.PlayInFixedTime("BoxToCB13");
                break;
            default:
                break;
        }
    }

    void TurnOffAnimator()
    {
        boxAnim.enabled = false;
    }
}
