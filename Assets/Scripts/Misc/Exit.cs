using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(1);
        if(collision.gameObject.tag == "Player")
        {
            GameObject.Find("SceneLevelManager").GetComponent<ChangeScene>().SceneToChangeTo(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
