using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    public AudioClip audioClip;
    public float clipPitch = 1;
    public bool playAtStart = false;
    public bool trigger2DActivate;
    public bool destroyThisAfterActivation = false;
    // Use this for initialization
    void Start()
    {
        if (!trigger2DActivate && playAtStart)
        {
            MusicManager.play(audioClip, 1, 2);
            MusicManager.SetPitch(clipPitch);
            /*if (destroyThisAfterActivation)
                Destroy(transform.gameObject);*/
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            MusicManager.SetPitch(clipPitch);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (trigger2DActivate)
        {
            MusicManager.play(audioClip, 1, 2);
            MusicManager.SetPitch(clipPitch);

        }
    }
}
