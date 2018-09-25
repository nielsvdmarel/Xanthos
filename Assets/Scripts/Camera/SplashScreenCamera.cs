using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class SplashScreenCamera : MonoBehaviour {
    [SerializeField]
    private PostProcessingProfile cc;
    [SerializeField]
    private Animator AnimController;
    private DepthOfFieldModel UpDof;
    private DepthOfFieldModel DownDof;
    private DepthOfFieldModel.Settings settings;

    void Start ()
    {
        AnimController = this.gameObject.GetComponent<Animator>();
        cc = this.GetComponent<PostProcessingBehaviour>().profile;
        settings = cc.depthOfField.settings;
	}
	
	void Update ()
    {
        if (AnimController.GetCurrentAnimatorStateInfo(0).normalizedTime > .65)
        {
            settings.aperture = (float)Mathf.Lerp(settings.aperture, .7f, Time.deltaTime / 2f);
            cc.depthOfField.settings = settings;
        }

        if (AnimController.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            //Debug.Log("sds");
        }
    }
}
