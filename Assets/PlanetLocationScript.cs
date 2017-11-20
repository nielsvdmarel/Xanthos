using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLocationScript : MonoBehaviour
{
    public Vector2 Planetlocation;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private Texture2D PlanetMap;
    [SerializeField]
    private Color pixel_colour;

    [SerializeField]
    public int planetlocationx;
    [SerializeField]
    public int planetlocationy;
    public Color[] Boven;
    public Color[] Onder;
    public Color[] Rechts;
    public Color[] Links;
    [SerializeField]
    private Color MainPlanetColor;
    private Vector2 CurrenlyAdding;
    public int ScanSpaceHorizon;
    public int ScanSpaceVertical;

    public TextureCord[] FullSpace;
    public Color[] BuildingColor;
    public List<Vector2> SpawnedCords;

    void Start()
    {
        SpawnedCords = new List<Vector2>();
        Planetlocation.x = 50;// PlanetMap.height / 2;
        Planetlocation.y = 252; // PlanetMap.width / 2;
        PlanetPixelDecider();
        CheckSurounding();
    }

    void Update()
    {
        FixCords();
        PlanetPixelDecider();

        if (Input.GetKey(KeyCode.W))
        {
            Planetlocation.y += walkSpeed;
            CheckSurounding();
            AddSpawnCordBoven();
        }

        if (Input.GetKey(KeyCode.S))
        {
            Planetlocation.y -= walkSpeed;
            CheckSurounding();
            AddSpawnCordOnder();
        }

        if (Input.GetKey(KeyCode.D))
        {
            Planetlocation.x += walkSpeed;
            CheckSurounding();
            AddSpawnCordRechts();
        }

        if (Input.GetKey(KeyCode.A))
        {
            Planetlocation.x -= walkSpeed;
            CheckSurounding();
            AddSpawnCordLinks();
        }
    }


    void PlanetPixelDecider()
    {
        planetlocationx = Mathf.RoundToInt(Planetlocation.x);
        planetlocationy = Mathf.RoundToInt(Planetlocation.y);

        pixel_colour = PlanetMap.GetPixel(planetlocationx, planetlocationy);
    }

    void EnterNewPlanet()
    {
        Planetlocation.x = 0;
        Planetlocation.y = 0;
    }

    void FixCords()
    {
        if (Planetlocation.x > 2048)
        {
            Planetlocation.x = 0;
        }

        if (Planetlocation.x < 0)
        {
            Planetlocation.x = 2048;
        }

        if (Planetlocation.y > 2048)
        {
            Planetlocation.y = 0;
        }

        if (Planetlocation.y < 0)
        {
            Planetlocation.y = 2048;
        }
    }

    void CheckSurounding()
    {
        Boven = PlanetMap.GetPixels(planetlocationx - ScanSpaceVertical / 2, planetlocationy + ScanSpaceHorizon / 2, 50, 1);
        Onder = PlanetMap.GetPixels(planetlocationx - ScanSpaceVertical / 2, planetlocationy - ScanSpaceHorizon / 2, 50, 1);
        Links = PlanetMap.GetPixels(planetlocationx - ScanSpaceVertical / 2, planetlocationy - ScanSpaceVertical / 2, 1, 50);
        Rechts = PlanetMap.GetPixels(planetlocationx + ScanSpaceVertical / 2, planetlocationy - ScanSpaceVertical / 2, 1, 50);
    }

    void AddSpawnCordBoven()
    {
        for (int i = 0; i < ScanSpaceHorizon; i++)
        {
            if (Boven[i] != MainPlanetColor)
            {
                CurrenlyAdding = new Vector2(planetlocationx + i, planetlocationy + 25);

                if (!SpawnedCords.Contains(CurrenlyAdding))
                {
                    SpawnedCords.Add(new Vector2(planetlocationx + i, planetlocationy + 25));

                    for (int j = 0; j < BuildingColor.Length; j++)
                    {
                        if (Boven[i] == BuildingColor[j])
                        {


                        }
                    }
                }
            }
        }
    }
    void AddSpawnCordOnder()
    {
        for (int i = 0; i < ScanSpaceHorizon; i++)
        {
            if (Onder[i] != MainPlanetColor)
            {
                CurrenlyAdding = new Vector2(planetlocationx + i, planetlocationy - 25);

                if (!SpawnedCords.Contains(CurrenlyAdding))
                {
                    SpawnedCords.Add(new Vector2(planetlocationx + i, planetlocationy -25));

                    for (int j = 0; j < BuildingColor.Length; j++)
                    {
                        if (Onder[i] == BuildingColor[j])
                        {


                        }
                    }
                }
            }
        }
    }
    void AddSpawnCordRechts()
    {
        for (int i = 0; i < ScanSpaceHorizon; i++)
        {
            if (Rechts[i] != MainPlanetColor)
            {
                CurrenlyAdding = new Vector2(planetlocationx + 25, planetlocationy + i);

                if (!SpawnedCords.Contains(CurrenlyAdding))
                {
                    SpawnedCords.Add(new Vector2(planetlocationx + 25, planetlocationy + i));

                    for (int j = 0; j < BuildingColor.Length; j++)
                    {
                        if (Rechts[i] == BuildingColor[j])
                        {


                        }
                    }
                }
            }
        }
    }
    void AddSpawnCordLinks()
    {
        for (int i = 0; i < ScanSpaceHorizon; i++)
        {
            if (Links[i] != MainPlanetColor)
            {
                CurrenlyAdding = new Vector2(planetlocationx - 25, planetlocationy + i);

                if (!SpawnedCords.Contains(CurrenlyAdding))
                {
                    SpawnedCords.Add(new Vector2(planetlocationx - 25, planetlocationy + i));

                    for (int j = 0; j < BuildingColor.Length; j++)
                    {
                        if (Links[i] == BuildingColor[j])
                        {


                        }
                    }
                }
            }
        }
    }


    void SpawnBuilding()
    {

    }
}
