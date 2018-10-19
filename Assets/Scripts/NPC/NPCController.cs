using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour {
    public GameObject exclamationMark;
    public GameObject cloudPopUp;

    public bool InPlayerView = false;
	void Start () {
		
	}

    public void Update()
    {
        if (InPlayerView)
        {
            EnableNPCmark(exclamationMark);
        }

        else if (!InPlayerView)
        {
            DisableNPCmark(exclamationMark);
        }
    }

    public void EnableNPCmark(GameObject Mark)
    {
        Mark.SetActive(true);
    }

    public void DisableNPCmark(GameObject Mark)
    {
        Mark.SetActive(false);
    }
}
