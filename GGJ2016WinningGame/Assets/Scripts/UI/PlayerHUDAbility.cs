using UnityEngine;
using System.Collections;

public class PlayerHUDAbility : MonoBehaviour {

    public string ability;
    CanvasGroup canvas;

    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
    }

	void OnEnable()
    {
        PlayerAbility.ActivateAbility += ActivateAbility;
    }

    void OnDisable()
    {
        PlayerAbility.ActivateAbility -= ActivateAbility;
    }

    void ActivateAbility(string ability)
    {
        if (this.ability == ability)
            canvas.alpha = 1;
    }
}
