using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierEnemy : MonoBehaviour
{
    [SerializeField]
    Transform leftEdge;
    [SerializeField]
    Transform rightEdge;
    private Animator anim;
    [SerializeField]
    private float speed;
    private bool moveLeft;
    [SerializeField]
    private float idleTimer;
    private float timePassed;
    private bool chasePlayer=false;
    [SerializeField]
    private float rangedDistance;
    [SerializeField]
    private float meleeDistance;
    [SerializeField]
    LayerMask playerLayer;
    [SerializeField]
    private Collider2D collid;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private float attackCd;
    private float attackTimer;
    private Health health;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float chaseDistance;
    private float gazeOffSet=3;
    [SerializeField]
    private float playerXPush;
    [SerializeField]
    private float playerYPush;
    public bool canAttack;
    [SerializeField]
    private GameObject[] arrows;
    //private string weaponType;
    [SerializeField]
    private Transform firePoint;

    private void Awake() {
        anim=GetComponent<Animator>();
        attackTimer=attackCd;
        canAttack=true;
        //weaponType=FindWeaponInChildren(transform,"Bow");
    }
    private void Update(){
        if(playerInsight(rangedDistance)||chasePlayer)
            if(playerInsight(meleeDistance))
                attackPlayer();
            else
                moveToPlayer();
        else{
            patrol();
        }
        attackTimer+=Time.deltaTime;//look for a better place to increament
    }
    private void patrol() {
        if(moveLeft){
            if(transform.position.x<=rightEdge.position.x)
                move(1);
            else{
                changeDirection();
            }    
        }
        else{
            if(transform.position.x>=leftEdge.position.x)
                move(-1);
            else{
                changeDirection();
            }  
        }
    }
    private void move(float _direction) {
        timePassed=0;
        transform.localScale=new Vector2(-Mathf.Abs(transform.localScale.x)*_direction,transform.localScale.y);
        transform.position=new Vector2(transform.position.x+speed*Time.deltaTime*_direction,transform.position.y);
        anim.SetBool("Run",true);
    }
    private void changeDirection() {
        anim.SetBool("Run",false);
        if(timePassed>=idleTimer)
            moveLeft=!moveLeft;
        else
            timePassed+=Time.deltaTime;
    }
    private void moveToPlayer() {
        if(playerTransform.position.x>(rightEdge.transform.position.x+chaseDistance)||playerTransform.position.x<(leftEdge.transform.position.x-chaseDistance)){
            chasePlayer=false;
            changeDirection();
            return;
        }
        else if(transform.position.x>(rightEdge.transform.position.x+chaseDistance)||transform.position.x<(leftEdge.transform.position.x-chaseDistance)){
            chasePlayer=false;
            changeDirection();
            return;
        }
        if(transform.position.x>playerTransform.position.x){
            transform.localScale=new Vector2(1,1);
            moveLeft=false;
        } 
        else{
            transform.localScale=new Vector2(-1,1);
            moveLeft=true;
        }
        if(Mathf.Abs(playerTransform.position.x-transform.position.x)<=meleeDistance){
            anim.SetBool("Run",false);
            return;
        }
        timePassed=0;
        transform.position=Vector3.MoveTowards(transform.position,new Vector2(playerTransform.position.x,transform.position.y),speed*Time.deltaTime);
        anim.SetBool("Run",true);
    }   
    private bool playerInsight(float distance){
        Vector3 dir;
        if(Mathf.Sign(transform.localScale.x)<=0){
            dir=Vector3.right;
        }
        else{
            dir=Vector3.left;
        }
        RaycastHit2D hit=Physics2D.BoxCast(collid.bounds.center+dir/gazeOffSet,collid.bounds.size,0,dir,distance,playerLayer);
        if(hit.collider!=null){
            health = hit.collider.GetComponent<Health>();
            chasePlayer=true;
            return true;
        }   
        else
            return false;
    }
    private void OnDrawGizmos() {
        Vector3 dir;
        if(moveLeft){
            dir=Vector3.right;
        }
        else{
            dir=Vector3.left;
        }    
        Gizmos.color=Color.red;
        Gizmos.DrawWireCube(collid.bounds.center+dir/gazeOffSet,collid.bounds.size);
    }
    private void attackPlayer(){
        anim.SetBool("Run",false);
        if(attackTimer>=attackCd&&canAttack){
            anim.SetTrigger("Attack");
            attackTimer=0;
        }
    }
    private void damagePlayer() {
        if(playerInsight(meleeDistance))
            health.takeDamage(damage,-Mathf.Sign(transform.localScale.x),playerXPush,playerYPush);
    }
    private int findArrow() {
        //can optimize this to go from the previous i so that it doesnt run from start all the time
        for (int i = 0; i < arrows.Length; i++){
            if(!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    /*string FindWeaponInChildren(Transform transform, string weaponName) {
        if (transform.name == weaponName) {
            return weaponName;
        }
        for (int i = 0; i < transform.childCount; i++) {
            if (FindWeaponInChildren(transform.GetChild(i), weaponName)==weaponName) {
                return weaponName;
            }
        }
        return null;
        this method is not needed now cause we dont actually need to know what weapon they hold
    }*/
    private void shootArrow(){
        arrows[findArrow()].transform.position=firePoint.position;
        arrows[findArrow()].GetComponent<ArrowScript>().activateArrow(-Mathf.Sign(transform.localScale.x));
        attackTimer=0;
    }
}
