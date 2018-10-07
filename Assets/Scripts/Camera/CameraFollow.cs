using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
     [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offsetPosition;
    private Vector3 offsetRotation;
    private bool lookAt = true;
    [SerializeField]
    private float smoothTime = 0.3f;

    public void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Missing target ref !", this);

            return;
        }

        //transform.position = target.TransformPoint(offsetPosition);
        //transform.position = Vector3.MoveTowards(transform.position, target.position, smoothTime* Time.deltaTime);
        transform.position = Vector3.Slerp(transform.position, target.position, smoothTime * Time.deltaTime);
        transform.rotation = target.rotation;
    }
}