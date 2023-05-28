using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour, IDamagable
{
    [SerializeField]
    protected float maxHealth;
    public float currentHealth;
    protected SpriteRenderer[] childRenderers;
    protected int numberofFlashes=3;
    [SerializeField]
    protected float transparentDuration;
    [SerializeField]
    protected GameObject deathVfx;
    public bool immune;
    protected virtual void Awake() {
        currentHealth=maxHealth;
        childRenderers = transform.GetComponentsInChildren<SpriteRenderer>();
        immune=false;
    }
    public abstract void TakeDamage(float damage,float direction);
    //protected abstract void Die();
    protected virtual void FlashCompleted(){}
    protected IEnumerator flash() {
        Color flashColor = new Color(1f, 1f, 1f, 0.1f);
        Color normalColor = new Color(1f, 1f, 1f, 1f);
        for (int i = 0; i < numberofFlashes; i++) {
            foreach(SpriteRenderer childRenderer in childRenderers) {
                childRenderer.color = flashColor;
            }
            yield return new WaitForSeconds(transparentDuration);
            foreach(SpriteRenderer childRenderer in childRenderers) {
                childRenderer.color = normalColor;
            }
            yield return new WaitForSeconds(transparentDuration);
        }
        FlashCompleted();
    }
}