using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : BaseHealth{
    public delegate void DamageTakenEvent();
    public event DamageTakenEvent OnDamageTaken;
    private FloatingHealthBar healthBar;
    public bool shielded;
    private bool transitioned;
    protected override void Awake(){
        base.Awake();
        healthBar=GetComponentInChildren<FloatingHealthBar>();
        healthBar.updateHealthBar(maxHealth,maxHealth);
        transitioned=false;
    }
    public override void TakeDamage(float damage, float direction){
        if(immune)
            return;
        if(shielded==true){
            OnDamageTaken?.Invoke(); 
            return;
        } 
        currentHealth-=damage;
        healthBar.updateHealthBar(currentHealth,maxHealth);
        if(currentHealth<=maxHealth/4&&!transitioned){
            transform.GetComponent<Boss>().animTrigger("Transition");
        }
        else if(currentHealth<=0)
            transform.GetComponent<Boss>().animTrigger("Death");
    }
    private void setImmunity(int imn) {
        if(imn==1)
            immune=true;
        else{
            immune=false;
            if(!transitioned){
                currentHealth=maxHealth;
                healthBar.updateHealthBar(currentHealth,maxHealth);
                transitioned=true;
            }
        }    
    }
}

