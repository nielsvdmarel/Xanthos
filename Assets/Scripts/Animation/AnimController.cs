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

	void Start () {
        AnimatorController = this.gameObject.GetComponent<Animator>();
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractAnim();
            CurrentAnim = this.gameObject.GetComponent<Animation>();
        }
    }

    void IdleAnim() {

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
