using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public TMP_Text healtext;

    public void Start()
    {
        healtext.text = "HP:" + currentHealth + " / " + maxHealth;
    }
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        healtext.text = "HP:" + currentHealth + " / " + maxHealth;
        
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}