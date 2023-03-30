using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkWave : MonoBehaviour
{
    public float maxRadius = 10f;
    public float growSpeed = 1f;
    public float fadeSpeed = 1f;
    private bool isGrowing = false;
    private SpriteRenderer spriteRenderer;
    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update(){
        if (isGrowing){
            transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
            if (transform.localScale.x >= maxRadius){
                isGrowing = false;
            }
        }
        else{
            spriteRenderer.color -= new Color(0f, 0f, 0f, fadeSpeed * Time.deltaTime);
            if (spriteRenderer.color.a <= 0f){
                Destroy(gameObject);
            }
        }
    }

    public void StartEffect(){
        transform.localScale = Vector3.zero;
        spriteRenderer.color = new Color(0f, 0f, 0f, 0.8f);
        isGrowing = true;
    }
}
