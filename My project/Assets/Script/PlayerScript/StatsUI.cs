using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlots;
    public CanvasGroup statsCanvas;
    private bool statsOpen = false;

    public void Start()
    {
        UpdateAllStats();
    }

    public void Update()
    {
        if (Input.GetButtonDown("ToggleStats"))
            if (statsOpen)
            {
                Time.timeScale = 1;
                UpdateAllStats();
                statsCanvas.alpha = 0;
                statsOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                UpdateAllStats();
                statsCanvas.alpha = 1;
                statsOpen = true;
            }
            
    }

    public void UpdateDamage()
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = "Damage: " + StartsManager.Instance.damage;
    }

    public void UpdateSpeed()
    {
        statsSlots[1].GetComponentInChildren<TMP_Text>().text = "Speed: " + StartsManager.Instance.speed;
    }

    public void UpdateAllStats()
    {
        UpdateDamage();
        UpdateSpeed();
    }

}
