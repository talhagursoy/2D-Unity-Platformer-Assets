using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionVfx;
    private Rigidbody2D mybody;
    private Vector3 initialPosition;
    private float startTime;// make it that orb dissappear so that it doesnt fall of map infinite
    [SerializeField]
    private LayerMask enemyLayer;
    [SerializeField]
    private float radius;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float enemyXPush;
    [SerializeField]
    private float enemyYPush;
    public float direction;
    [SerializeField]
    private float pushForce;
    [SerializeField]
    private float rotateSpeed;
    private void Awake() {
        mybody=GetComponent<Rigidbody2D>();
    }
    private void OnEnable() {
        mybody.velocity=Vector2.zero;
        Vector2 push=new Vector2(pushForce*direction,6f);// orb is being pushed only right
        mybody.AddForce(push,ForceMode2D.Impulse);
    }
    void Update(){
        transform.Rotate(-Vector3.forward,rotateSpeed*Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        Vector3 collisionPoint = other.contacts[0].point;
        Instantiate(explosionVfx, collisionPoint, transform.rotation);
        
        Collider2D[] enemies = Physics2D.OverlapCircleAll(collisionPoint, radius, enemyLayer);
        foreach(Collider2D enemy in enemies) {
            float direction = enemy.transform.position.x - transform.position.x;
            enemy.GetComponent<Health>().takeDamage(damage,Mathf.Sign(direction), enemyXPush, enemyYPush);
        }
        gameObject.SetActive(false);
    }
}