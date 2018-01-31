using UnityEngine;

public class Cheese : MonoBehaviour
{
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            MusicManager.setLoop(false);
            MusicManager.setVolume(0.25f);
            MusicManager.play(pickupSound);
            ItemValue.AddCheese();
            Destroy(gameObject);
        }
    }
}
