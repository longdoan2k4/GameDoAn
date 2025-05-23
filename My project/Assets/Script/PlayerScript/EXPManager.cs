using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;
using System;
public class EXPManager : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int exptoLevel = 10;
    public float expGrowMultiplier = 1.2f;
    public Slider expSlider;

    public TMP_Text currentLevelText;

    public static event Action <int> OnLevelUp;


    public void Start()
    {
        UpdateUI();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GainExperience(2);
        }
    }

    private void OnEnable()
    {
        Enemy_Health.OnMonsterDefeated += GainExperience;
    }

    private void OnDisEnable()
    {
        Enemy_Health.OnMonsterDefeated -= GainExperience;
    }


    public void GainExperience(int amount)
    {
        currentExp += amount;
        if (currentExp >= exptoLevel)
        {
            LevelUP();
        }
        UpdateUI();
    }

    private void LevelUP()
    {
        level++;
        currentExp -= exptoLevel;
        exptoLevel = Mathf.RoundToInt(exptoLevel * expGrowMultiplier);
        OnLevelUp?.Invoke(1);
    }

    public void UpdateUI()
    {
        expSlider.maxValue = exptoLevel;
        expSlider.value = currentExp;
    }

}
