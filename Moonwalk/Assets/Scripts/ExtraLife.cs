using UnityEngine;

public class ExtraLife : MonoBehaviour
{

    public string heartbeat = "Heartbeat";
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("FREAK OUT! No AudioManager found in the scene.");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            audioManager.PlaySound(heartbeat);
            Destroy(gameObject);
            GameMaster._remainingLives += 1;
        }

    }

    private void Update()
    {
        Destroy(gameObject, 5f);
    }
}
