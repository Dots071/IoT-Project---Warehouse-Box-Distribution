using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartConveyorBelt : ConveyorBelt
{/*
    [SerializeField] bool boxOnEndOfCB = false;
    [SerializeField] float destCheckOffset = 0.3f;

    [SerializeField] bool pushForwardAtEnd = false;
    [SerializeField] float pushBoxTime = 1f;
*/


/*    void Update()
    {
        if (isBeltMoving && !boxOnEndOfCB)
        {
            BoxReachedDestCheck();

        }
    }*/

/*    private void  BoxReachedDestCheck()
    {
            foreach (Box box in onBelt)
            {
                if (Vector3.Distance(box.transform.position, box.currentDestPos.position) < destCheckOffset)  // WHY NOT ON BOX SCRIPT?? 
                {
              //  Debug.Log("Box reached offset at CB: " + cbIndex);
                CbManager.Instance.BoxAtEndCB(cbIndex);
                boxOnEndOfCB = true;
                }
            }
    }*/
/*
    protected override void HandleCBSwitch(int index, bool cbOn)
    {
       base.HandleCBSwitch(index, cbOn);
        if (index == cbIndex && cbOn)
        {
            if (pushForwardAtEnd)
            {
                StartCoroutine("PushBoxToNextCB");
            }
            else
            {
                boxOnEndOfCB = false;
            }
        }
    }
    
    IEnumerator PushBoxToNextCB()
    {
        yield return new WaitForSeconds(pushBoxTime);
        boxOnEndOfCB = false;
    }
*/

}
