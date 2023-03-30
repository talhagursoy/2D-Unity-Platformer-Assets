using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D boxCollider2D;
    [SerializeField]
    private GameObject vfx;
    public delegate void ChestOpened();
    public event ChestOpened onChestOpen;
    private void Start() {
        anim=GetComponent<Animator>();
        GameManager.registerChest(this);
        boxCollider2D=GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            anim.SetTrigger("Open");
            boxCollider2D.enabled=false;
            GameManager.chestOpened(this);
        }
    }
    private void initiateVFX() {
        Vector2 pos= new Vector2(transform.position.x,transform.position.y+1.5f);
        Instantiate(vfx,pos,transform.rotation);
        onChestOpen?.Invoke();
    }
}
