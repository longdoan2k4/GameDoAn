using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Player_Combat combat;
    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointSpent;
    }

    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointSpent;
    }

    private void HandleAbilityPointSpent(SkillSlot slot)
    {
        string skillName = slot.skillSO.skillName;

        switch (skillName)
        {
            case "Max Health Boost":
                StartsManager.Instance.UpdateMaxHealth(1);
                break;
            case "Sword Slash":
                combat.enabled = true;
                break;

            default:
                Debug.LogWarning("unknow skill: " + skillName);
                break;
        }
    }

}
