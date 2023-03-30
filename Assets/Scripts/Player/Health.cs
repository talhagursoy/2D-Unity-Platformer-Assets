using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    public float currentHealth{get; private set;}
    private Vector2 currentVelocity = Vector2.zero;
    private Rigidbody2D body;
    private SpriteRenderer[] childRenderers;
    private Animator anim;
    private PlayerMovement pm;
    private int numberofFlashes=3;
    [SerializeField]
    private float transparentDuration;
    private UIManager uIManager;
    [SerializeField]
    private GameObject deathVfx;
    public bool canTakeDamage;
    public delegate void DamageTakenEvent();
    public event DamageTakenEvent OnDamageTaken;
    // Start is called before the first frame update
    private void Awake() {
        currentHealth=maxHealth;
        body=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        childRenderers = transform.GetComponentsInChildren<SpriteRenderer>();
        uIManager=UIManager.FindObjectOfType<UIManager>();
        canTakeDamage=true;
    }
    public void takeDamage(float _damage,float _direction,float pushX,float pushY) {
        if(canTakeDamage==false){
            OnDamageTaken?.Invoke(); 
            return;
        } 
        currentHealth-=_damage;
        print(_damage);
        if(currentHealth<=0){
            anim.SetTrigger("Hurt");
            Instantiate(deathVfx,transform.position,transform.rotation);
            gameObject.SetActive(false);
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
            canTakeDamage=false;
        }
        else{
            Vector2 push= new Vector2(pushX*direction,pushY);
            body.AddForce(push, ForceMode2D.Impulse);
            //transform.GetComponent<SoldierEnemy>().canAttack=false; depracated since no soldierenemy component now
        }
        StartCoroutine(flash());
    }
     
    private IEnumerator flash() {
        Color flashColor = new Color(1f, 1f, 1f, 0.1f);
        Color normalColor = new Color(1f, 1f, 1f, 1f);
        for (int i = 0; i < numberofFlashes; i++) {
            foreach(SpriteRenderer childRenderer in childRenderers) {
                childRenderer.color = flashColor;
            }
            yield return new WaitForSeconds(transparentDuration);
            foreach(SpriteRenderer childRenderer in childRenderers) {
                childRenderer.color = normalColor;
            }
            yield return new WaitForSeconds(transparentDuration);
        }
        if (pm != null) {
            pm.canMove = true;
            canTakeDamage=true;
        } else {
            //transform.GetComponent<SoldierEnemy>().canAttack = true; depracated since no soldierenemy component now
        }
    }
    public void playerDeath(){
        uIManager.showDeathMenu();
    }
}
