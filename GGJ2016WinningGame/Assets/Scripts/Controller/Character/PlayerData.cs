using UnityEngine;
using UnityEngine.Experimental.Director;

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
    public bool hasNinjaStar = false, hasDive = false, hasPunch = false;
    public bool canDive = false, canNinja = false, canPunch = false;
    public int skillPoints;

    Animator anim;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        maxXP = 100;
        maxEnergy = 100;
        maxHealth = 100;
        anim = GetComponent<Animator>();

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
        currXP += healthOverTime * 5 * Time.deltaTime;

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
        
        if (hasNinjaStar)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !canNinja)
            {
                Debug.Log("Throw Star");
                canNinja = true;
            }
        }
        if (hasDive)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && !canDive)
            {
                canDive = true;
                Debug.Log("Dive");
            }
        }
        if (hasPunch)
        {
            if (Input.GetKeyDown(KeyCode.Mouse2) && !canPunch)
            {
                canPunch = true;
                Debug.Log("Punch");
            }
        }
    }

}
