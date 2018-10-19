 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlanetTextureLocationScript : MonoBehaviour
{
    public InputManager inputManager;
    public PlayerController playerController;
    public SphereCollider CurrentPlanetSphereCollider;
    public float currentRadius;
    public float secondRadius;
    public float thirdRadius;
    public float SDistance;
    public Vector3 LastCheckedPos;

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

    void Start()
    {
        LastCheckedPos = this.transform.position;
        GetNewPlanetRadius();
        SetTextureColorCords();
        currentActivePixels = new List<Vector2>();
        xold = x;
        yold = y;
    }

    void Update()
    {
        FixCords();
        SphereDistance(LastCheckedPos, transform.position);
        if (x != xold) {
            TestAllPoints();
            xold = x;
        }

        if (y != yold) {
            TestAllPoints();
            yold = y;
        }
        if (!playerController.isStoppedByCollider)
        {
            if (inputManager.direction.normalized.z > 0)
            {
                //W or Forward
                planetlocation.y += walkPixelSpeed;
            }

            if (inputManager.direction.normalized.z < 0)
            {
                //S or Bakcward
                planetlocation.y -= walkPixelSpeed;
            }

            if (inputManager.direction.normalized.x > 0)
            {
                //D or Right
                planetlocation.x += walkPixelSpeed;
            }

            if (inputManager.direction.normalized.x < 0)
            {
                //A or Left
                planetlocation.x -= walkPixelSpeed;
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

    void GetNewPlanetRadius()
    {
        CurrentPlanetSphereCollider = this.gameObject.GetComponent<FauxGravityBody>().attractor.gameObject.GetComponent<SphereCollider>();
        currentRadius = (CurrentPlanetSphereCollider.radius) * (CurrentPlanetSphereCollider.gameObject.transform.localScale.x);
        secondRadius = CurrentPlanetSphereCollider.gameObject.GetComponent<Renderer>().bounds.extents.y;

    }

    public void SphereDistance(Vector3 point1, Vector3 point2)
    {
       SDistance = Mathf.Atan((Vector3.Magnitude((Vector3.Cross(point1, point2)))) / (Vector3.Dot(point1, point2)));
        if (SDistance > .05f)
        {
            LastCheckedPos = transform.position;
        }
    }

}
