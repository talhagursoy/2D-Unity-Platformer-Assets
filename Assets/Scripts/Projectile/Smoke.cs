using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour{
    [SerializeField]
    private ParticleSystem smoke;
    private Transform smokebomb;
    [SerializeField]
    private SmokeCollider smokeCollider;
    [SerializeField]
    private AudioClip steem;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Ground")){
            SoundManager.instance.playSound(steem);
            Instantiate(smoke,transform.position, Quaternion.identity); 
            smokeCollider.activateCircle(transform.position);
            Destroy(gameObject);
        }
    }
}
