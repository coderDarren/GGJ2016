using UnityEngine;
using System.Collections;

public class CharacterAnimController : MonoBehaviour {

    PlayerController controller;
    Animator anim;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool("Grounded", controller.Grounded());
        anim.SetFloat("Running", controller.forwardInput);
    }
}
