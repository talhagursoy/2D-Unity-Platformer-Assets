using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float camSpeed;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float followDistance;
    private float followOffset;
    [SerializeField]
    private float yOffset;
    private void Update() {
        transform.position=new Vector3(player.position.x+followOffset,player.transform.position.y+yOffset,transform.position.z);
        followOffset=Mathf.Lerp(followOffset,-followDistance*player.localScale.x,Time.deltaTime * camSpeed);
    }
}
