using UnityEngine;
using TMPro;

public class StartsManager : MonoBehaviour
{
    public static StartsManager Instance;
    public TMP_Text healthText;

    [Header("Combat Stats")]

    public int damage;
    public float weaponRange;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;

    [Header("Movement")]
    public int speed;

    [Header("Movement")]
    public int maxHealth;
    public int currentHealth;



    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateMaxHealth(int amount)
    {
        maxHealth += amount;
        healthText.text = "HP: " + currentHealth + "/" + maxHealth;
    }

}
