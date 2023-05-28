using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform pointA,pointB;
    [SerializeField]
    private float speed;
    private Vector2 targetPos;
    private void Start() {
        targetPos=pointB.position;
    }
    // Update is called once per frame
    void Update(){
        if(Vector2.Distance(transform.position,pointA.position)<0.1f) 
            targetPos=pointB.position;
        if(Vector2.Distance(transform.position,pointB.position)<0.1f) 
            targetPos=pointA.position;
        transform.position=Vector2.MoveTowards(transform.position,targetPos,speed*Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
            other.transform.SetParent(this.transform);
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player"))
            other.transform.SetParent(null);
    }
}
