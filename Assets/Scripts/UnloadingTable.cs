using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnloadingTable : MonoBehaviour
{
    public bool isBoxOnTable;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Box"))
            isBoxOnTable = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Box"))
            isBoxOnTable = false;
    }
}
