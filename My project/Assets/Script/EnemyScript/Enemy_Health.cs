using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int expReward = 3;

    public delegate void MonsterDefeated(int exp);
    public static event MonsterDefeated OnMonsterDefeated;
    public int currentHealth;
    public int maxHealth;

    [Header("Loop")]
    public List<LoopItems> loopTable = new List<LoopItems>();
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            OnMonsterDefeated(expReward);

            foreach (LoopItems loopItems in loopTable)
            {
                if (UnityEngine.Random.Range(0f, 100f) <= loopItems.dropChange)
                {
                    InstantiateLoot(loopItems.ItemPrefab);
                }
                break;
            }
            Destroy(gameObject);
        }
    }

    void InstantiateLoot(GameObject loot)
    {
        if (loot)
        {
            GameObject droppedLoot = Instantiate(loot, transform.position, quaternion.identity);

            droppedLoot.GetComponent<SpriteRenderer>().color = Color.red;
        }

    }

}
