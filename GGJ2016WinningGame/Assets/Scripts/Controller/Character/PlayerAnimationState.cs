using UnityEngine;
using System.Collections;

public class PlayerAnimationState : MonoBehaviour {

	public enum PlayerState
    {
        STAND_IDLE,//0
        STAND_WALK,//1
        STAND_WALKBACK,//2
        STAND_RUN,//3
        STAND_RUNBACK,//4
        STAND_STRAFELEFT,//5
        STAND_STRAFERIGHT,//6
        IDLE_JUMP,//7
        RUN_JUMP,//8
        CROUCH_IDLE,//9
        CROUCH_WALK,//10
        CROUCH_WALKBACK,//11
        CROUCH_STRAFELEFT,//12
        CROUCH_STRAFERIGHT,//13
        STRANGLE,//14
        THROW//15
    }
    public PlayerState playerState;
    
}
