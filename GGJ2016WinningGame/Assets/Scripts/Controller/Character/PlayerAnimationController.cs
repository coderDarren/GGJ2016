using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour {

    PlayerController controller;
    PlayerAnimationState animState;
    Animator anim;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetFloat("Forward", controller.forwardInput);
        anim.SetBool("Grounded", controller.grounded);
        anim.SetBool("Crouch", controller.crouch);
        anim.SetBool("Walk", controller.walk);
        anim.SetBool("TPS", controller.thirdPersonShooter);
        anim.SetFloat("Turn", controller.turnInput);
        anim.SetFloat("AbsoluteForward", Mathf.Abs(controller.forwardInput));
        anim.SetBool("Dive", PlayerData.Instance.canDive);
    }

}
