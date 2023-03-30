using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Behaviour : StateMachineBehaviour
{
    private float attackCooldown = 1.0f; // The duration of the cooldown period (in seconds)
    private float timeSinceLastAttack;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        timeSinceLastAttack = attackCooldown;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        timeSinceLastAttack -= Time.deltaTime;
        if (timeSinceLastAttack <= 0.0f){
            int attackType = Random.Range(0, 5);
            timeSinceLastAttack = attackCooldown;
            switch (attackType){
                case 0:
                    animator.SetTrigger("CastSpell");
                    break;
                case 1:
                    //animator.SetTrigger("Teleport");
                    break;
                case 2:
                    animator.SetTrigger("CreateShield");
                    break;
                case 3:
                    animator.SetTrigger("GravityOrb");
                    break;
                case 4:
                    animator.SetTrigger("Gaze");
                    break;
            }
        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
