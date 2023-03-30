using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : Ability{
    [SerializeField]
    private float duration;
    [SerializeField]
    private float distance;
    public bool isActive = false;
    private float startTime;
    [SerializeField]
    private Collider2D strikeCollider;
    private void Awake() {
        strikeCollider.enabled=false;
    }
    public override void Activate(){
        if (!isActive){
            Transform child=transform.Find("StrikeCollider");
            transform.GetComponent<PlayerMovement>().canMove=false;
            Vector3 targetPosition = new Vector2 (transform.position.x-Mathf.Sign(transform.localScale.x)*distance,transform.position.y);
            isActive = true;
            strikeCollider.enabled=true;
            StartCoroutine(MovePlayerOverTime(targetPosition, duration));
            if (AbilityActivated != null)
            {
                AbilityActivated();
            }
        }
    }
    IEnumerator MovePlayerOverTime(Vector2 targetPosition, float duration){
        float timePassed = 0f;
        Vector3 startingPos = transform.position;
        while (timePassed<duration){
            timePassed += Time.deltaTime;
            float t = Mathf.Clamp01(timePassed / duration);
            transform.position = Vector3.Lerp(startingPos, targetPosition, t);
            // Update the UI to show the player's current position
            yield return null;
        }
        isActive = false;
        strikeCollider.enabled=false;
        transform.GetComponent<PlayerMovement>().canMove = true;
    }
}
