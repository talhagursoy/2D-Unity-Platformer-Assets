using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Health playerHealth;
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private Image currentHealthBar;

    void Update()
    {
        currentHealthBar.fillAmount=playerHealth.currentHealth/50;
    }
}
