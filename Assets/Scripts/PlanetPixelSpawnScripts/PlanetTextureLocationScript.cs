 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlanetTextureLocationScript : MonoBehaviour
{
    public InputManager inputManager;
    public PlayerController playerController;
    public Vector2 planetlocation;
    [SerializeField]
    private float walkPixelSpeed;
    [SerializeField]
    private float RunPixelSpeed;
    [SerializeField]
    private Texture2D PlanetMap;
    private int[] textureColorCords = new int[262144];
    private Color[] texCords = new Color[26144];
    [SerializeField]
    private List<Vector2> currentActivePixels;
    public int largenum;
    public Color mainColor;

    //cords
    public int x;
    private int xold;
    public int y;
    private int yold;
    [SerializeField]
    private int checkSpaceHalf;
    [SerializeField]
    private int fullScanPixels;
    public Color testColor;

    [Header("RayCast Check tests")]
    public GameObject LocalDistanceCheckObject;
    public GameObject Midle2;
    public GameObject MiddlePointX;
    public GameObject MiddlePointY;
    public GameObject xLeft;
    public GameObject xRight;
    public GameObject yUp;
    public GameObject yDown;

    public Vector3 Xdifference;
    public Vector3 YDifference;
    public GameObject currentPlanet;
    public GameObject Main4Points;
    public GameObject LeftUp, LeftDown, RightUp, RightDown;
    public GameObject MiddleObjectInChild;

    [SerializeField]
    private float XLeft;
    [SerializeField]
    private float XRight;
    [SerializeField]
    private float YUp;
    [SerializeField]
    private float YDown;

    public float PlanetXLeft, PlanetXRight, PlanetYUp, PlanetYDown;

    void Start()
    {
        SetTextureColorCords();
        currentActivePixels = new List<Vector2>();
        xold = x;
        yold = y;
    }

    void Update()
    {
        RayCastCheck();
        FixCords();
        CheckXDistance();
        CheckYDistance();
        CheckInputForCord();
    }

    public void CheckInputForCord()
    {
        if (x != xold)
        {
            TestAllPoints();
            xold = x;
        }

        if (y != yold)
        {
            TestAllPoints();
            yold = y;
        }
        if (!playerController.isStoppedByCollider)
        {
            if (inputManager.direction.normalized.z > 0)
            {
                //W or Forward
                if (playerController.CurrentSpeedMovement == PlayerController.SpeedMovement.Walking)
                {
                    planetlocation.y += walkPixelSpeed;
                }
                if (playerController.CurrentSpeedMovement == PlayerController.SpeedMovement.Running)
                {
                    planetlocation.y += RunPixelSpeed;
                }
            }

            if (inputManager.direction.normalized.z < 0)
            {
                //S or Bakcward
                if (playerController.CurrentSpeedMovement == PlayerController.SpeedMovement.Walking)
                {
                    planetlocation.y -= walkPixelSpeed;
                }
                if (playerController.CurrentSpeedMovement == PlayerController.SpeedMovement.Running)
                {
                    planetlocation.y -= RunPixelSpeed;
                }
            }

            if (inputManager.direction.normalized.x > 0)
            {
                //D or Right
                if (playerController.CurrentSpeedMovement == PlayerController.SpeedMovement.Walking)
                {
                    planetlocation.x += walkPixelSpeed;
                }
                if (playerController.CurrentSpeedMovement == PlayerController.SpeedMovement.Running)
                {
                    planetlocation.x += RunPixelSpeed;
                }
            }

            if (inputManager.direction.normalized.x < 0)
            {
                //A or Left
                if (playerController.CurrentSpeedMovement == PlayerController.SpeedMovement.Walking)
                {
                    planetlocation.x -= walkPixelSpeed;
                }
                if (playerController.CurrentSpeedMovement == PlayerController.SpeedMovement.Running)
                {
                    planetlocation.x -= RunPixelSpeed;
                }
            }
        }
    }

    void FixCords()
    {
       x = Mathf.RoundToInt(planetlocation.x);
       y = Mathf.RoundToInt(planetlocation.y);

        if (planetlocation.x > 512)
        {
            planetlocation.x = 0;
        }

        if (planetlocation.x < 0)
        {
            planetlocation.x = 512;
        }

        if (planetlocation.y > 512)
        {
            planetlocation.y = 0;
        }

        if (planetlocation.y < 0)
        {
            planetlocation.y = 512;
        }
    }

    void SetTextureColorCords()
    {
        texCords = PlanetMap.GetPixels(0, 0, 512, 512);
    }

    void GetTexturePoint()
    {
        //testColor = texCords[250];
        //x plus y keer breedte
        //currennumb = 118272;
        //0 = 500
        //500 - 40 = 460;
        largenum = x + ((PlanetMap.height - y) * PlanetMap.width);
        testColor = texCords[largenum];
    }

    void GetTexturePointUp()
    {
        int uppointx = x;
        int uppointy = y;
        uppointx -= checkSpaceHalf;
        uppointy += checkSpaceHalf;
        Color CurrentColor;

        int checkpointUp = uppointx + ((PlanetMap.height - uppointy) * PlanetMap.width);
        for (int i = 0; i < fullScanPixels; i++)
        {
            CurrentColor = texCords[checkpointUp + i];
            if (CurrentColor != mainColor)
            {
                Vector2 CurrentAdd = new Vector2(uppointx += i, uppointy);
                if (!currentActivePixels.Contains(CurrentAdd))
                {
                    currentActivePixels.Add(CurrentAdd);
                }

                else if (currentActivePixels.Contains(CurrentAdd))
                {
                    currentActivePixels.Remove(CurrentAdd);
                }
            }
        }

    }

    void GetTexturePointDown()
    {
        int Downpointx = x;
        int Downpointy = y;
        Downpointx -= checkSpaceHalf;
        Downpointy -= checkSpaceHalf;
        Color CurrentColor;

        int checkpointDown = Downpointx + ((PlanetMap.height - Downpointy) * PlanetMap.width);
        for (int i = 0; i < fullScanPixels; i++)
        {
            CurrentColor = texCords[checkpointDown + i];
            if (CurrentColor != mainColor)
            {
                Vector2 CurrentAdd = new Vector2(Downpointx += i, Downpointy);
                if (!currentActivePixels.Contains(CurrentAdd))
                {
                    currentActivePixels.Add(CurrentAdd);
                }

                else if (currentActivePixels.Contains(CurrentAdd))
                {
                    currentActivePixels.Remove(CurrentAdd);
                }
            }
        }

    }

    void GetTexturePointLeft()
    {
        int Leftpointx = x;
        int Leftpointy = y;
        Leftpointx -= checkSpaceHalf;
        Leftpointy -= checkSpaceHalf;
        Color CurrentColor;

        for (int i = 0; i < fullScanPixels; i++)
        {
            int checkpointLeft = Leftpointx + ((PlanetMap.height - (Leftpointy + i)) * PlanetMap.width);
            CurrentColor = texCords[checkpointLeft];

            if (CurrentColor != mainColor)
            {
                Vector2 CurrentAdd = new Vector2(Leftpointx, (Leftpointy += i));
                if (!currentActivePixels.Contains(CurrentAdd))
                {
                    currentActivePixels.Add(CurrentAdd);
                }

                else if (currentActivePixels.Contains(CurrentAdd))
                {
                    currentActivePixels.Remove(CurrentAdd);
                }
            }
        }
    }

    void GetTexturePointRight()
    {
        int Rightpointx = x;
        int Rightpointy = y;
        Rightpointx += checkSpaceHalf;
        Rightpointy -= checkSpaceHalf;
        Color CurrentColor;

        for (int i = 0; i < fullScanPixels; i++)
        {

            int checkpointRight = Rightpointx + ((PlanetMap.height - (Rightpointy + i)) * PlanetMap.width);
            CurrentColor = texCords[checkpointRight];
            if (CurrentColor != mainColor)
            {
                Vector2 CurrentAdd = new Vector2(Rightpointx, (Rightpointy += i));
                if (!currentActivePixels.Contains(CurrentAdd))
                {
                    currentActivePixels.Add(CurrentAdd);
                }

                else if (currentActivePixels.Contains(CurrentAdd))
                {
                    currentActivePixels.Remove(CurrentAdd);
                }
            }
        }
    }

    void TestAllPoints()
    {
        GetTexturePointUp();
        GetTexturePointDown();
        GetTexturePointLeft();
        GetTexturePointRight();
    }

    public void SetObjectOnCurrentPosX()
    {
        Debug.Log("resetX Side");
        MiddlePointX.transform.localPosition = this.transform.position;
        MiddlePointX.transform.localRotation = transform.localRotation;
        
    }

    public void SetObjectOnCurrentPosY()
    {
        Debug.Log("Reset y side");
        MiddlePointY.transform.localPosition = this.transform.position;
        MiddlePointY.transform.localRotation = transform.localRotation;
       
    }

    public void CheckXDistance()
    {
        if(XLeft < .01f || XRight < .01f) {
            SetObjectOnCurrentPosX();
        }
    }

    public void CheckYDistance()
    {
        if(YDown < .01f || YUp < .01f) {
            SetObjectOnCurrentPosY();
        }
    }

    public void RayCastCheck()
    {
        //MiddlePointY.transform.localRotation = this.transform.rotation;
        //MiddlePointX.transform.localRotation = this.transform.rotation;
        Main4Points.transform.position = transform.position;
        Main4Points.transform.rotation = transform.rotation;

        RaycastHit HitYUp;
        RaycastHit HitYDown;
        RaycastHit hitXLeft;
        RaycastHit hitXRight;

        RaycastHit HitYUpPlanet, HitYDownPlanet, HitXLeftPlanet, HitXRightPlanet;

        //Xleft
        Physics.Linecast(xLeft.transform.position, LocalDistanceCheckObject.transform.position, out hitXLeft);
        Debug.DrawLine(xLeft.transform.position, LocalDistanceCheckObject.transform.position, Color.red);
        XLeft = hitXLeft.distance;
        //XRight
        Physics.Linecast(xRight.transform.position, LocalDistanceCheckObject.transform.position, out hitXRight);
        Debug.DrawLine(xRight.transform.position, LocalDistanceCheckObject.transform.position, Color.red);
        XRight = hitXRight.distance; 
        //YUp
        Physics.Linecast(yUp.transform.position, Midle2.transform.position, out HitYUp);
        Debug.DrawLine(yUp.transform.position, Midle2.transform.position, Color.blue);
        YUp = HitYUp.distance;
        //YDown
        Physics.Linecast(yDown.transform.position, Midle2.transform.position, out HitYDown);
        Debug.DrawLine(yDown.transform.position, Midle2.transform.position, Color.blue);
        YDown = HitYDown.distance;


        // planet looker
        //
        //
        Physics.Linecast(xLeft.transform.position, currentPlanet.transform.position, out HitXLeftPlanet);
        Debug.DrawLine(xLeft.transform.position, currentPlanet.transform.position, Color.yellow);
        PlanetXLeft = HitXLeftPlanet.distance;
        if(PlanetXLeft > .30)
        {
           
        }

        Physics.Linecast(xRight.transform.position, currentPlanet.transform.position, out HitXRightPlanet);
        Debug.DrawLine(xRight.transform.position, currentPlanet.transform.position, Color.yellow);
        PlanetXRight = HitXRightPlanet.distance;
        if(PlanetXRight > .30)
        {
            
        }

        Physics.Linecast(yUp.transform.position, currentPlanet.transform.position, out HitYUpPlanet);
        Debug.DrawLine(yUp.transform.position, currentPlanet.transform.position, Color.yellow);
        PlanetYUp = HitYUpPlanet.distance;
        if(PlanetYUp > .30)
        {
            
        }

        Physics.Linecast(yDown.transform.position, currentPlanet.transform.position, out HitYDownPlanet);
        Debug.DrawLine(yDown.transform.position, currentPlanet.transform.position, Color.yellow);
        PlanetYDown = HitYDownPlanet.distance;
        if(PlanetYDown > .30)
        {
           
        }

        Main4Points.transform.position = transform.position;
        Main4Points.transform.rotation = transform.rotation;

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


        xLeft.transform.position = new Vector3(xLeft.transform.position.x, xLeft.transform.position.y, LocalDistanceCheckObject.transform.position.z);
        xRight.transform.position = new Vector3(xRight.transform.position.x, xRight.transform.position.y, LocalDistanceCheckObject.transform.position.z);

        yUp.transform.position = new Vector3(Midle2.transform.position.x, yUp.transform.position.y, yUp.transform.position.z);
        yDown.transform.position = new Vector3(Midle2.transform.position.x, yDown.transform.position.y, yDown.transform.position.z);

        LocalDistanceCheckObject.transform.position = MiddleObjectInChild.transform.position;
        Midle2.transform.position = MiddleObjectInChild.transform.position;
    }

}
