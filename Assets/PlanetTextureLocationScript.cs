 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlanetTextureLocationScript : MonoBehaviour
{
    public Vector2 Planetlocation;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private Texture2D PlanetMap;
    private int[] TextureColorCords = new int[262144];
    private Color[] texCords = new Color[26144];
    [SerializeField]
    private List<Vector2> CurrentActivePixels;
    public int largenum;
    public Color MainColor;

    //cords
    public int x;
    private int xold;
    public int y;
    private int yold;
    [SerializeField]
    private int CheckSpaceHalf;
    [SerializeField]
    private int FullScanPixels;

    public Color testColor;

    void Start()
    {
        SetTextureColorCords();
        CurrentActivePixels = new List<Vector2>();
        xold = x;
        yold = y;
    }

    void Update()
    {
        FixCords();
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

        if (Input.GetKey(KeyCode.W))
        {
            Planetlocation.y += walkSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetTexturePoint();
            GetTexturePointUp();
        }

        if (Input.GetKey(KeyCode.S))
        {
            Planetlocation.y -= walkSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Planetlocation.x += walkSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Planetlocation.x -= walkSpeed;
        }
    }

    void FixCords()
    {
       x = Mathf.RoundToInt(Planetlocation.x);
       y = Mathf.RoundToInt(Planetlocation.y);

        if (Planetlocation.x > 512)
        {
            Planetlocation.x = 0;
        }

        if (Planetlocation.x < 0)
        {
            Planetlocation.x = 512;
        }

        if (Planetlocation.y > 512)
        {
            Planetlocation.y = 0;
        }

        if (Planetlocation.y < 0)
        {
            Planetlocation.y = 512;
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
        uppointx -= CheckSpaceHalf;
        uppointy += CheckSpaceHalf;
        Color CurrentColor;

        int checkpointUp = uppointx + ((PlanetMap.height - uppointy) * PlanetMap.width);
        for (int i = 0; i < FullScanPixels; i++)
        {
            CurrentColor = texCords[checkpointUp + i];
            if (CurrentColor != MainColor)
            {
                Vector2 CurrentAdd = new Vector2(uppointx += i, uppointy);
                if (!CurrentActivePixels.Contains(CurrentAdd))
                {
                    CurrentActivePixels.Add(CurrentAdd);
                }

                else if (CurrentActivePixels.Contains(CurrentAdd))
                {
                    CurrentActivePixels.Remove(CurrentAdd);
                }
            }
        }

    }

    void GetTexturePointDown()
    {
        int Downpointx = x;
        int Downpointy = y;
        Downpointx -= CheckSpaceHalf;
        Downpointy -= CheckSpaceHalf;
        Color CurrentColor;

        int checkpointDown = Downpointx + ((PlanetMap.height - Downpointy) * PlanetMap.width);
        for (int i = 0; i < FullScanPixels; i++)
        {
            CurrentColor = texCords[checkpointDown + i];
            if (CurrentColor != MainColor)
            {
                Vector2 CurrentAdd = new Vector2(Downpointx += i, Downpointy);
                if (!CurrentActivePixels.Contains(CurrentAdd))
                {
                    CurrentActivePixels.Add(CurrentAdd);
                }

                else if (CurrentActivePixels.Contains(CurrentAdd))
                {
                    CurrentActivePixels.Remove(CurrentAdd);
                }
            }
        }

    }

    void GetTexturePointLeft()
    {
        int Leftpointx = x;
        int Leftpointy = y;
        Leftpointx -= CheckSpaceHalf;
        Leftpointy -= CheckSpaceHalf;
        Color CurrentColor;

        for (int i = 0; i < FullScanPixels; i++)
        {
            int checkpointLeft = Leftpointx + ((PlanetMap.height - (Leftpointy + i)) * PlanetMap.width);
            CurrentColor = texCords[checkpointLeft];

            if (CurrentColor != MainColor)
            {
                Vector2 CurrentAdd = new Vector2(Leftpointx, (Leftpointy += i));
                if (!CurrentActivePixels.Contains(CurrentAdd))
                {
                    CurrentActivePixels.Add(CurrentAdd);
                }

                else if (CurrentActivePixels.Contains(CurrentAdd))
                {
                    CurrentActivePixels.Remove(CurrentAdd);
                }
            }
        }
    }

    void GetTexturePointRight()
    {
        int Rightpointx = x;
        int Rightpointy = y;
        Rightpointx += CheckSpaceHalf;
        Rightpointy -= CheckSpaceHalf;
        Color CurrentColor;

        for (int i = 0; i < FullScanPixels; i++)
        {
            
            int checkpointRight = Rightpointx + ((PlanetMap.height - (Rightpointy + i)) * PlanetMap.width);
            CurrentColor = texCords[checkpointRight];
            if (CurrentColor != MainColor)
            {
                Vector2 CurrentAdd = new Vector2(Rightpointx, (Rightpointy += i));
                if (!CurrentActivePixels.Contains(CurrentAdd))
                {
                    CurrentActivePixels.Add(CurrentAdd);
                }

                else if (CurrentActivePixels.Contains(CurrentAdd))
                {
                    CurrentActivePixels.Remove(CurrentAdd);
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

}
