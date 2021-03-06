﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour {
    [Header("Main needed Components")]
    public Animator AnimatorController;
    //private AnimatorController SpecificAnimController;
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
        JumpAnim();
    }

    void Movement() {
        if(playerController.CurrentSpeedMovement == PlayerController.SpeedMovement.Idle)
        {
            AnimatorController.SetFloat("Speed", Mathf.Lerp(AnimatorController.GetFloat("Speed"), 0, Time.deltaTime * 5));
            AnimatorController.SetFloat("InputMagnitude", Mathf.Lerp(AnimatorController.GetFloat("InputMagnitude"), 0, Time.deltaTime * 15));
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

        if (playerController.IsGrounded)
        {
            AnimatorController.SetBool("Movement", true);
            AnimatorController.SetBool("SmallJump", false);
        }
    }

    void WalkAnim() {

    }

    void RunAnim() {

    }

    void JumpAnim() {
        if (!playerController.IsGrounded)
        {
            //AnimatorController.applyRootMotion = true;
            //AnimatorController.SetBool("SmallJump", true);
        }

        if (playerController.IsGrounded)
        {
            //AnimatorController.applyRootMotion = false;
            //AnimatorController.SetBool("SmallJump", false);
        }

        if(playerController.IsJumping == true)
        {
            StartCoroutine(JumpAnimTimer());
        }
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

    private IEnumerator JumpAnimTimer()
    {
            AnimatorController.SetBool("SmallJump", true);
            AnimatorController.SetBool("Movement", false);
        if (AnimatorController.GetCurrentAnimatorStateInfo(0).IsName("rig|jump_all"))
        {
            yield return new WaitForSeconds(AnimatorController.GetCurrentAnimatorStateInfo(0).length);
            playerController.IsJumping = false;
            //AnimatorController.SetBool("Movement", true);
            //AnimatorController.SetBool("SmallJump", false);
        }
    }


}
