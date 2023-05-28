using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsHealth : BaseHealth{
    protected override void Awake() {
        base.Awake();
    }
    public override void TakeDamage(float damage, float direction){
        currentHealth-=damage;
        if(currentHealth<=0){
            gameObject.SetActive(false);
        }
        else{
            StartCoroutine(flash());
        }
    }
}
