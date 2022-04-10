using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineManager : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera[] virtualCams;
    [SerializeField] int unloadingCamIndex = 0;
    [SerializeField] List<Vector3> camsInitPos;
    [SerializeField] List<Quaternion> camsInitRotation;


    private void Start()
    {
        camsInitPos = new List<Vector3>();
        camsInitRotation = new List<Quaternion>();
        for (int i = 0; i < virtualCams?.Length; i++)
        {
            camsInitPos.Add(virtualCams[i].transform.position);
            camsInitRotation.Add(virtualCams[i].transform.rotation);
        }
    }

    public void GoToCam(int camIndex)
    {
        // BoxSpawner.Instance.AutoSpawnOn(camIndex == unloadingCamIndex ? false : true);

        for (int i = 0; i < virtualCams?.Length; i++)
        {
            if(camIndex - 1 == i)
            {
                virtualCams[i].transform.position = camsInitPos[i];
                virtualCams[i].transform.rotation = camsInitRotation[i];
                virtualCams[i].Priority = 1;
            } else
            {
                virtualCams[i].Priority = 0;
            }
             
        }
    }

    private void OnDestroy()
    {
        camsInitPos.Clear();
        camsInitRotation.Clear();
    }
}
