using UnityEngine;

public class Patrol : MonoBehaviour {
    [SerializeField]
    protected Transform leftEdge;
    [SerializeField]
    protected Transform rightEdge;
    [SerializeField]
    protected float walkSpeed;
    [SerializeField]
    protected float runSpeed;
    [SerializeField]
    protected float idleTimer;
    [SerializeField]
    protected float chaseDistance;
    [SerializeField]
    protected float attackDistance;
    [SerializeField]
    protected float visionDistance;
    [SerializeField]
    protected LayerMask playerLayer;
    [SerializeField]
    protected Collider2D collid;
    [SerializeField]
    protected Transform playerTransform;
    protected bool moveRight;
    protected float timePassed;
    protected Animator anim;
    [SerializeField]
    protected Transform firePoint;
    protected Health health;
    protected Collider2D hit;
    protected virtual void Awake() {
        moveRight = true;
        timePassed = 0;
        anim=GetComponent<Animator>();
    }
    protected virtual void Update(){
        if(inSight(visionDistance))
            if(inSight(attackDistance)){
                attackPlayer();
            }
            else{
                moveToPlayer();
            }
        else{
            patrol();
        }
    }
    protected virtual void patrol() {
        if (moveRight){
            if (transform.position.x <= rightEdge.position.x){
                Move(1);
            }
            else{
                ChangeDirection();
            }    
        }
        else{
            if (transform.position.x >= leftEdge.position.x){
                Move(-1);
            }
            else{
                ChangeDirection();
            }  
        }
    }

    protected virtual void Move(float _direction) {
        timePassed=0;
        transform.localScale=new Vector2(-Mathf.Abs(transform.localScale.x)*_direction,transform.localScale.y);
        transform.position=new Vector2(transform.position.x+walkSpeed*Time.deltaTime*_direction,transform.position.y);
        anim.SetBool("Run",true);
    }

    protected virtual void ChangeDirection() {
        anim.SetBool("Run",false);
        if(timePassed >= idleTimer){
            moveRight=!moveRight;
            timePassed = 0;
        }
        else{
            timePassed += Time.deltaTime;
        }
    }
    protected virtual void moveToPlayer() {
        if(transform.position.x>playerTransform.position.x){
            transform.localScale=new Vector2(1,1);
            moveRight=false;
        } 
        else{
            transform.localScale=new Vector2(-1,1);
            moveRight=true;
        }
        if(Mathf.Abs(playerTransform.position.x-transform.position.x)<=attackDistance){
            anim.SetBool("Run",false);
            return;
        }
        timePassed=0;
        transform.position=Vector3.MoveTowards(transform.position,new Vector2(playerTransform.position.x,transform.position.y),runSpeed*Time.deltaTime);
        anim.SetBool("Run",true);
    }
    protected virtual bool inSight(float rangedDistance){
        if(playerTransform.position.x>(rightEdge.transform.position.x+attackDistance+chaseDistance)||playerTransform.position.x<(leftEdge.transform.position.x-chaseDistance-attackDistance)){
            return false;
        }
        else if(transform.position.x>(rightEdge.transform.position.x+chaseDistance)||transform.position.x<(leftEdge.transform.position.x-chaseDistance)){
            return false;
        }
        hit = Physics2D.OverlapCircle(firePoint.transform.position, rangedDistance, playerLayer);
        if (hit != null) {
            health = hit.GetComponent<Health>();
            return true;
        }
        else {
            return false;
        }
    }
    protected virtual void attackPlayer(){}
    private void OnDrawGizmos() {
        Vector3 dir;
        if(moveRight){
            dir=Vector3.right;
        }
        else{
            dir=Vector3.left;
        }    
        Gizmos.color=Color.red;
        Gizmos.DrawWireCube(collid.bounds.center+dir,collid.bounds.size);
    }
}
