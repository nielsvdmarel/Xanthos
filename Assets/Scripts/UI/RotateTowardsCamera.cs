using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsCamera : MonoBehaviour {

    [SerializeField]
    private Transform target;
    [SerializeField]
    private bool RotateTowardsObject;
    [SerializeField]
    private Transform planet;
	void Update ()
    {
        if (RotateTowardsObject)
        {
            //Vector3 targetpos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            //transform.LookAt(targetpos);
            //transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
            //now working with faux gravity system
        }
	}
}
