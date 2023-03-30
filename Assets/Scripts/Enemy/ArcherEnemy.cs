using UnityEngine;

public class ArcherEnemy : Patrol {
    [SerializeField]
    private float attackCd;
    private float attackTimer;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float playerXPush;
    [SerializeField]
    private float playerYPush;
    public bool canAttack;
    [SerializeField]
    private GameObject[] arrows;
    protected override void Awake() {
        base.Awake();
        attackTimer = attackCd;
        canAttack = true;
    }
    protected override void Update() {
        base.Update();
        attackTimer += Time.deltaTime;
    }
    protected override void attackPlayer(){
        float newX;
        if(transform.position.x > playerTransform.position.x){
            newX=Mathf.Abs(transform.localScale.x);
            moveRight=false;
        }
        else{
            moveRight=true;
            newX=-Mathf.Abs(transform.localScale.x);
        }
        //float newX = transform.position.x > playerTransform.position.x ? Mathf.Abs(transform.localScale.x) : -Mathf.Abs(transform.localScale.x);
        //print(newX);
        transform.localScale=new Vector2(newX,transform.localScale.y);
        anim.SetBool("Run",false);
        if(attackTimer>=attackCd&&canAttack){
            anim.SetTrigger("Attack");
            attackTimer=0;
        }
    }
    private void damagePlayer() {
        if(inSight(attackDistance))
            if(health!=null)
                health.takeDamage(damage,-Mathf.Sign(transform.localScale.x),playerXPush,playerYPush);
    }
    private int findArrow() {
        for (int i = 0; i < arrows.Length; i++){
            if(!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private void shootArrow(){
        print("activating arrow");
        arrows[findArrow()].transform.position=firePoint.position;
        arrows[findArrow()].GetComponent<ArrowScript>().activateArrow(-Mathf.Sign(transform.localScale.x),playerTransform.position);
        attackTimer=0;
    }
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