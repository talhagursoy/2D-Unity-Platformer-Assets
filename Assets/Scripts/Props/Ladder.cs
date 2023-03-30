using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public delegate void ladderHandler(bool isOnLadder);
    public event ladderHandler onCollision;
    private void OnTriggerEnter2D(Collider2D other) {
        onCollision?.Invoke(true);
    }
    private void OnTriggerExit2D(Collider2D other) {
        onCollision?.Invoke(false);
    }
}
