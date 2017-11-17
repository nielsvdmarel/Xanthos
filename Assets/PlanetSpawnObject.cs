using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawnObject : MonoBehaviour {
    [SerializeField]
    private Vector3 Orrigin;
    [SerializeField]
    private Texture2D PlanetMap;
    public Color[] Colors;
    public GameObject[] SpawnPrefabs;
    [SerializeField]
    private Color pixel_colour;

    [SerializeField]
    private Vector2 xypos;
    private GameObject _player;
   public void Start ()
    {
        GetCurrentPixels();
        _player = GameObject.Find("Player");
    }
	
	void Update ()
    {
        transform.position = Orrigin;
    }

    void GetCurrentPixels()
    {
        PlanetLocationScript planetLocationScript = _player.GetComponent<PlanetLocationScript>();
        
        pixel_colour = PlanetMap.GetPixel(1, 5);
    }

}
