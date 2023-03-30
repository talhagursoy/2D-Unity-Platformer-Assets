using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSkill : Ability{
    [SerializeField]
    private GameObject Orb;
    [SerializeField]
    private Transform attackPoint;

    public override void Activate(){
        if (AbilityActivated != null)
        {
            AbilityActivated();
        }
        Invoke("ActivateOrb", 0.4f);
    }
    private void ActivateOrb(){
        Orb.transform.position = new Vector2(attackPoint.position.x - 0.3f, attackPoint.position.y + 1f);
        Orb.GetComponent<Orb>().direction = -Mathf.Sign(transform.localScale.x);
        Orb.SetActive(true);
    }
}
