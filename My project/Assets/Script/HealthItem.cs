using UnityEngine;
using System;
public class HealthItem : MonoBehaviour, IItem
{
    public int healAmount =1;
    public event Action<int> OnHealthCollect;

    public void Collect()
    {
        OnHealthCollect.Invoke(healAmount);
        Destroy(gameObject);
    }
}
