using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawnObject : MonoBehaviour {
    [SerializeField]
    private Vector3 Orrigin;
    [SerializeField]
    private Texture2D PlanetMap;
    [Header("Spawn Buildings Colors")]
    public Color[] Colors;
    [Header("SpawnObjects")]
    public GameObject[] SpawnUp;
    public GameObject[] SpawnDown;
    public GameObject[] SpawnRight;
    public GameObject[] SpawnLeft;
    [SerializeField]
    private Color pixel_colour;
    [Header("Rest")]
    [SerializeField]
    private Vector2 xypos;
    private GameObject _player;
   public void Start ()
    {
        _player = GameObject.Find("Player");
        //GetCurrentPixels();
    }
	
	void Update ()
    {
        transform.position = Orrigin;
    }

    void GetCurrentPixels()
    {
        PlanetTextureLocationScript planetLocationScript = _player.GetComponent<PlanetTextureLocationScript>();
        
        pixel_colour = PlanetMap.GetPixel(1, 5);
    }

}
