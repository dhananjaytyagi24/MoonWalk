using UnityEngine;

public class ExtraOxygen : MonoBehaviour
{

    public string breathing = "Breathing";
    private AudioManager audioManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            audioManager.PlaySound(breathing);
            Destroy(gameObject);
            GameMaster.startTime = Time.time;
        }
    }

    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("FREAK OUT! No AudioManager found in the scene.");
        }
    }

    private void Update()
    {
        Destroy(gameObject, 5f);
    }
}
