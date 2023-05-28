using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeCollider : MonoBehaviour{
    private CircleCollider2D circleCollider;
    private void Awake() {
        circleCollider=GetComponent<CircleCollider2D>();
        circleCollider.enabled=false;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            other.gameObject.layer = LayerMask.NameToLayer("Default");
            other.gameObject.GetComponent<SpriteRenderer>().color=Color.gray;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            other.gameObject.layer = LayerMask.NameToLayer("Player");
            other.gameObject.GetComponent<SpriteRenderer>().color=Color.white;
        }
    }
    public void activateCircle(Vector3 position) {
        StartCoroutine(activateCircleCol(position));
    }
    IEnumerator activateCircleCol(Vector3 position){
        circleCollider.transform.position=position;
        circleCollider.enabled=true;
        yield return new WaitForSeconds(5f);
        circleCollider.enabled=false;
        Destroy(transform.parent.gameObject);
    }
}