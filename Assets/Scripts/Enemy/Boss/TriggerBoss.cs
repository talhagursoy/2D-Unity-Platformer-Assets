using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoss : MonoBehaviour{
    private BoxCollider2D boxCollider2D;
    private void Awake() {
        boxCollider2D=GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            StartCoroutine(GameManager.summonBoss1());
            boxCollider2D.enabled=false;
        }
    }
}
