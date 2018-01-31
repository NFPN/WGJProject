using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log(1);
            GameObject.Find("SceneLevelManager").GetComponent<ChangeScene>().SceneToChangeTo(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
