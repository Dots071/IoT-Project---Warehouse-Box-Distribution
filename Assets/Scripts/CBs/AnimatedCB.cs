using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedCB : MonoBehaviour
{

    [SerializeField] int cbIndex;
    [SerializeField] bool isBeltMoving = true;
    [SerializeField] List<Box> onAnimatedBelt;
    [SerializeField] AudioSource audioSource;



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
        onAnimatedBelt = new List<Box>();
    }

    //When entered CB, add Box to list and animate.
    private void OnTriggerEnter(Collider other)
    {
        Box tempBox;
        if (tempBox = other.GetComponent<Box>())
        {
            if (onAnimatedBelt.Count == 0)
                if (audioSource != null)
                    audioSource.Play();

            AnimateNewBoxOnBelt(tempBox);
        }
    }


    public void AnimateNewBoxOnBelt(Box box)
    {
        if (!onAnimatedBelt.Contains(box))
        {
            onAnimatedBelt.Add(box);
            box.AnimateByCBIndex(cbIndex);
            box.boxAnim.enabled = true;

            if (!isBeltMoving)
                box.boxAnim.speed = 0;
        }
    }

    //Pause/activate boxes' animations according to isBeltMoving


    private void HandleCBSwitch(int index, bool cbOn)
    {
        if (this.cbIndex == index)
        {
            isBeltMoving = cbOn;

            if (audioSource != null)
                PlayCBSound(cbOn);

            for (int i = onAnimatedBelt.Count - 1; i >= 0; i--)
            {
                if (onAnimatedBelt[i].atEndOfCB)
                {
                    onAnimatedBelt[i].atEndOfCB = false;
                    onAnimatedBelt.Remove(onAnimatedBelt[i]);
                }
                else
                {
                    onAnimatedBelt[i].boxAnim.speed = cbOn ? 1 : 0;   //Pauses or activates animator depending on the CB switch.
                   // Debug.Log("Box " + i + "on CB " + cbIndex + " animation speed is set to: " + onAnimatedBelt[i].boxAnim.speed);
                }
            }
        }
    }

    void PlayCBSound(bool play)
    {
        if (play && onAnimatedBelt.Count != 0)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
