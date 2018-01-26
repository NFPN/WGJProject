using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    void Update()
    {
        if(GameObject.Find("MainTheme") != null)
        {
            Destroy(GameObject.Find("MainTheme"));
        }
    }
}
