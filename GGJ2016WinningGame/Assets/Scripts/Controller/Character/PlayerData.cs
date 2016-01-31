using UnityEngine;

public class PlayerData : MonoBehaviour {

    public static PlayerData Instance;

    public float energyOverTime = 1;
    public float healthOverTime = 2;
    public float currXP, maxXP;
    public float currEnergy, maxEnergy;
    public float currHealth, maxHealth;
    public int level;
    public int maxSpeed, currSpeed;
    public int maxStealth, currStealth;
    public int maxDamage, currDamage;
    public bool hasNinjaStar = false, hasDive = false, hasStrangle = false;
    public int skillPoints;

    void Awake()
    {
        Instance = this;
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
            skillPoints += 5;
        }

        currEnergy += energyOverTime * Time.deltaTime;
        currHealth += healthOverTime * Time.deltaTime;
        currXP += healthOverTime * 10 * Time.deltaTime;

        if (currEnergy >= maxEnergy)
            currEnergy = maxEnergy;
        if (currHealth >= maxHealth)
            currHealth = maxHealth;
        if (currEnergy <= 0)
            currEnergy = 0;
        if (currHealth <= 0)
            currHealth = 0;

        if (currDamage >= maxDamage)
            currDamage = maxDamage;
        if (currStealth >= maxStealth)
            currStealth = maxStealth;
        if (currSpeed >= maxSpeed)
            currSpeed = maxSpeed;

    }

}
