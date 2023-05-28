using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour{
    private float interactRange = 2f;   // Range at which the player can interact with the torch
    private Animator anim;
    [SerializeField]
    private Transform player;
    public bool isActive;
    private void Awake() {
        anim = transform.GetComponent<Animator>();
        anim.SetBool("Light",true);
        isActive=true;
    }
    private void Update(){
        if (Vector3.Distance(transform.position, player.position) <= interactRange){
            if (Input.GetKeyDown(KeyCode.E)){
                if(!isActive){
                    active(true);
                    GameManager.torchCount();
                }
            }
        }
        if (Vector3.Distance(transform.position, player.position) <= interactRange){
            if (Input.GetKeyDown(KeyCode.F)){
                active(false);
            }
        }
    }
    public void active(bool act) {
        anim.SetBool("Light",act);
        isActive=act;
    }
}