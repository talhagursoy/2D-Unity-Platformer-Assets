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
    // Start is called before the first frame update
    // Update is called once per frame
    void Update(){
        transform.position=new Vector2(transform.position.x+speed*Time.deltaTime*transform.localScale.x,transform.position.y);
        duration+=Time.deltaTime;
        if(duration>=maxDuration){
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            other.GetComponent<Health>().takeDamage(damage,Mathf.Sign(transform.localScale.x),playerXPush,playerYPush);
        }
        //play anim if want to
        gameObject.SetActive(false);
    }
    public void activateArrow(float _direction) {
        duration=0;
        transform.localScale=new Vector3(Mathf.Abs(transform.localScale.x)*_direction,transform.localScale.y,transform.localScale.z);
        gameObject.SetActive(true);
    }
}
