using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Tooltip("The component these sound effects will play from.")]
    [SerializeField] AudioSource scannerAudioSource;

    [Tooltip("Sound effect/clip.")]
    [SerializeField] AudioClip openScannerDoors, closeScannerDoors, printBarcode, readBarcode, scanning;


    public void PlayOpenDoorsAudio()
    {
        scannerAudioSource.PlayOneShot(openScannerDoors);
    }

    public void PlayCloseDoorsAudio()
    {
        scannerAudioSource.PlayOneShot(closeScannerDoors);
    }

    public void PlayPrintBarcodeAudio()
    {
        scannerAudioSource.PlayOneShot(printBarcode);
    }

    public void PlayReadBarcodeAudio()
    {
        scannerAudioSource.PlayOneShot(readBarcode);
    }

    public void PlayScanningAudio()
    {
        scannerAudioSource.PlayOneShot(scanning);
    }
}
