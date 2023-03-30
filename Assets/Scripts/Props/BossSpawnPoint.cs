using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnPoint : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    void Start(){
        GameManager.regbossSpawn(this);
    }
}
