using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour
{
    private Collider2D hit;
    private IDamagable damagable;
    private float attackDistance=1f;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float runSpeed=2f;
    [SerializeField]
    private LayerMask playerLayer;
    private Animator anim;
    private float radius=2f;
    private float damage=10f;
    [SerializeField]
    private GameObject explosionVFX;
    private bool canMove;
    private void Awake() {
        anim=GetComponent<Animator>();
        canMove=false;
    }
    void Update(){
        if(!canMove)
            return;
        if(inSight(attackDistance))
            attackPlayer();
        else
            moveToPlayer();
    }
    private void moveToPlayer() {
        if(Mathf.Abs(player.position.x-transform.position.x)<=attackDistance){
            return;
        }
        transform.position=Vector3.MoveTowards(transform.position,new Vector2(player.position.x,transform.position.y),runSpeed*Time.deltaTime);
    }
    private bool inSight(float attackDistance) {
        hit = Physics2D.OverlapCircle(transform.position, attackDistance, playerLayer);
        if (hit != null) {
            damagable = hit.GetComponent<IDamagable>();
            return true;
        }
        else
            return false;
    }
    private void attackPlayer() {
        
        anim.SetTrigger("Explode");
    }
    private void explode() {
        Collider2D player=Physics2D.OverlapCircle(transform.position, radius, playerLayer);
        if (player != null) {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null) {
                playerHealth.TakeDamage(damage, 0);
            }
        }
        Instantiate(explosionVFX,transform.position,transform.rotation);
        gameObject.SetActive(false);
    }
    private void setCanMove() {
        canMove=true;
    }
}
