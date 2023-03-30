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
    LayerMask wallLayer;
    [SerializeField]
    private float footOffset=0.7f;
    //[SerializeField]
    //private float reachOffset=0.6f;
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
    private float dashForce;
    private AbilityManager abilityManager;
    private Ladder ladderManager;
    private bool onLadder;
    [SerializeField]
    private float ladderMoveSpeed;
    [SerializeField]
    private float fallSpeed;
    private bool castingAbility;
    private MeleeComboSystem meleeComboSystem;
    [SerializeField]
    private float wallSlideSpeed;
    private bool jumping;
    private void Awake() {
        anim=GetComponent<Animator>();
        body=GetComponent<Rigidbody2D>();
        sr=GetComponent<SpriteRenderer>();
        playerCollider=GetComponent<BoxCollider2D>();
        castingAbility=false;
        meleeComboSystem=GetComponent<MeleeComboSystem>();
    }
    private void Update() {
        if(!canMove) return;
        movePlayer();
        if(Input.GetKeyDown(KeyCode.Space)){
            jump();
        }
        wallSlide();
        if(!onWall&&canMove){
            body.gravityScale=2;
        }
        if(Input.GetButtonDown("Fire3"))
            StartCoroutine(dash());
        if(onLadder){
            ladderMovement();
        }
        if(!castingAbility)
            anim.SetBool("Jump",!onGround());
        meleeComboSystem.canAttack=onGround();
    }
    private void Start(){
        abilityManager = GameObject.Find("AbilityManager").GetComponent<AbilityManager>();
        for (int i = 0; i < abilityManager.abilities.Length; i++){
            Ability ability = abilityManager.abilities[i];
            switch (ability){
                case Strike strikeAbility:
                    strikeAbility.AbilityActivated += OnStrikeActivated;
                    break;
                case OrbSkill orbAbility:
                    orbAbility.AbilityActivated += OnOrbActivated;
                    break;
                default:
                    break;
            }
        }
        ladderManager=FindObjectOfType<Ladder>();
        if(ladderManager)
            ladderManager.onCollision+=activateLadder;
    }
    private void activateLadder(bool isOnLadder) {
        if(isOnLadder){
            onLadder=true;
            body.velocity=Vector2.zero;
        }
        else
            onLadder=false;
    }
    private void OnStrikeActivated(){
        castingAbility=true;
        anim.SetBool("Jump",false);
        anim.SetTrigger("Strike");
    }
    private void OnOrbActivated(){
        castingAbility=true;
        anim.SetBool("Jump",false);
        anim.SetTrigger("Cast");
    }
    private void movePlayer(){
        horizontalInput=Input.GetAxisRaw("Horizontal");
        if(horizontalInput>0.1f){
            transform.localScale=new Vector3(-1*Mathf.Abs(transform.localScale.x),1*Mathf.Abs(transform.localScale.x),1*Mathf.Abs(transform.localScale.x));
            onWall=false;
        }
        else if(horizontalInput<-0.1f){
            transform.localScale=new Vector3(1*Mathf.Abs(transform.localScale.x),1*Mathf.Abs(transform.localScale.x),1*Mathf.Abs(transform.localScale.x));
            onWall=false;
        }
        body.velocity=new Vector2(horizontalInput*speed,body.velocity.y);
        anim.SetBool("Walk",horizontalInput!=0);
    }
    private void jump(){
        jumping=true;
        if(!onGround()||onLadder)
            if(onWall){
                body.AddForce(new Vector2(Mathf.Sign(transform.localScale.x)*pushBackForce,wallJumpForce));
                transform.localScale=new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
                onWall=false;
            }  
            else if(onLadder){
                body.AddForce(new Vector2(0f,wallJumpForce));
            }
            else{
                return;
            }  
        else
            body.velocity=new Vector2(body.velocity.x,jumpForce);  
        Invoke("resetjump",0.2f);
    }
    private bool onGround(){
        RaycastHit2D hit=Physics2D.BoxCast(playerCollider.bounds.center,playerCollider.bounds.size,0,Vector2.down,0.1f,groundLayer);
        return hit;
    }
    private bool isWalled() {
        float direction=-playerTransform.localScale.x;
        Vector3 footEyeOffSet=new Vector3(footOffset*direction,eyeHeight,0);
        Vector2 grabDir = new Vector2(direction, 0f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position+footEyeOffSet, grabDir, grabDistance,wallLayer);
        if(hit){
            onWall=true;
        }
        return hit;
    }
    private void wallSlide() {
        if(isWalled()&&!onGround()&&horizontalInput==0){
            body.velocity=new Vector2(body.velocity.x,Mathf.Clamp(body.velocity.y, -wallSlideSpeed,float.MaxValue));
        }
    }
    private void ladderMovement(){
        if(jumping)return;
        float verticalInput = Input.GetAxisRaw("Vertical");
        if(verticalInput<=0&&onGround()){
            return;
        }
        body.velocity=new Vector2(body.velocity.x,Mathf.Clamp(body.velocity.y, ladderMoveSpeed*verticalInput,float.MaxValue));
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
    private void abilityReset() {
        castingAbility=false;
    }
    private void resetjump () {
        jumping=false;
    }
    public IEnumerator darkenPlayer(){
        print("coro called");
        sr.color=Color.gray;
        canMove=false;
        body.velocity=Vector2.zero;
        anim.SetBool("Walk",false);
        meleeComboSystem.canAttack=false;
        yield return new WaitForSeconds(2f);
        print("canmove true");
        sr.color=Color.white;
        canMove=true;
        meleeComboSystem.canAttack=true;
    }
}
