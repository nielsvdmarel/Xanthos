using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class SplashScreenCamera : MonoBehaviour {
    [SerializeField]
    private PostProcessingProfile cc;
    [SerializeField]
    private Animator AnimController;
    [SerializeField]
    private DepthOfFieldModel.Settings BeginSettings;
    private DepthOfFieldModel UpDof;
    private DepthOfFieldModel DownDof;
    private DepthOfFieldModel.Settings settings;
    void Start ()
    {
        AnimController = this.gameObject.GetComponent<Animator>();
        cc = this.GetComponent<PostProcessingBehaviour>().profile;
        BeginSettings = cc.depthOfField.settings;
        BeginSettings.aperture = 10;
        cc.depthOfField.settings = BeginSettings;
        settings = cc.depthOfField.settings;
        
	}
	
	void Update ()
    {
        //UpdateDepthOfField();  
    }

    void UpdateDepthOfField()
    {
        if (AnimController.GetCurrentAnimatorStateInfo(0).normalizedTime > .65)
        {
            settings.aperture = (float)Mathf.Lerp(settings.aperture, 10f, Time.deltaTime / .5f);
            cc.depthOfField.settings = settings;
        }

        if (AnimController.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            //Debug.Log("sds");
        }
    }
}
