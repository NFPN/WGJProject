using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class LoadScreen : MonoBehaviour
{
    // Singleton
    public static LoadScreen Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("_LoadScreen");
                go.hideFlags = HideFlags.HideInHierarchy;
                instance = go.AddComponent<LoadScreen>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    private static LoadScreen instance;

    
    // manualSceneActivation If set to true, when the loading is complete, call ActivateScene() to continue.
    public void LoadLevel(string levelName, bool manualSceneActivation = false, string customLoadScene = "LoadScreen")
    {
        StopAllCoroutines();
        StartCoroutine(DoLoadLevel(levelName, customLoadScene, manualSceneActivation));
    }
    
    public void LoadLevel(string levelName, string customLoadScene)
    {
        LoadLevel(levelName, false, customLoadScene);
    }
   
    // Return the percentage of the loading. The value is between 0-100.    
    public int Progress = 0;
    private bool _activeScene = false;
 
    public void ActivateScene()
    {
        _activeScene = true;
    }

    public delegate void LoadEvent();
   
    // Occurs when the scene finished loading and start waiting to a call for ActivateScene() to continue.  
    public event LoadEvent OnStartWaitingEventToActivateScene;

    IEnumerator DoLoadLevel(string name, string customLoadScene, bool manualActivation)
    {
        // Reset
        Progress = 0;
        OnStartWaitingEventToActivateScene = null;
        _activeScene = true;

        // Load the loading scene
        SceneManager.LoadScene(customLoadScene);
        yield return null;
        yield return null;

        // Load the scene async
        AsyncOperation async = SceneManager.LoadSceneAsync(name);


        if (manualActivation)
        {
            async.allowSceneActivation = false;
            _activeScene = false;
        }

        while (!async.isDone)
        {
            if (manualActivation)
            {
                if (async.progress < 0.9f)
                {
                    Progress = Mathf.RoundToInt(Mathf.Clamp((async.progress / 0.9f) * 100, 0, 100));
                }
                else
                {
                    // the first load phase was completed
                    Progress = 100;
                    if (!async.allowSceneActivation)
                    {
                        //  notify the end of first loading phase
                        if (OnStartWaitingEventToActivateScene != null) OnStartWaitingEventToActivateScene();
                        // Call LoadScreen.instance.ActivateScene(); to continue
                        while (!_activeScene) { yield return null; }
                        async.allowSceneActivation = true;
                    }
                }
            }
            else
            {
                Progress = Mathf.RoundToInt(async.progress * 100);
            }
            yield return null;
        }
        OnStartWaitingEventToActivateScene = null;
    }

   
    // Loads the add level. but don't use loading scene. Use two callbacks to display and hide.   
    public void LoadLevelAddictive(string name)
    {
        StopAllCoroutines();
        StartCoroutine(DoLoadLevelAddictive(name));
    }
   
    // Use this to display  progress GUI, images, etc. Register this callback before call LoadLevelAddictive.   
    public event LoadEvent OnStartLoadEventAddictive;
    
    // When the scene finish loading. Use to hide progress GUI, images, etc. Register before call LoadLevelAddictive.   
    public event LoadEvent OnEndLoadEventAddictive;


    IEnumerator DoLoadLevelAddictive(string name)
    {
        // Reset the percentage
        Progress = 0;
        if (OnStartLoadEventAddictive != null) OnStartLoadEventAddictive();
        yield return null;
       
        AsyncOperation async = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

        while (!async.isDone)
        {
            // updates the percentage
            Progress = Mathf.RoundToInt(async.progress * 100);
            yield return null;
        }
        // Reset the events
        if (OnEndLoadEventAddictive != null) OnEndLoadEventAddictive();
        OnStartLoadEventAddictive = null;
        OnEndLoadEventAddictive = null;
    }
}