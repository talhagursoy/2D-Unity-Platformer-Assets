using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour{
    private Transform fireballSpawnPoint;
    private void deactivateObject() {
        gameObject.SetActive(false);
    }
    private void actiavetFireball() {
        Quaternion newRotation = Quaternion.Euler(0f,0f,transform.rotation.z-90f);
        Instantiate(Resources.Load<GameObject>("FireBall"), transform.position,newRotation);
    }
}
