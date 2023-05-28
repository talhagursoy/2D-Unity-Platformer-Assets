using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private Animator anim;
    private CircleCollider2D circleCollider2D;
    private SpriteRenderer sr;
    [SerializeField]
    private Sprite portal1;
    [SerializeField]
    private Sprite portal2;
    [SerializeField]
    private Sprite portal3;
    [SerializeField]
    private Sprite portal4;
    private ChestScript chestScript;
    private int count=1;
    private void Awake() {
        circleCollider2D=GetComponent<CircleCollider2D>();
        sr=GetComponent<SpriteRenderer>();
        anim=GetComponent<Animator>();
        sr.sprite = portal1;
    }
    void Start(){ 
        GameManager.registerDoor(this);
        ChestScript[] chestScripts = FindObjectsOfType<ChestScript>();
        foreach(ChestScript chestScript in chestScripts){
            chestScript.onChestOpen += portalOpen;
        }
    }
    private void portalOpen(){
        count++;
        switch(count){   
            case 2:
                sr.sprite = portal2;
                break;
            case 3:
                sr.sprite = portal3;
                break;
            case 4:
                sr.sprite = portal4;
                break;
            default :
                sr.sprite = portal1;
                break;
        }
    }
    public void openDoor() {
        circleCollider2D.isTrigger=true;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            GameManager.loadNextScene();
        }
    }
}
