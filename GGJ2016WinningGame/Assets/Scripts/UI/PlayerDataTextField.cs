using UnityEngine;
using UnityEngine.UI;


public class PlayerDataTextField : MonoBehaviour {

    public enum PlayerDataType
    {
        Level,
        SkillPoint
        
    }
    public PlayerDataType playerDataType;

    Text t;

    void Start()
    {
        t = GetComponent<Text>();
        switch (playerDataType)
        {
            case PlayerDataType.Level: break;
            case PlayerDataType.SkillPoint: t.text = "Skill Points: " +PlayerData.Instance.skillPoints.ToString(); break;
        }
    }

    void Update()
    {
        if (playerDataType == PlayerDataType.Level)
            t.text = PlayerData.Instance.level.ToString();
    }
}
