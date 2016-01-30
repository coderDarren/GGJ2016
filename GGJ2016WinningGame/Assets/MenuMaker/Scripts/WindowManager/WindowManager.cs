using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;

public class WindowManager : WindowBehavior {

    [System.Serializable]
    public class Page
    {
        public GameObject page;
        public RuntimeAnimatorController animController;
        public float inAnimSpeed;
        public float outAnimSpeed;
        public AudioSource openSound;
        public AudioClip audioClip;
        public bool useSound = false;
        public float volume;
        public float pitch;
        public bool loop = false;
    }

    public List<Page> pages = new List<Page>();
    List<GameObject> activePages;
    WindowState stateManager;
    static WindowManager Instance;
    GameObject canvas;
    string currentLevel = "";
    float loadTimer = 0;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        stateManager = GetComponent<WindowState>();
        activePages = new List<GameObject>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    /// <summary>
    /// ////////////////////////////////////////Page Displaying////////////////////////////////
    /// </summary>
    
    public void DisplayPage(string pageName, string removeUpToPage, string resourceFolder, bool removeAllPages)
    {
        string pagePath = resourceFolder+ "/" + pageName;
        string removePage = resourceFolder+ "/" + removeUpToPage;
        //Debug.Log("Page Path: " + pagePath);
        GameObject newPage = null;
        GameObject basePage = null;

        if (removeUpToPage != "" && removeUpToPage != null)
        {
            basePage = Resources.Load(removePage) as GameObject;
            RemovePagesUpTo(basePage, ref activePages);
            SetPageActive(basePage, activePages, false);
        }
        else if (removeUpToPage != "")
            Debug.LogError("You are trying to remove up to a page that does not exist: " + removeUpToPage);

        if (removeAllPages)
        {
            //if the page we are loading is already loaded, we do not want to remove it
            //below, we call CreatePage, but this will only create the page if it does not exist already
            if (PageExists(pageName, activePages))
                RemoveAllPagesExcept(pageName, ref activePages);
            else
                RemoveAllPages(ref activePages);
        }

        if (pageName != "" && pagePath != null)
        {
            newPage = Resources.Load(pagePath) as GameObject;
            Debug.Log("Trying to create " + newPage.name);
            CreatePage(newPage, ref activePages, canvas);
            SetPageActive(newPage, activePages, true);
        }
        else
            Debug.LogError("You are trying to add a page that does not exist: " + pageName);

        //Debug.Log("Page added");
    }

    /// <summary>
    /// ////////////////////////////////////////LOADING////////////////////////////////
    /// </summary>


    public void LoadLevel(string toLevel) //invoked once
    {
        currentLevel = toLevel;
        //asyncLoad = Application.LoadLevelAsync(toLevel);
        Application.LoadLevel(toLevel);
    }

    public void SetState(string toState) //continuously invoked
    {
        //Debug.Log("Load Progress: " + asyncLoad.progress);
        loadTimer += Time.deltaTime;
        if (Application.loadedLevelName == currentLevel && loadTimer > 2) //fake loading time for testing
        {
            stateManager.SetState(toState);
            loadTimer = 0;
        }
    }
}
