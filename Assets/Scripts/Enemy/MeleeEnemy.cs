using UnityEngine;

public class MeleeEnemy : Patrol {
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
        anim.SetBool("Run",false);
        if(attackTimer>=attackCd&&canAttack){
            anim.SetTrigger("Attack");
            attackTimer=0;
        }
    }
    private void damagePlayer() {
        if(inSight(attackDistance))
            if(damagable!=null)
                damagable.TakeDamage(damage,-Mathf.Sign(transform.localScale.x));
    }
}