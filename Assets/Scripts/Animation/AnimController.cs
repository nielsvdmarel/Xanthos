using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class AnimController : MonoBehaviour {
    [Header("Main needed Components")]
    public Animator AnimatorController;
    [SerializeField]
    private AnimatorController SpecificAnimController;
    [Header("Variables")]
    [SerializeField]
    private int RandomInterActAnim;
    private Animation CurrentAnim;
    [SerializeField]
    private PlayerController playerController;
	void Start () {
        AnimatorController = this.gameObject.GetComponent<Animator>();

	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractAnim();
            CurrentAnim = this.gameObject.GetComponent<Animation>();
        }
        Movement();
    }

    void Movement() {
        if(playerController.CurrentSpeedMovement == PlayerController.SpeedMovement.Idle)
        {
            AnimatorController.SetFloat("Speed", Mathf.Lerp(AnimatorController.GetFloat("Speed"), 0, Time.deltaTime * 15));
            AnimatorController.SetFloat("InputMagnitude", 0);
        }

        if(playerController.CurrentSpeedMovement == PlayerController.SpeedMovement.Walking)
        {
            AnimatorController.SetFloat("Speed", Mathf.Lerp(AnimatorController.GetFloat("Speed"), 0f, Time.deltaTime * 8));
            AnimatorController.SetFloat("InputMagnitude", 1);
        }

        if(playerController.CurrentSpeedMovement == PlayerController.SpeedMovement.Running)
        {
            AnimatorController.SetFloat("Speed", Mathf.Lerp(AnimatorController.GetFloat("Speed"), 1, Time.deltaTime * 6));
            AnimatorController.SetFloat("InputMagnitude", 1);
        }
    }

    void WalkAnim() {

    }

    void RunAnim() {

    }

    void JumpAnim() {

    }

    void InteractAnim() {
        RandomInterActAnim = Random.Range(0, 4);
        AnimatorController.SetInteger("Interact", RandomInterActAnim);
        AnimatorController.SetBool("IsInteracting", true);
    }

    void WavingAnim() {

    }

    void HoodAnimHandler(bool HoodState) {

    }


}
