using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CB6 : MonoBehaviour
{
    PuhserExit pusher;
    AnimatedCB cb6;
    private void Start()
    {
        pusher = GetComponentInChildren<PuhserExit>();
        cb6 = GetComponentInParent<AnimatedCB>();
        if (pusher == null || cb6 == null)
            Debug.LogError("Couldn't find pusher/AnimatedCB componenet in CB6");
    }
    private void OnTriggerEnter(Collider other)
    {
        SoundManager.Instance.PlayPrintBarcodeAudio();
        pusher.currentPusherState = PuhserExit.PusherState.PULLING;
        Box tempBox;
        if (tempBox = other.GetComponent<Box>())
            cb6.AnimateNewBoxOnBelt(tempBox);  
    }
}
