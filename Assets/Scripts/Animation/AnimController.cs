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
        if(playerController.currentSpeed > .1 && playerController.currentSpeed < .5)
        {
            AnimatorController.SetFloat("Speed", 0);
            AnimatorController.SetFloat("InputMagnitude", 1);
        }

        if(playerController.currentSpeed > .5)
        {
            AnimatorController.SetFloat("Speed", 1);
            AnimatorController.SetFloat("InputMagnitude", 1);
        }

        if(playerController.currentSpeed > .05 && playerController.currentSpeed < .1)
        {
            AnimatorController.SetFloat("InputMagnitude", 0);
            AnimatorController.SetFloat("Speed", 0);
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
