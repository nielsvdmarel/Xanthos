using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSphereCordScript : MonoBehaviour {
    [Header("RayCast Check tests")]
    public GameObject PlayerMiddlePos;
    public GameObject PlayerCheckX;
    public GameObject PlayerCheckY;
    public GameObject MiddlePointX;
    public GameObject MiddlePointY;
    public GameObject xLeft;
    public GameObject xRight;
    public GameObject yUp;
    public GameObject yDown;
    public float PlanetXLeft, PlanetXRight, PlanetYUp, PlanetYDown;
    public Vector3 Xdifference;
    public Vector3 YDifference;
    public GameObject currentPlanet;
    public GameObject Main4Points;
    public GameObject LeftUp, LeftDown, RightUp, RightDown;

    private RaycastHit HitYUp;
    private RaycastHit HitYDown;
    private RaycastHit hitXLeft;
    private RaycastHit hitXRight;
    private RaycastHit HitYUpPlanet, HitYDownPlanet, HitXLeftPlanet, HitXRightPlanet;

    float minDistance = .20f;

    [SerializeField]
    private float XLeft;
    [SerializeField]
    private Vector3 XleftHitPos;
    [SerializeField]
    private float XRight;
    [SerializeField]
    private Vector3 XRightHitPos;
    [SerializeField]
    private float YUp;
    [SerializeField]
    private Vector3 YUpHitPos;
    [SerializeField]
    private float YDown;
    [SerializeField]
    private Vector3 YDownHitPos;
    void Start () {
        SetObjectOnCurrentPosX();
        SetObjectOnCurrentPosY();
        xLeft.transform.localPosition = new Vector3(xLeft.transform.localPosition.x, xLeft.transform.localPosition.y + .80f, xLeft.transform.localPosition.z);
        xRight.transform.localPosition = new Vector3(xRight.transform.localPosition.x, xRight.transform.localPosition.y + .80f, xRight.transform.localPosition.z);
        yDown.transform.localPosition = new Vector3(yDown.transform.localPosition.x, yDown.transform.localPosition.y + .80f, yDown.transform.localPosition.z);
        yUp.transform.localPosition = new Vector3(yUp.transform.localPosition.x, yUp.transform.localPosition.y + .80f, yUp.transform.localPosition.z);
    }
	
	void Update () {
        CheckXDistance();
        CheckYDistance();
        RayCastCheck();
        FixCheckAxises();
        PlayerCheckX.transform.localRotation = transform.rotation;
        PlayerCheckY.transform.localRotation = transform.localRotation;
    }

    public void SetObjectOnCurrentPosX() {
        Debug.Log("resetX Side");
        PlayerCheckX.transform.localRotation = transform.rotation;
        PlayerCheckY.transform.localRotation = transform.localRotation;
        PlayerCheckX.transform.localPosition = this.transform.position;
    }

    public void SetObjectOnCurrentPosY() {
        Debug.Log("Reset y side");
        PlayerCheckY.transform.localPosition = this.transform.position;
        PlayerCheckX.transform.localRotation = transform.rotation;
        PlayerCheckY.transform.localRotation = transform.rotation;
    }

    public void CheckXDistance() {
        if (XLeft < .01f || XRight < .01f) {
            SetObjectOnCurrentPosX();
        }
    }

    public void CheckYDistance()
    {
        if (YDown < .01f || YUp < .01f)
        {
            SetObjectOnCurrentPosY();
        }
    }

    public void RayCastCheck() {
        Main4Points.transform.position = transform.position;
        Main4Points.transform.rotation = transform.rotation;

        //Xleft
        Physics.Linecast(xLeft.transform.position, PlayerMiddlePos.transform.position, out hitXLeft);
        Debug.DrawLine(xLeft.transform.position, PlayerMiddlePos.transform.position, Color.red);
        XLeft = hitXLeft.distance;
        XleftHitPos = hitXLeft.point;
        //XRight
        Physics.Linecast(xRight.transform.position, PlayerMiddlePos.transform.position, out hitXRight);
        Debug.DrawLine(xRight.transform.position, PlayerMiddlePos.transform.position, Color.red);
        XRight = hitXRight.distance;
        XRightHitPos = hitXRight.point;
        //YUp
        Physics.Linecast(yUp.transform.position, PlayerMiddlePos.transform.position, out HitYUp);
        Debug.DrawLine(yUp.transform.position, PlayerMiddlePos.transform.position, Color.blue);
        YUp = HitYUp.distance;
        YUpHitPos = HitYUp.point;
        //YDown
        Physics.Linecast(yDown.transform.position, PlayerMiddlePos.transform.position, out HitYDown);
        Debug.DrawLine(yDown.transform.position, PlayerMiddlePos.transform.position, Color.blue);
        YDown = HitYDown.distance;
        YDownHitPos = HitYDown.point;

        Physics.Linecast(xLeft.transform.position, currentPlanet.transform.position, out HitXLeftPlanet);
        Debug.DrawLine(xLeft.transform.position, currentPlanet.transform.position, Color.yellow);
        PlanetXLeft = HitXLeftPlanet.distance;
        if (PlanetXLeft > .30)
        {

        }

        Physics.Linecast(xRight.transform.position, currentPlanet.transform.position, out HitXRightPlanet);
        Debug.DrawLine(xRight.transform.position, currentPlanet.transform.position, Color.yellow);
        PlanetXRight = HitXRightPlanet.distance;
        if (PlanetXRight > .30)
        {

        }

        Physics.Linecast(yUp.transform.position, currentPlanet.transform.position, out HitYUpPlanet);
        Debug.DrawLine(yUp.transform.position, currentPlanet.transform.position, Color.yellow);
        PlanetYUp = HitYUpPlanet.distance;
        if (PlanetYUp > .30)
        {

        }

        Physics.Linecast(yDown.transform.position, currentPlanet.transform.position, out HitYDownPlanet);
        Debug.DrawLine(yDown.transform.position, currentPlanet.transform.position, Color.yellow);
        PlanetYDown = HitYDownPlanet.distance;
        if (PlanetYDown > .30)
        {

        }
    }

    void FixCheckAxises()
    {
        //Main4Points.transform.position = transform.position;
        //Main4Points.transform.rotation = transform.rotation;

        // voor het overnemen van de hele positie van de speler, zal gebruikt moeten worden!
        /*
        MiddlePointX.transform.position = transform.position;
        MiddlePointY.transform.position = transform.position;
        MiddlePointX.transform.rotation = transform.rotation;
        MiddlePointY.transform.rotation = transform.rotation;
        */


        // voor het gebruiken van alleen de hoogte van de speler (hoeft niet per se te werken :()
        //MiddlePointX.transform.position = new Vector3(MiddlePointX.transform.position.x, transform.localPosition.y, MiddlePointX.transform.position.z);
        //MiddlePointY.transform.position = new Vector3(MiddlePointY.transform.position.x, transform.localPosition.y, MiddlePointY.transform.position.z);

        float xLeftYFloat = (xLeft.transform.position.y - XleftHitPos.y) * .4f + XleftHitPos.y;

         xLeft.transform.position = new Vector3(xLeft.transform.position.x, xLeftYFloat , PlayerMiddlePos.transform.position.z);
         xRight.transform.position = new Vector3(xRight.transform.position.x, xRight.transform.position.y, PlayerMiddlePos.transform.position.z);

         yUp.transform.position = new Vector3(PlayerMiddlePos.transform.position.x, yUp.transform.position.y, yUp.transform.position.z);
         yDown.transform.position = new Vector3(PlayerMiddlePos.transform.position.x, yDown.transform.position.y, yDown.transform.position.z);

        

        //LocalDistanceCheckObject.transform.position = MiddleObjectInChild.transform.position;
        //  Midle2.transform.position = MiddleObjectInChild.transform.position;
    }
}
