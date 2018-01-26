using UnityEngine;
using System.Collections;

public class PressToContinue : MonoBehaviour
{
    UnityEngine.UI.Text text;
    
    bool sceneIsReady = false;
    void Start()
    {
        text = GetComponent<UnityEngine.UI.Text>();
        text.enabled = false;

        // Enable the text when the load is completed
        LoadScreen.Instance.OnStartWaitingEventToActivateScene += delegate
        {
            text.enabled = true;
            sceneIsReady = true;
        };
    }

    void Update()
    {
        if (sceneIsReady && Input.anyKeyDown)
        {
            StartCoroutine(StartingScene());
        }
    }

    IEnumerator StartingScene()
    {
        float fadeTime = GameObject.Find("SceneLevelManager").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        // When we got any get down and the text is enabled (when the event OnWaitingEventToActivateScene is fired)
        LoadScreen.Instance.ActivateScene();
    }
}