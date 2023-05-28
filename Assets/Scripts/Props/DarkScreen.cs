using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DarkScreen : MonoBehaviour{
    public Torch[] torches;          // Array of torches
    private SpriteRenderer spriteRenderer;
    private float scaleFactor; // Scale factor to adjust the sprite size
    public float transitionDuration = 2f; // Duration of the transition in seconds
    private float prevScale;
    private float targetScale;
    private float transitionTimer;
    private float currentScale;
    private void Awake() {
        spriteRenderer=GetComponent<SpriteRenderer>();
        prevScale=spriteRenderer.transform.localScale.x;
    }
    private void Update(){
        int activeTorchCount = 0;
        for (int i = 0; i < torches.Length; i++){
            if (torches[i].isActive){
                activeTorchCount++;
            }
        } 
        switch (activeTorchCount) {
            case 1:
                scaleFactor=100;
                break;
            case 2:
                scaleFactor=120;
                break;
            case 3:
                scaleFactor=500;
                break;
            default :
                scaleFactor=0;
                break;
        }
        float scale = 100f + (activeTorchCount * scaleFactor);
        targetScale=scale;
        if (transitionTimer < transitionDuration){
            transitionTimer += Time.deltaTime;
            float t = Mathf.Clamp01(transitionTimer / transitionDuration);
            currentScale = Mathf.Lerp(prevScale, targetScale, t);
            spriteRenderer.transform.localScale = new Vector3(currentScale, currentScale, 1f);
        }
        else{
            transitionTimer=0;
            prevScale=currentScale;
        }
    }
}