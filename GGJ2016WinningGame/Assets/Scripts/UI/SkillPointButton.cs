using UnityEngine;
using System.Collections;

public class SkillPointButton : MonoBehaviour {

	public enum Skill
    {
        Speed,
        Stealth,
        Damage
    }
    public Skill skill;

    public void RaiseSkill()
    {
        if (PlayerData.Instance.skillPoints > 0)
        {
            switch (skill)
            {
                case Skill.Damage:
                    PlayerData.Instance.currDamage++;
                    break;
                case Skill.Speed:
                    PlayerData.Instance.currSpeed++;
                    break;
                case Skill.Stealth:
                    PlayerData.Instance.currStealth++;
                    break;
            }
            PlayerData.Instance.skillPoints--;
        }
    }

}
