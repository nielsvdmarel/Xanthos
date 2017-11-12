using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureCord : MonoBehaviour {
    public Vector2 texCord;
    public RaycastHit hit;
    [SerializeField]
    private float TextureResX;
    [SerializeField]
    private float TextureResY;


    
	void Start ()
    {
		
	}
	
	void Update ()
    {
        if (Physics.Raycast(transform.position, -transform.up, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.cyan);

            texCord.x = hit.textureCoord.x * TextureResX;
            texCord.y = hit.textureCoord.y * TextureResY;
        }
    }
}
