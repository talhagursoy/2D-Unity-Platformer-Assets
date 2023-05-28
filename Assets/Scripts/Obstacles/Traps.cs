using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Traps : MonoBehaviour{
    public float damage;
    public float pushX;
    public float pushY;
    protected void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            other.GetComponent<PlayerHealth>().TakeDamage(damage,other.transform.position.x>=transform.position.x?1:-1);
        }
    }
}
