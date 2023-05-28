using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityManager : MonoBehaviour{
    public Ability[] abilities;
    private float[] cooldownTimers;
    public TextMeshProUGUI[] cooldownTexts;
    private void OnEnable() {
        abilities=GameObject.Find("Player").GetComponents<Ability>();
    }
    private void Start(){
        cooldownTimers = new float[abilities.Length];
        for (int i = 0; i < abilities.Length; i++)
        {
            cooldownTimers[i] = 0f;
            cooldownTexts[i].text = "";
        }
    }
    private void Update()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            if (cooldownTimers[i] > 0f)
            {
                cooldownTimers[i] -= Time.deltaTime;
                if (cooldownTimers[i] < 0f)
                {
                    cooldownTimers[i] = 0f;
                }
                cooldownTexts[i].text = Mathf.CeilToInt(cooldownTimers[i]).ToString();
            }
            else
            {
                cooldownTexts[i].text = "";
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateAbility(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateAbility(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateAbility(2);
        }
    }

    public void ActivateAbility(int abilityIndex){
        if (cooldownTimers[abilityIndex] <= 0f){
            abilities[abilityIndex].Activate();
            cooldownTimers[abilityIndex] = abilities[abilityIndex].cooldown;
            cooldownTexts[abilityIndex].text = Mathf.CeilToInt(cooldownTimers[abilityIndex]).ToString();
        }
    }
}