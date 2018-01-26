using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int lvNum;
    public float logoTimeTillNextScene;
    public bool loadNextScene = false;
    private Animator anim;


    void Start()
    {
        //Default is 2 seconds till fade in starts
        if (logoTimeTillNextScene == 0)
            logoTimeTillNextScene = 2;

        //Loads the next scene automatically if its the logo
        if (SceneManager.GetActiveScene().buildIndex == 0)
            StartCoroutine(SceneChangeInit(SceneManager.GetActiveScene().buildIndex));
        if (loadNextScene)
            SceneToChangeTo(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Call this before changing to a scene with a Retry button for the last level
    static public void SaveSceneAsPreviousScene()
    {
        PlayerPrefs.SetInt("LastSceneNum", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
    }

    //Call only if last scene was saved
    public void LoadPreviousScene()
    {
        if (PlayerPrefs.HasKey("LastSceneNum"))
        {
            lvNum = PlayerPrefs.GetInt("LastSceneNum");
            if (lvNum != SceneManager.GetActiveScene().buildIndex)
            {
                StartCoroutine(SceneChange());
            }
        }
    }

    //Loads the scene with specified build number
    public void SceneToChangeTo(int SceneToChangeTo)
    {
        lvNum = SceneToChangeTo;
        StartCoroutine(SceneChange());
    }
    //Loads the scene with specified name
    public void SceneToChangeTo(string SceneToChangeTo)
    {
        StartCoroutine(SceneChange(SceneToChangeTo));
    }
    // Alternative method showing to wait a key press to continue and using a second load scene
    public void ChangeSceneAsync(string sceneToName)
    {
        StartCoroutine(SceneChangeAsync(sceneToName));
    }

    // Use to Add a scene to current one 
    public void AddScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
    }

    //Unload the scene you added. OBS: Don't use different scene index from what you loaded.
    public void UnloadAddScene(int sceneIndex)
    {
        //SceneManager.UnloadScene(sceneIndex);
        SceneManager.UnloadSceneAsync(sceneIndex);
    }

    IEnumerator SceneChange()
    {
        Fading fadeScript;
        fadeScript = GameObject.Find("SceneLevelManager").GetComponent<Fading>();
        GameObject.Find("SceneLevelManager").GetComponent<Fading>().BeginFade(1);
        while (fadeScript.alpha <= 0.9f)
        {
            yield return new WaitForSeconds(0.1f);
        }
        SceneManager.LoadScene(lvNum);
    }

    IEnumerator SceneChange(string sceneNameToLoad)
    {
        Fading fadeScript;
        fadeScript = GameObject.Find("SceneLevelManager").GetComponent<Fading>();
        GameObject.Find("SceneLevelManager").GetComponent<Fading>().BeginFade(1);
        while (fadeScript.alpha <= 0.9f)
        {
            yield return new WaitForSeconds(0.1f);
        }
        SceneManager.LoadScene(sceneNameToLoad);
    }

    IEnumerator SceneChangeAsync(string sceneNameToLoad)
    {
        Fading fadeScript;
        fadeScript = GameObject.Find("SceneLevelManager").GetComponent<Fading>();
        GameObject.Find("SceneLevelManager").GetComponent<Fading>().BeginFade(1);
        while (fadeScript.alpha <= 0.95f)
        {
            yield return new WaitForSeconds(0.1f);
        }
        LoadScreen.Instance.LoadLevel(sceneNameToLoad, true, "LoadScreen");
    }

    IEnumerator SceneChangeInit(int sceneIndex)
    {
        yield return new WaitForSeconds(logoTimeTillNextScene);
        float fadeTime = GameObject.Find("SceneLevelManager").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime + 0.2f);
        SceneManager.LoadScene(sceneIndex + 1);
    }

    public void ReloadScene()
    {
        SceneToChangeTo(SceneManager.GetActiveScene().buildIndex);
    }
}
