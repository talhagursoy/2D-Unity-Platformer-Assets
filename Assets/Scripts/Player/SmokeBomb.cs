using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBomb : Ability{
    [SerializeField]
    private AudioClip dropBomb;
    public override void Activate(){
        SoundManager.instance.playSound(dropBomb);
        Instantiate(Resources.Load<GameObject>("Smoke"),transform.position, Quaternion.identity);
    }
}
