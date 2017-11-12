using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLocationScript : MonoBehaviour {
    [SerializeField]
    private Vector2 Planetlocation;
    [SerializeField]
    private float walkSpeed;
	void Start ()
    {
		
	}
	
	void Update ()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Planetlocation.x += walkSpeed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            Planetlocation.x -= walkSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Planetlocation.y += walkSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Planetlocation.y -= walkSpeed;
        }
    }

    void EnterNewPlanet()
    {
        Planetlocation.x = 0;
        Planetlocation.y = 0;
    }
}
