using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeComboSystem : MonoBehaviour
{
    private float atkTimer;
    [SerializeField]
    private float atkCd;
    [SerializeField]
    private int maxCombo;
    private int currentCombo;
    [SerializeField]
    private GameObject firstAttackFx;
    [SerializeField]
    private GameObject secondAttackFx;
    [SerializeField]
    private GameObject thirdAttackFx;
    [SerializeField]
    private float radius;
    private float currentRadius;
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private float meleeDamage;
    [SerializeField]
    private LayerMask enemyLayer;
    private Animator anim;
    [SerializeField]
    private float vfxOffset;
    public bool canAttack;
    [SerializeField]
    private AudioClip attack1;
    [SerializeField]
    private AudioClip attack2;
    [SerializeField]
    private AudioClip attack3;
    private void Awake() {
        anim=GetComponent<Animator>();
        currentCombo=0;
        atkTimer=atkCd;
        canAttack=true;
    }
    

    private void Update() {
        if(Input.GetButtonDown("Fire1")&&atkTimer>=atkCd&&canAttack){
            anim.SetTrigger("Attack"); 
            atkTimer=0; 
        }  
        else
            atkTimer+=Time.deltaTime;
    }

    public void damageEnemy() {
        Vector2 vfxPos=new Vector2(attackPoint.transform.position.x-(Mathf.Sign(transform.localScale.x)*vfxOffset),attackPoint.transform.position.y+0.2f);
        switch (currentCombo) {
            case 0:
                firstAttackFx.SetActive(true);
                SoundManager.instance.playSound(attack1);
                currentRadius=radius;
                break;
            case 1:
                secondAttackFx.SetActive(true);
                SoundManager.instance.playSound(attack2);
                currentRadius=radius*2;
                break;
            case 2:
                thirdAttackFx.SetActive(true);
                SoundManager.instance.playSound(attack3);
                currentRadius=radius*3;
                break;
            default :
                break;
        }
        currentCombo++;
        if(currentCombo==maxCombo)
            currentCombo=0;
        Collider2D[] objects = Physics2D.OverlapCircleAll(attackPoint.position, currentRadius);
        foreach (Collider2D obj in objects) {
            if (obj.gameObject == gameObject) {
                continue;
            }
            IDamagable damagableObj = obj.GetComponent<IDamagable>();
            if (damagableObj != null) {
                damagableObj.TakeDamage(meleeDamage, -Mathf.Sign(transform.localScale.x));
            }
        }
    }
    public void resetAtk() {
        atkTimer=atkCd;
    }
    public void resetCombo() {
        currentCombo=0;
    }
}
