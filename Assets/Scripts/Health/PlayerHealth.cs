using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : BaseHealth{
    private PlayerMovement pm;
    protected override void Awake() {
        base.Awake();
        pm=transform.GetComponent<PlayerMovement>();
    }
    public override void TakeDamage(float damage, float direction){
        if(immune==true)
            return;
        currentHealth-=damage;
        if(currentHealth<=0){
            //Instantiate(deathVfx,transform.position,transform.rotation);
            gameObject.SetActive(false);
        }
        else{
            pushback(direction,4f,2f);
        }
    }
    private void pushback(float direction,float pushX,float pushY) {
        if(pm!=null){
            pm.canMove=false;
            pm.body.velocity=Vector2.zero;
            Vector2 push= new Vector2(pushX*direction,pushY);
            pm.body.AddForce(push, ForceMode2D.Impulse);
            immune=true;
        }
        StartCoroutine(flash());
    }
    protected override void FlashCompleted(){
        pm.canMove = true;
        immune=false;
    }
}
