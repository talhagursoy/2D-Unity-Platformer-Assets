using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour{
    [SerializeField]
    private float damage;
    [SerializeField]
    private float playerXPush;
    [SerializeField]
    private float playerYPush;
    private float duration;
    [SerializeField]
    private float maxDuration;
    [SerializeField]
    private GameObject explosionVFX;
    [SerializeField]
    private float radius=1f;
    [SerializeField]
    private LayerMask playerLayer;
    void Update(){
        duration+=Time.deltaTime;
        if(duration>=maxDuration){
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Collider2D player=Physics2D.OverlapCircle(transform.position, radius, playerLayer);
        if (player != null) {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null) {
                playerHealth.TakeDamage(damage, 0);
            }
        }
        Instantiate(explosionVFX,transform.position,transform.rotation);
        Destroy(gameObject);
    }
}
