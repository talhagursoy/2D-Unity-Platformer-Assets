using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Behaviour : StateMachineBehaviour
{
    private float attackCooldown = 2.0f; // The duration of the cooldown period (in seconds)
    private float timeSinceLastAttack;
    private string previousStateName;
    private bool bossDown;
    private float downTimer=5f;
    private float timePassed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        timeSinceLastAttack = attackCooldown;
        if (stateInfo.fullPathHash != 0){
            AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(layerIndex);
            if (clips.Length > 0){
                previousStateName = clips[0].clip.name;
            }
        }
        if(previousStateName=="LoseWings"){
            bossDown=true;
        }       
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        timeSinceLastAttack -= Time.deltaTime;
        if(bossDown)
            timePassed+=Time.deltaTime;
        if(timePassed>=downTimer){
            animator.SetTrigger("Transition");
            timePassed=0;
        }      
        if (timeSinceLastAttack <= 0.0f){
            int attackType = Random.Range(0,4);
            timeSinceLastAttack = attackCooldown;
            switch (attackType){
                case 0:
                    animator.SetTrigger("CloseExplosion");
                    break;
                case 1:
                    animator.SetTrigger("Gaze");
                    break;
                case 2:
                    animator.SetTrigger("CreateShield");
                    break;
                case 3:
                    animator.SetTrigger("GravityOrb");
                    break;
            }
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){}
}
