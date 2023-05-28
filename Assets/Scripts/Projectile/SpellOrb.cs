using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellOrb : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            Boss bossScript = other.GetComponent<Boss>();
            if(bossScript!=null){
                //bossScript.spellOrbCollision();
            }
            Destroy(gameObject);
        }
    }
}
