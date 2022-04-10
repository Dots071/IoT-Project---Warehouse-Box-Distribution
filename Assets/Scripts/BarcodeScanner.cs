using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarcodeScanner : MonoBehaviour
{
    [Tooltip("Child object to check if there is an object to scan")]
    public Transform objectCheck;

    [Tooltip("LayerMask to filter which layer to check for scanning")]
    public LayerMask objectLayer;

    [Tooltip("Light that will turn on/off when scanner is active")]
    public GameObject scannerLight;

    bool isScanning = false; 

    // Update is called once per frame
    void Update()
    {
        if(Physics.Linecast(transform.position, objectCheck.position, objectLayer))
        {
            if(!isScanning)
            {
                ActivateScanner(true);
                SoundManager.Instance.PlayReadBarcodeAudio();
            }   
        } else
        {
            if (isScanning)
                ActivateScanner(false);
        }
    }

    void ActivateScanner(bool activate)
    {
        isScanning = activate;
        scannerLight.SetActive(activate);
    }
}
