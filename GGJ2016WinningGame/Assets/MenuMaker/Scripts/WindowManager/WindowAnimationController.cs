using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class WindowAnimationController : MonoBehaviour {

    public float exitTimeToDestroy = 1;

    Animator anim;
    bool opening = true;
    float exitTimer = 0;

    void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Opening", true);
    }

    void Update()
    {
        if (!opening)
        {
            exitTimer += Time.deltaTime;
            if (exitTimer > exitTimeToDestroy)
                Destroy(gameObject);
        }
    }

    public void SetOpening(bool opening)
    {
        this.opening = opening;
        anim.SetBool("Opening", opening);
    }
}
