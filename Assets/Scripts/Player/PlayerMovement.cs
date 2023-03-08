using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Animator anim;
    public Rigidbody2D body;
    private SpriteRenderer sr;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private Transform playerTransform;
    private BoxCollider2D playerCollider;
    private float horizontalInput;
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private float radius;
    [SerializeField]
    private LayerMask enemyLayer;
    [SerializeField]
    private float meleeDamage;
    private float attackTimer;
    [SerializeField]
    private float attackCd;
    [SerializeField]
    LayerMask wallLayer;
    [SerializeField]
    private float footOffset=0.7f;
    [SerializeField]
    private float reachOffset=0.6f;
    [SerializeField]
    private float grabDistance=0.1f;
    [SerializeField]
    private float eyeHeight=1f;
    private bool onWall;
    [SerializeField]
    private float pushBackForce;
    [SerializeField]
    private float wallJumpForce;
    public bool canMove=true;
    [SerializeField]
    private float enemyXPush;
    [SerializeField]
    private float enemyYPush;
    [SerializeField]
    private float dashForce;
    private void Awake() {
        anim=GetComponent<Animator>();
        body=GetComponent<Rigidbody2D>();
        sr=GetComponent<SpriteRenderer>();
        playerCollider=GetComponent<BoxCollider2D>();
        attackTimer=attackCd;
    }
    private void Update() {
        movePlayer();
        if(Input.GetKeyDown(KeyCode.Space)){
            jump();
        }
        if(!onWall&&canMove)
            body.gravityScale=2;
        if(Input.GetButtonDown("Fire1")&&onGround()&&attackTimer>=attackCd){
            attack();
        }
        else
            attackTimer+=Time.deltaTime;
        if(Input.GetButtonDown("Fire3"))
            StartCoroutine(dash());
        wallCheck();
        anim.SetBool("Jump",!onGround());
        
    }
    private void movePlayer(){
        if(!canMove) return;
        horizontalInput=Input.GetAxisRaw("Horizontal");
        if(horizontalInput>0.1f){
            transform.localScale=new Vector3(-1,1,1);
            onWall=false;
        }
        else if(horizontalInput<-0.1f){
            transform.localScale=Vector3.one;
            onWall=false;
        }
        body.velocity=new Vector2(horizontalInput*speed,body.velocity.y);
        anim.SetBool("Walk",horizontalInput!=0);
    }
    private void jump(){
        if(!onGround())
            if(onWall){
                body.AddForce(new Vector2(Mathf.Sign(transform.localScale.x)*pushBackForce,wallJumpForce));
                onWall=false;
            }  
            else{
                return;
            }  
        else
            body.velocity=new Vector2(body.velocity.x,jumpForce);  
    }
    private bool onGround(){
        RaycastHit2D hit=Physics2D.BoxCast(playerCollider.bounds.center,playerCollider.bounds.size,0,Vector2.down,0.01f,groundLayer);
        return hit;
    }
    private void attack(){
        anim.SetTrigger("Attack");
        attackTimer=0;
    }
    public void damageEnemy() {
        Collider2D[] enemies=Physics2D.OverlapCircleAll(attackPoint.position,radius,enemyLayer);
        foreach(Collider2D enemy in enemies){
            enemy.GetComponent<Health>().takeDamage(meleeDamage,-Mathf.Sign(transform.localScale.x),enemyXPush,enemyYPush);
        }
    }
    private void OnDrawGizmosSelected() {
        if(attackPoint==null) return;
        Gizmos.DrawWireSphere(attackPoint.position,radius);
    }
    private void wallCheck() {
        float direction=-playerTransform.localScale.x;
        float playerHeight=playerCollider.size.y;
        Vector3 footEyeOffSet=new Vector3(footOffset*direction,eyeHeight,0);
        Vector3 reachHeightOffSet=new Vector3(reachOffset*direction,playerHeight,0);
        Vector2 grabDir = new Vector2(direction, 0f);
		RaycastHit2D ledgeCheck = Physics2D.Raycast(transform.position+reachHeightOffSet, Vector2.down, grabDistance,wallLayer);
		RaycastHit2D wallCheck = Physics2D.Raycast(transform.position+footEyeOffSet, grabDir, grabDistance,wallLayer); 
        if (!onGround() && body.velocity.y < 0f && ledgeCheck && wallCheck){ 
            body.velocity=Vector3.zero;
            body.gravityScale=1;
            onWall=true;
		}
    }

    IEnumerator dash(){
        body.velocity=Vector2.zero;
        Vector2 push=new Vector2(dashForce*-transform.localScale.x,0);
        body.AddForce(push,ForceMode2D.Impulse);
        canMove=false;
        body.gravityScale=0.5f;
        yield return new WaitForSeconds(0.5f);
        canMove=true;
    }
}
