using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetTextureLocationScript : MonoBehaviour {
    public Vector2 Planetlocation;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private Texture2D PlanetMap;
    private int[] TextureColorCords = new int[262144];
    private Color[] texCords = new Color[26144];

    public Color testColor;

    void Start ()
    {
        SetTextureColorCords();
	}
	
	void Update ()
    {
        FixCords();

        if (Input.GetKey(KeyCode.W))
        {
            Planetlocation.y += walkSpeed;
            
        }

        if (Input.GetKey(KeyCode.Space))
        {
            GetTexturePoint();

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
        testColor = texCords[250];
        //x plus y keer breedte 
    }
}
