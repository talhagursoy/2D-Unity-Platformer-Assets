using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeCollider : MonoBehaviour
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private float pushX;
    [SerializeField]
    private float pushY;
    private float direction;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.position.x>transform.position.x)
            direction=1;
        else
            direction=-1;
        if(other.CompareTag("Enemy")){
            other.GetComponent<Health>().takeDamage(damage,direction,pushX,pushY);
        }
    }
}
