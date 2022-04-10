using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float zoomSpeed = 5;
    [SerializeField] float rotationSpeed = 5;
    [SerializeField] float mouseX_Sensitivity = 5;
    [SerializeField] float mouseY_Sensitivity = 5;
    float maxClampAngle = 45;
    float minClampAngle = -45;


    [Header("Camera Move Limit")]
    float cameraMinX = -28;
    float cameraMaxX = 81;
    float cameraMinY = -3.5f;
    float cameraMaxY = 6;
    float cameraMinZ = -15;
    float cameraMaxZ = 21;


    float cameraXRotation = 0;
    float cameraYRotation = 0;

    CinemachineVirtualCamera cam;

    private void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();

    }

    void LateUpdate()
    {
        if(cam.Priority == 1)
        {
            MoveCamera();
            ZoomCamera();
            if (Input.GetMouseButton(1))
            {
               // LimitCamera();
                RotateCamera();
            }
        }
    }

    void MoveCamera()
    {

            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");

            transform.Translate(Vector3.up * verticalInput * moveSpeed * Time.deltaTime);
            transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);

    }

    void ZoomCamera()
    {
        float mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (mouseScrollInput != 0)
            transform.Translate(Vector3.forward * mouseScrollInput * zoomSpeed * Time.deltaTime);
    }

    void RotateCamera()
    {
            cameraXRotation += Input.GetAxis("Mouse Y") * mouseY_Sensitivity * -1;
            cameraYRotation += Input.GetAxis("Mouse X") * mouseX_Sensitivity;

            cameraXRotation = Mathf.Clamp(cameraXRotation, minClampAngle, maxClampAngle);
            cameraYRotation = Mathf.Repeat(cameraYRotation, 360);

            Vector3 rotatingAngle = new Vector3(cameraXRotation, cameraYRotation, 0);

            Quaternion rotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(rotatingAngle), rotationSpeed * Time.deltaTime);

            transform.localRotation = rotation;
 
    }

    void LimitCamera()
    {
        Vector3 cameraPos = transform.position;

        if (cameraPos.x > cameraMaxX)
        {
            cameraPos.x = cameraMaxX;
        }
        else if (cameraPos.x < cameraMinX)
        {
            cameraPos.x = cameraMinX;
        }

        if (cameraPos.y > cameraMaxY)
        {
            cameraPos.y = cameraMaxY;
        }
        else if (cameraPos.y < cameraMinY)
        {
            cameraPos.y = cameraMinY;
        }

        if (cameraPos.z > cameraMaxZ)
        {
            cameraPos.z = cameraMaxZ;
        }
        else if (cameraPos.z < cameraMinZ)
        {
            cameraPos.z = cameraMinZ;
        }

        transform.position = cameraPos;

    }



}
