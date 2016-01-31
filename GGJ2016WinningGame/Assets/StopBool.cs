using UnityEngine;
using System.Collections;

public class StopBool : StateMachineBehaviour {

    public enum BooltoStop
    {
        Dive,
        Punch,
        Ninja
    }
    public BooltoStop boolToStop;
    public GameObject projectile;
    int frameCount = 0;
    bool shot = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        frameCount++;
        Debug.Log(frameCount);
        if (stateInfo.normalizedTime > 1 && !animator.IsInTransition(0))
        {
            switch (boolToStop)
            {
                case BooltoStop.Dive: PlayerData.Instance.canDive = false; break;
                case BooltoStop.Ninja: PlayerData.Instance.canNinja = false; break;
                case BooltoStop.Punch: PlayerData.Instance.canPunch = false; break;
            }
        }
        if (frameCount >= 30 && !shot && projectile != null)
        {
            GameObject go = Instantiate(projectile) as GameObject;
            go.transform.rotation = animator.transform.rotation;
            go.transform.position = animator.transform.position + animator.transform.up * 1 + animator.transform.forward * 1;
            shot = true;
        }
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        frameCount = 0;
        shot = false;
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
