﻿using UnityEngine;
using System.Collections;

public class StopBool : StateMachineBehaviour {

    public enum BooltoStop
    {
        Dive,
        Punch,
        Ninja
    }
    public BooltoStop boolToStop;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (stateInfo.normalizedTime > 1 && !animator.IsInTransition(0))
        {
            switch (boolToStop)
            {
                case BooltoStop.Dive: PlayerData.Instance.canDive = false; break;
                case BooltoStop.Ninja: PlayerData.Instance.canNinja = false; break;
                case BooltoStop.Punch: PlayerData.Instance.canPunch = false; break;
            }
        }
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
