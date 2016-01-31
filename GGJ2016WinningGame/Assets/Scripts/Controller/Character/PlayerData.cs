using UnityEngine;

public class PlayerData : MonoBehaviour {

    public static PlayerData Instance;

    public float energyOverTime = 1;
    public float healthOverTime = 2;
    public int currXP, maxXP;
    public float currEnergy, maxEnergy;
    public float currHealth, maxHealth;
    public int level;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        maxXP = 100;
        maxEnergy = 100;
        maxHealth = 100;
    }

    void Update()
    {
        if (currXP >= maxXP)
        {
            level++;
            currXP = 0;
            maxXP = level * 100;
        }

        currEnergy += energyOverTime * Time.deltaTime;
        currHealth += healthOverTime * Time.deltaTime;

        if (currEnergy >= maxEnergy)
            currEnergy = maxEnergy;
        if (currHealth >= maxHealth)
            currHealth = maxHealth;
        if (currEnergy <= 0)
            currEnergy = 0;
        if (currHealth <= 0)
            currHealth = 0;

    }

}
