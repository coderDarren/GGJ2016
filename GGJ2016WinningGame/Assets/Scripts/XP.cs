using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class XP : MonoBehaviour {

    int maxXP;
    public int currentXP;
    public int level;
    public bool incrementing;
    public int incrementAmount;
    public float incrementRate;
    public int incrementSpeed;
    private float currentTime;
    public float nextIncrement;
    public Text xpText;
	// Use this for initialization
	void Start () {
        incrementing = false;
        currentTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        maxXP = 500 * level * level;

        currentTime += Time.deltaTime;

        if(currentXP >= maxXP)
        {
            level++;
        }

        if(incrementing && currentTime >= nextIncrement)
        {
            nextIncrement = currentTime + incrementRate;
            currentXP += incrementSpeed;
            incrementAmount -= incrementSpeed;
        }

        if(incrementAmount <= 0)
        {
            incrementing = false;
        }

        xpText.text = currentXP.ToString();


	}

    public void IncrementXp()
    {
        incrementAmount += 500;
        incrementing = true;
    }
}
