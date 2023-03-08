using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private float pushX;
    [SerializeField]
    private float pushY;
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.CompareTag("Player")){
            if(other.collider.transform.position.x>=transform.position.x)
                other.collider.GetComponent<Health>().takeDamage(damage,1,pushX,pushY);
            else
                other.collider.GetComponent<Health>().takeDamage(damage,-1,pushX,pushY);
            print("takingdamage");
        }
    }
}
