using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Behaviour : StateMachineBehaviour{
    private float attackCooldown = 2.0f;
    private float timeSinceLastAttack;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        timeSinceLastAttack = attackCooldown;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        timeSinceLastAttack -= Time.deltaTime;
        if (timeSinceLastAttack <= 0.0f){
            int attackType = Random.Range(0,4);
            timeSinceLastAttack = attackCooldown;
            switch (attackType){
                case 0:
                    animator.SetTrigger("Spirit");
                    break;
                case 1:
                    animator.SetTrigger("Gaze2");
                    break;
                case 2:
                    animator.SetTrigger("Spell2");
                    break;
                case 3:
                    animator.SetTrigger("Fireball");
                    break;
            }
        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
