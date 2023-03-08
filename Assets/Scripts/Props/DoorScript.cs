using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D boxCollider2D;
    void Start()
    {
        anim=GetComponent<Animator>();
        GameManager.registerDoor(this);
        boxCollider2D=GetComponent<BoxCollider2D>();
    }

    public void openDoor() {
        anim.SetTrigger("Open");
        boxCollider2D.isTrigger=true;
        //update ui that door is open
    }
}
