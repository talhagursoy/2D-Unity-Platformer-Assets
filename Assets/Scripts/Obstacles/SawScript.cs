using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.CompareTag("Player"));
            
    }
}
