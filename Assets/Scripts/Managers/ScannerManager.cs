using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScannerManager : MonoBehaviour
{
    [SerializeField] Animator scannerAnim;
    [SerializeField] GameObject scannerGameObject;
    [SerializeField] Light scannerPointLight;
    [SerializeField] int onCBIndex;
    public bool scannerBusy = false;


    private void OnEnable()
    {
        CbManager.OnConveyorBeltSwitch += HandleCBSwitch;
    }

    private void OnDisable()
    {
        CbManager.OnConveyorBeltSwitch -= HandleCBSwitch;
    }

    private void HandleCBSwitch(int cbIndex, bool cbOn)
    {
        //Debug.Log("Index: " + cbIndex + " , on: " + cbOn);
        if (cbIndex == onCBIndex)
        {
            scannerAnim.SetBool("DoorsAreClosed", !cbOn);
            if(cbOn)
            {
                DeactivateScanner();
            } else
            {
                StartCoroutine(ActivateScanner());
            }
        }
    }

    IEnumerator ActivateScanner()
    {
        SoundManager.Instance.PlayCloseDoorsAudio();
        yield return new WaitForSeconds(1);
        FadeChildrenInGameObject(scannerGameObject, 0.1f);
        scannerPointLight.DOIntensity(5f, 2);
        SoundManager.Instance.PlayScanningAudio();
    }

    void DeactivateScanner()
    {
        FadeChildrenInGameObject(scannerGameObject, 1f);
        scannerPointLight.DOIntensity(0.1f, 2);
        SoundManager.Instance.PlayOpenDoorsAudio();
    }

    public void FadeChildrenInGameObject(GameObject objectToFade, float alpha)
    {
        MeshRenderer[] children = objectToFade.GetComponentsInChildren<MeshRenderer>();

        foreach(MeshRenderer child in children)
        {
            child.material.DOFade(alpha, 2);
        }
    }
}
