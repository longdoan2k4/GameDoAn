using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public TMP_Text healtext;

    private void Start()
    {
        UpdateHealthUI();
    }

    public void ChangeHealth(int amount)
    {
        StartsManager.Instance.currentHealth += amount;

        // Clamp health within range
        if (StartsManager.Instance.currentHealth > StartsManager.Instance.maxHealth)
        {
            StartsManager.Instance.currentHealth = StartsManager.Instance.maxHealth;
        }

        UpdateHealthUI();

        if (StartsManager.Instance.currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void UpdateHealthUI()
    {
        healtext.text = "HP: " + StartsManager.Instance.currentHealth + " / " + StartsManager.Instance.maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HealthItem item = other.GetComponent<HealthItem>();
        if (item != null)
        {
            // Đăng ký event rồi thu thập
            item.OnHealthCollect += Heal;
            item.Collect();
        }
    }

    private void Heal(int amount)
    {
        ChangeHealth(amount);
    }
}
