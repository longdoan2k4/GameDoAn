using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public TMP_Text healtext;

    public void Start()
    {
        healtext.text = "HP:" + StartsManager.Instance.currentHealth + " / " + StartsManager.Instance.maxHealth;
    }
    public void ChangeHealth(int amount)
    {
        StartsManager.Instance.currentHealth += amount;
        healtext.text = "HP:" + StartsManager.Instance.currentHealth + " / " + StartsManager.Instance.maxHealth;
        
        if (StartsManager.Instance.currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}