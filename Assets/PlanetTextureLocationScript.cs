﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlanetTextureLocationScript : MonoBehaviour {
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
    public int y;
    [SerializeField]
    private int CheckSpaceHalf;
    [SerializeField]
    private int FullScanPixels;

    public Color testColor;

    void Start ()
    {
        SetTextureColorCords();
        CurrentActivePixels = new List<Vector2>();
    }
	
	void Update ()
    {
        FixCords();

        if (Input.GetKey(KeyCode.W))
        {
            Planetlocation.y += walkSpeed;
            GetTexturePointUp();
            GetTexturePointDown();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetTexturePoint();
            GetTexturePointUp();
        }

        if (Input.GetKey(KeyCode.S))
        {
            Planetlocation.y -= walkSpeed;
            GetTexturePointUp();
            GetTexturePointDown();
        }

        if (Input.GetKey(KeyCode.D))
        {
            Planetlocation.x += walkSpeed;
            GetTexturePointUp();
            GetTexturePointDown();
        }

        if (Input.GetKey(KeyCode.A))
        {
            Planetlocation.x -= walkSpeed;
            GetTexturePointUp();
            GetTexturePointDown();
        }
    }

    void FixCords()
    {
        if (Planetlocation.x > 500)
        {
            Planetlocation.x = 0;
        }

        if (Planetlocation.x < 0)
        {
            Planetlocation.x = 500;
        }

        if (Planetlocation.y > 500)
        {
            Planetlocation.y = 0;
        }

        if (Planetlocation.y < 0)
        {
            Planetlocation.y = 500;
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
       
        largenum =  x  + ((PlanetMap.height - y) * PlanetMap.width);
        testColor = texCords[largenum];
    }

    void GetTexturePointUp()
    {
        int uppointx = Mathf.RoundToInt(Planetlocation.x);
        int uppointy = Mathf.RoundToInt(Planetlocation.y);
        uppointx -= CheckSpaceHalf;
        uppointy += CheckSpaceHalf;

        int checkpointUp = uppointx + ((PlanetMap.height - uppointy) * PlanetMap.width);
        for (int i = 0; i < FullScanPixels; i++)
        {
            testColor = texCords[checkpointUp + i];
            if(testColor != MainColor)
            {
                Vector2 CurrentAdd = new Vector2(uppointx += i, uppointy);
                if (!CurrentActivePixels.Contains(CurrentAdd))
                {
                    CurrentActivePixels.Add(CurrentAdd);
                }

                else if(CurrentActivePixels.Contains(CurrentAdd))
                {
                    CurrentActivePixels.Remove(CurrentAdd);
                }
            }
        }
        
    }

    void GetTexturePointDown()
    {
        int Downpointx = Mathf.RoundToInt(Planetlocation.x);
        int Downpointy = Mathf.RoundToInt(Planetlocation.y);
        Downpointx -= CheckSpaceHalf;
        Downpointy -= CheckSpaceHalf;

        int checkpointDown = Downpointx + ((PlanetMap.height - Downpointy) * PlanetMap.width);
        for (int i = 0; i < FullScanPixels; i++)
        {
            testColor = texCords[checkpointDown + i];
            if (testColor != MainColor)
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
}