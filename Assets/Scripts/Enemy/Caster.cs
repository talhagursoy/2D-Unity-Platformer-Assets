using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caster : Patrol{
    [SerializeField]
    private float attackCd;
    private float attackTimer;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float playerXPush;
    [SerializeField]
    private float playerYPush;
    public bool canAttack;
    [SerializeField]
    private GameObject[] portals;
    [SerializeField]
    private float playerOffset;
    [SerializeField]
    private float enemyOffset;
    [SerializeField]
    private float heightOffset;
    [SerializeField]
    private GameObject portal;
    protected override void Awake() {
        base.Awake();
        attackTimer = attackCd;
        canAttack = true;
    }
    protected override void Update() {
        base.Update();
        attackTimer += Time.deltaTime;
    }
    protected override void attackPlayer(){
        float newX;
        if(transform.position.x > playerTransform.position.x){
            newX=Mathf.Abs(transform.localScale.x);
            moveRight=false;
        }
        else{
            moveRight=true;
            newX=-Mathf.Abs(transform.localScale.x);
        }
        transform.localScale=new Vector2(newX,transform.localScale.y);
        anim.SetBool("Run",false);
        if(attackTimer>=attackCd&&canAttack){
            anim.SetTrigger("Attack");
            attackTimer=0;
        }
    }
    private void summonPortal(){
        float direction=transform.position.x>playerTransform.position.x?1:-1;
        Vector2 portalPoint;
        if(playerTransform.position.x*direction+playerOffset<transform.position.x*direction-enemyOffset){
            portalPoint=new Vector2(playerTransform.position.x+playerOffset*direction*-Input.GetAxisRaw("Horizontal"),heightOffset);
        }
        else{
            portalPoint=new Vector2(transform.position.x-enemyOffset*direction,heightOffset);
        }
        
        Instantiate(portal,portalPoint,Quaternion.identity);
    }
}
