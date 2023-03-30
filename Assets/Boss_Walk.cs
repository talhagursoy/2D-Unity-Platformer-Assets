using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Walk : StateMachineBehaviour
{
    [SerializeField]
    private float speed=1.5f;
    Transform player;
    Rigidbody2D body;
    Boss boss;
    float attackRange=3f;
    float cdTimer=1f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player=GameObject.FindWithTag("Player").transform;
        body=animator.GetComponent<Rigidbody2D>();
        boss=animator.GetComponent<Boss>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(player.position, body.position) <= attackRange&&cdTimer>=1f){
            float randomNum = Random.Range(0f, 1f);
            if (randomNum <= 0.4f){
                animator.SetTrigger("Block");
                Debug.Log("Blocking");
                cdTimer=0f;
            }
            else{
                animator.SetTrigger("Attack");
                Debug.Log("Attacking");
                cdTimer=0f;
            }
        }
        else{
            if(Vector2.Distance(player.position, body.position) >= attackRange){
                boss.LookAtPlayer();
                Vector2 target = new Vector2(player.position.x,body.position.y);
                Vector2 newPos = Vector2.MoveTowards(body.position,target,speed*Time.fixedDeltaTime);
                body.MovePosition(newPos);
            }
        }
        cdTimer+=Time.deltaTime;   
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        //animator.ResetTrigger("Block");
    }
}
