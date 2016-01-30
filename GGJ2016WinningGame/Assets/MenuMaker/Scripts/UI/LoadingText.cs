using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour {

    Text text;

    void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine("LoadingDots");
    }

    IEnumerator LoadingDots()
    {
        bool truth = true;
        while (truth)
        {
            yield return new WaitForSeconds(0.4f);
            text.text = "Loading";
            yield return new WaitForSeconds(0.4f);
            text.text = "Loading.";
            yield return new WaitForSeconds(0.4f);
            text.text = "Loading..";
            yield return new WaitForSeconds(0.4f);
            text.text = "Loading...";
        }
    }
}
