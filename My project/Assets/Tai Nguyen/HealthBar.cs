using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthBar : MonoBehaviour
{
    public Image HP;
    public TextMeshProUGUI HPText;

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        HP.fillAmount = currentHealth / maxHealth;
        HPText.text = currentHealth + "/" + maxHealth;
    }  
}
