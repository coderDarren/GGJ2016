using UnityEngine;
using UnityEngine.UI;

public class PlayerAbility : MonoBehaviour {

    public Text unlockText;
    public Button acceptButton;
    public int levelRequirement;
    public Color acceptColor;
    public enum Ability
    {
        Stars,
        Dive,
        Punch
    }
    public Ability ability;

    int playerLevel;

    void Start()
    {
        playerLevel = PlayerData.Instance.level;
        if (playerLevel >= levelRequirement)
        {
            switch(ability)
            {
                case Ability.Dive:
                    if (!PlayerData.Instance.hasDive)
                    {
                        acceptButton.gameObject.SetActive(true);
                        unlockText.gameObject.SetActive(false);
                    }
                    else {
                        acceptButton.gameObject.SetActive(false);
                        unlockText.color = acceptColor;
                        unlockText.text = "You have this ability.";

                    }
                    break;
                case Ability.Punch:
                    if (!PlayerData.Instance.hasPunch)
                    {
                        acceptButton.gameObject.SetActive(true);
                        unlockText.gameObject.SetActive(false);
                    }
                    else {
                        acceptButton.gameObject.SetActive(false);
                        unlockText.color = acceptColor;
                        unlockText.text = "You have this ability.";

                    }
                    break;
                case Ability.Stars:
                    if (!PlayerData.Instance.hasNinjaStar)
                    {
                        acceptButton.gameObject.SetActive(true);
                        unlockText.gameObject.SetActive(false);
                    }
                    else {
                        acceptButton.gameObject.SetActive(false);
                        unlockText.color = acceptColor;
                        unlockText.text = "You have this ability.";
                    }
                    break;
            }
        }    
        else
        {
            acceptButton.gameObject.SetActive(false);
            unlockText.text = "Unlocks at LVL " + levelRequirement;
        }
    }

    public delegate void AbilityDelegate(string ability);
    public static event AbilityDelegate ActivateAbility;

    public void AcceptAbility()
    {
        switch(ability)
        {
            case Ability.Dive:
                PlayerData.Instance.hasDive = true;
                ActivateAbility("dive");
                break;
            case Ability.Punch:
                PlayerData.Instance.hasPunch = true;
                ActivateAbility("punch");
                break;
            case Ability.Stars:
                PlayerData.Instance.hasNinjaStar = true;
                ActivateAbility("ninja");

                break;
        }
        acceptButton.gameObject.SetActive(false);
        unlockText.gameObject.SetActive(true);
        unlockText.color = acceptColor;
        unlockText.text = "You have this ability.";
    }
}
