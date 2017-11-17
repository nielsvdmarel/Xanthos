using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLocationScript : MonoBehaviour {
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

    public TextureCord[] FullSpace;

    void Start ()
    {
        Planetlocation.x = PlanetMap.height / 2;
        Planetlocation.y = PlanetMap.width / 2;
        PlanetPixelDecider();
	}
	
	void Update ()
    {
        FixCords();
        PlanetPixelDecider();

        if (Input.GetKey(KeyCode.W))
        {
            Planetlocation.y += walkSpeed;
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

    }
}
