using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    float rotationSpeed = 400;

    private void OnMouseDrag()
    {
        float rotateX = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
        float rotateY = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad;

        transform.Rotate(Vector3.up, -rotateX);
        //transform.Rotate(Vector3.right, -rotateY);

    }
}
