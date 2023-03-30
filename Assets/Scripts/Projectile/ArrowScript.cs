using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float playerXPush;
    [SerializeField]
    private float playerYPush;
    private float duration;
    [SerializeField]
    private float maxDuration;
    private Vector3 initialDirection;
    void Update(){
        transform.position = transform.position + initialDirection * speed * Time.deltaTime;
        duration+=Time.deltaTime;
        if(duration>=maxDuration){
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            other.GetComponent<Health>().takeDamage(damage,transform.rotation.z>0.5? -1:1,playerXPush,playerYPush);
        }
        print("triggered");
        //play anim if want to
        gameObject.SetActive(false);
    }
    public void activateArrow(float _direction,Vector3 playerTransform) {
        duration=0;
        initialDirection = (playerTransform-transform.position).normalized;
        Vector3 dir = playerTransform - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        gameObject.SetActive(true);
    }
}
