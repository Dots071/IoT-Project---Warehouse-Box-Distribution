using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// This script allows the object it is attached to, to be dragged by the mouse when clicked on.
[RequireComponent(typeof(Rigidbody))]
public class MouseDrag : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float moveForce = 50;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    Vector3 mouseOffset;
    float mouseZCoord;  

    void OnMouseDown()
    {
        mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouseOffset = gameObject.transform.position - GetMouseWorldPos();

        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        //rb.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    private void OnMouseUp()
    {
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mouseZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        Vector3 destinationPos = GetMouseWorldPos() + mouseOffset;

        rb.MovePosition(destinationPos);
        //transform.position = destinationPos;
        //transform.position = Vector3.MoveTowards(transform.position, destinationPos, moveForce * Time.deltaTime);
        //rb.velocity = (destinationPos - transform.position) * moveForce * Time.deltaTime;
        // transform.Translate(destinationPos * Time.deltaTime * moveForce);
    }
}
