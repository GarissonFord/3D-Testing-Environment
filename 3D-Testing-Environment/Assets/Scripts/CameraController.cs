using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Thanks GamesPlusJames

    public Transform target;

    public Vector3 offset;

    public bool useOffsetValues;

    public float rotateSpeed;

    public Transform pivot;

    public float maxViewAngle;
    public float minViewAngle;

    public bool invertY;

    void Start()
    {
        if (!useOffsetValues)
            offset = target.position - transform.position;

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;
    }

    void LateUpdate()
    {
        //Get the X position of the mouse and rotate the target
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0.0f, horizontal, 0.0f);

        //Get the Y position of the mouse
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;

        if (invertY)
            pivot.Rotate(-vertical, 0.0f, 0.0f);
        else
            pivot.Rotate(vertical, 0.0f, 0.0f);

        //Limit the up/down camera rotation
        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180.0f)
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0.0f, 0.0f);
        else if (pivot.rotation.eulerAngles.x > 180.0f && pivot.rotation.eulerAngles.x < 360 + minViewAngle)
            pivot.rotation = Quaternion.Euler(360.0f + minViewAngle, 0.0f, 0.0f);

        //Move the camera based on the current rotation of the target and the original offset
        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0.0f);
        transform.position = target.position - (rotation * offset);

        //transform.position = target.position - offset;  

        if (transform.position.y < target.position.y)
            transform.position = new Vector3(transform.position.x, target.position.y - 0.5f, transform.position.z);

        transform.LookAt(target);
    }
}
