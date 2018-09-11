using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSelector : MonoBehaviour {
    public GameObject SelectedPlanet;
    private RaycastHit hit;
    void Start ()
    {
		
	}
	
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000f);

            if(hit.transform.tag == "Planet")
            {
                Debug.Log("niels is cool");
                SelectedPlanet = hit.transform.gameObject;
            }
        }
     }
 }


