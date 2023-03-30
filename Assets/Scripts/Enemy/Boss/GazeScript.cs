using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeScript : MonoBehaviour{
    public DarkWave circlePrefab;
    [SerializeField]
    private Transform player;
    private PlayerMovement pm;
    private void Awake() {
        pm=player.GetComponent<PlayerMovement>();
    }
    private void activateGaze() {
        DarkWave circleEffect = Instantiate(circlePrefab, transform.position, Quaternion.identity);
        circleEffect.StartEffect();
        if((player.localScale.x*transform.localScale.x)<0){
            pm.StartCoroutine("darkenPlayer");
        }
    }
    private void deactivateObject() {
        gameObject.SetActive(false);
    }
}
