using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOrb : MonoBehaviour
{
    private float timeInTrigger;
    private float forceMultiplier = 2f;
    [SerializeField]
    private float timeForExplosion;
    [SerializeField]
    private GameObject explosionVFX;
    private float direction;
    [SerializeField]
    private float orbSpeed=2f;
    private float duration;
    [SerializeField]
    private float maxDuration;
    [SerializeField]
    private float damage=10f;
    [SerializeField]
    private float radius;
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private float playerPushX;
    [SerializeField]
    private float playerPushY;
    [SerializeField]
    private float idleTimer=0.5f;
    private Rigidbody2D rb;
    public float attractionForce = 2000.0f;
    private Collider2D cr;
    private void Awake() {
        cr=GetComponent<Collider2D>();
        cr.enabled=false;
    }
    private void OnEnable() {
        idleTimer=0.5f;
        timeInTrigger=0f;
    }
    private void Update(){
        idleTimer-=Time.deltaTime;
        if(idleTimer>=0) return;
        cr.enabled=true;
        transform.position=new Vector2(transform.position.x+direction*orbSpeed*Time.deltaTime,transform.position.y);
        duration+=Time.deltaTime;
        if(duration>=maxDuration){
            explode();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            //timeInTrigger=0f;
            rb=other.GetComponent<Rigidbody2D>();
            //rb.velocity=Vector2.zero;
        }
        else if(other.CompareTag("Ground"))
            explode();
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")){
            if(other.GetComponent<Strike>().isActive==true){
                print("returning");
                return;
            }
            Vector2 force=new Vector2(transform.position.x-other.transform.position.x,0f);  
            rb.AddForce(force * attractionForce * Time.deltaTime*forceMultiplier);
            timeInTrigger += Time.deltaTime;
            forceMultiplier = Mathf.Lerp(1f, 5f, timeInTrigger / 15f);
            if(timeInTrigger>=timeForExplosion){
                explode();
            }
        }
    }
    public void activateOrb(float dir){
        duration=0;
        direction=dir;
        gameObject.SetActive(true);
    }
    
    private void explode(){
        Collider2D player=Physics2D.OverlapCircle(transform.position, radius, playerLayer);
        if (player != null) {
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null) {
                playerHealth.takeDamage(damage, Mathf.Sign(direction), playerPushX, playerPushY);
            }
        }
        Instantiate(explosionVFX,transform.position,transform.rotation);
        gameObject.SetActive(false);
    }
     private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
