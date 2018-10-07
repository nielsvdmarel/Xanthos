using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private int CurrentCameraSetUp;
    [SerializeField]
    public enum CameraSetUp {CloseCameraFollow, LongDistanceCameraFollow, CinematicCamera};
    [SerializeField]
    private float DistanceFromObject;
    [SerializeField]
    private bool FadeInFrontObject = false;
    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public void ChangeCameraSetUp()
    {

    }
}
