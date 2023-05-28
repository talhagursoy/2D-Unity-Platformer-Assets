using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour{
    private float damage=10f;
    private BoxCollider2D boxCollider2D;
    [SerializeField]
    private Transform player;
    private void OnEnable() {
        boxCollider2D.enabled=false;
        transform.position=player.transform.position;
    }
    private void Awake(){
        boxCollider2D=GetComponent<BoxCollider2D>();
    }
    private void spellDamage() {
        boxCollider2D.enabled=true;
        Invoke("destroyObject",0.2f);
    }
    private void destroyObject() {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
            other.GetComponent<PlayerHealth>().TakeDamage(damage,0);
    }
}
