using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : BaseHealth{
    protected Rigidbody2D rb;
    public delegate void DamageTakenEvent();
    public event DamageTakenEvent OnDamageTaken;
    private FloatingHealthBar healthBar;
    protected override void Awake() {
        base.Awake();
        rb=transform.GetComponent<Rigidbody2D>();
    }
    private void OnEnable() {
        healthBar=GetComponentInChildren<FloatingHealthBar>();
        healthBar.updateHealthBar(maxHealth,maxHealth);
    }
    public override void TakeDamage(float damage, float direction){
        if(immune==true){
            OnDamageTaken?.Invoke(); 
            return;
        } 
        currentHealth-=damage;
        healthBar.updateHealthBar(currentHealth,maxHealth);
        if(currentHealth<=0){
            if(deathVfx!=null)
                Instantiate(deathVfx,transform.position,transform.rotation);
            gameObject.SetActive(false);
        }
        else{
            pushback(direction,400f,200f);
        }
    }
    private void pushback(float direction,float pushX,float pushY) {
        Vector2 push= new Vector2(pushX*direction,pushY);
        rb.AddForce(push, ForceMode2D.Impulse);
        StartCoroutine(flash());
    }
}
