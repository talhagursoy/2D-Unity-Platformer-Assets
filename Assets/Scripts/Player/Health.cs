using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    private float currentHealth;
    private Vector2 currentVelocity = Vector2.zero;
    private Rigidbody2D body;
    private SpriteRenderer[] childRenderers;
    private Animator anim;
    private PlayerMovement pm;
    private int numberofFlashes=3;
    [SerializeField]
    private float transparentDuration;
    // Start is called before the first frame update
    private void Awake() {
        currentHealth=maxHealth;
        body=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        childRenderers = transform.GetComponentsInChildren<SpriteRenderer>();
    }
    public void takeDamage(float _damage,float _direction,float pushX,float pushY) {
        currentHealth-=_damage;
        if(currentHealth<=0){
            anim.SetTrigger("Died");
            print("dead");
        }
        else
            pushback(_direction,pushX,pushY);
    }
    private void pushback(float direction,float pushX,float pushY) {
        pm=transform.GetComponent<PlayerMovement>();
        if(pm!=null){
            pm.canMove=false;
            pm.body.velocity=Vector2.zero;
            Vector2 push= new Vector2(pushX*direction,pushY);
            body.AddForce(push, ForceMode2D.Impulse);
        }
        else{
            Vector2 push= new Vector2(pushX*direction,pushY);
            body.AddForce(push, ForceMode2D.Impulse);
            transform.GetComponent<SoldierEnemy>().canAttack=false;
        }
        StartCoroutine(flash());
    }
     
    IEnumerator flash(){
        for (int i = 0; i < numberofFlashes; i++)
        {
            foreach(SpriteRenderer childRenderer in childRenderers) {
                Color color = childRenderer.color;
                color.a = 0.1f;
                childRenderer.color = color;
            }
            yield return new WaitForSeconds(transparentDuration);
            foreach(SpriteRenderer childRenderer in childRenderers) {
                Color color = childRenderer.color;
                color.a = 1f;
                childRenderer.color = color;
            }
            yield return new WaitForSeconds(transparentDuration);
        }
        if(pm!=null)
            pm.canMove=true;
        else
            transform.GetComponent<SoldierEnemy>().canAttack=true;
    }
}
