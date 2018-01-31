using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
	private void Awake ()
    {
		DontDestroyOnLoad(gameObject);
	}

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            Destroy(gameObject);
        }
    }
}