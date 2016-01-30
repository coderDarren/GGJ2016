using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Helper functions for the Window Manager
/// </summary>
public class WindowBehavior : MonoBehaviour {


    #region Manager Helper Functions

    /// <summary>
    /// Add page to a list 'pages' on the specified canvas.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="pages"></param>
    /// <param name="canvas"></param>
    public void CreatePage(GameObject prefab, ref List<GameObject> pages, GameObject canvas)
    {
        if (PageExists(prefab.name, pages))
        {
            Debug.Log("Found copy");
            return;
        }
        else
        {
            GameObject page = Instantiate(prefab) as GameObject;
            RectTransform rt = page.GetComponent<RectTransform>();
            rt.SetParent(canvas.transform);
            rt.offsetMax = Vector2.zero;
            rt.offsetMin = Vector2.zero;
            rt.localScale = Vector3.one;

            pages.Add(page);

            Debug.Log("Loaded page: " + page.name + ". Page Count: " + pages.Count);
        }

    }

    /// <summary>
    /// Finds a page in a list of pages and sets the page to be active or inactive.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pages"></param>
    /// <param name="active"></param>
    public void SetPageActive(GameObject page, List<GameObject> pages, bool active)
    {
        GameObject p = GetPage(page, pages);
        if (p)
        {
            Debug.Log("Setting page " + p.name + (active ? " active." : " inactive."));
            Button[] buttons = p.GetComponentsInChildren<Button>();
            foreach (Button b in buttons)
                b.interactable = active;
        }
    }


    void KillPage(GameObject page)
    {
        if (page != null)
        {
            WindowAnimationController wac = page.GetComponent<WindowAnimationController>();
            if (wac)
                wac.SetOpening(false); //tells the animator to transition to the closing animation
            else
                Destroy(page);
        }
    }

    /// <summary>
    /// Gets rid of all pages in front of the argument page in the list pages.
    /// </summary>
    /// <param name="page"></param>
    public void RemovePagesUpTo(GameObject page, ref List<GameObject> pages)
    {
        if (!PageExists(page.name, pages))
            return;
        for (int i = pages.Count - 1; i >= 0; i--)
        {
            if (pages[i].name != page.name + "(Clone)")
            {
                Debug.Log("Removing Page: " + pages[i].name);
                KillPage(pages[i]);
                pages.RemoveAt(i);
            }
            else
                break;
        }
    }

    /// <summary>
    /// Removes all pages in the list.
    /// </summary>
    /// <param name="pages"></param>
    public void RemoveAllPages(ref List<GameObject> pages)
    {
        GameObject temp = null;
        while (pages.Count > 0)
        {
            temp = pages[pages.Count - 1];
            pages.RemoveAt(pages.Count - 1);
            KillPage(temp);
        }
    }

    public void RemoveAllPagesExcept(string pageName, ref List<GameObject> pages)
    {
        for (int i = pages.Count - 1; i >= 0; i--)
        {
            if (pages[i].name != pageName+"(Clone)")
            {
                GameObject temp = pages[i];
                pages.RemoveAt(i);
                KillPage(temp);
            }
        }
    }

    GameObject GetPage(GameObject page, List<GameObject> pages)
    {
        GameObject p = null;
        foreach (GameObject go in pages)
        {
            if (go.name == page.name + "(Clone)")
                p = go;
        }
        return p;
    }

    public bool PageExists(string pageName, List<GameObject> pages)
    {
        for (int i = 0; i < pages.Count; i++)
        {
            if (pageName + "(Clone)" == pages[i].name)
                return true;
        }
        return false;
    }


    #endregion
}
