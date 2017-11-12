using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawnObject : MonoBehaviour {
    [SerializeField]
    private Vector3 Orrigin;
    [SerializeField]
    private Texture PlanetMap;
    public Texture[] Colors;
    public GameObject[] SpawnPrefabs;
    void Start ()
    {
		
	}
	
	
	void Update ()
    {
        transform.position = Orrigin; 
	}
}
